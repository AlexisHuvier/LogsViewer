using LogsViewer.UI;
using SharpEngine.Core.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsViewer.Manager
{
    public class LogsManager
    {
        private Dictionary<string, string?> _logCache = [];

        public LogsManager()
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");
            UpdateListLogs();
        }

        public void UpdateListLogs()
        {
            _logCache.Clear();
            foreach (var log in Directory.GetFiles("Logs"))
                _logCache.Add(Path.GetFileName(log), null);
        }

        public Dictionary<string, string?>.KeyCollection GetLogs() => _logCache.Keys;

        public string OpenLogs(string path)
        {
            if (_logCache.TryGetValue(path, out var log) && log != null)
                return log;

            var content = File.ReadAllText(Path.Join("Logs", path));
            if(!_logCache.TryAdd(path, content))
                _logCache[path] = content;
            return content;
        }

        public void DeleteLogs(string path)
        {
            _logCache.Remove(path);
            if (File.Exists(Path.Join("Logs", path)))
                File.Delete(Path.Join("Logs", path));
            UpdateListLogs();
        }

        public void DeleteLogs(BaseUI ui)
        {
            Task.Run(() =>
            {
                ui.Process = true;
                ui.MaxProcess = _logCache.Keys.Count;
                ui.CurrentProcess = 0;

                foreach (var log in _logCache.Keys)
                {
                    if (File.Exists(Path.Join("Logs", log)))
                        File.Delete(Path.Join("Logs", log));
                    ui.CurrentProcess++;
                }

                ui.Process = false;
                _logCache.Clear();
                UpdateListLogs();
            });
        }

        public void ImportLogs(BaseUI ui, string? path = null)
        {
            Task.Run(() =>
            {
                path ??= Environment.ExpandEnvironmentVariables(Path.Combine("%appdata%", ".minecraft", "logs"));

                ui.Process = true;
                ui.MaxProcess = Directory.GetFiles(path).Length;
                ui.CurrentProcess = 0;

                foreach (var log in Directory.GetFiles(path))
                {
                    if (log.EndsWith(".log.gz") && !log.Contains("debug"))
                    {
                        using FileStream originalFileStream = File.OpenRead(log);

                        string currentFileName = Path.GetFileName(log);
                        string newFileName = Path.Combine("Logs", currentFileName.Remove(currentFileName.Length - Path.GetExtension(log).Length));

                        if (!File.Exists(newFileName))
                        {
                            using FileStream decompressedFileStream = File.Create(newFileName);
                            using GZipStream decompressionStream = new(originalFileStream, CompressionMode.Decompress);

                            decompressionStream.CopyTo(decompressedFileStream);
                            DebugManager.Log(SharpEngine.Core.Utils.LogLevel.LogDebug, "Imported : " + newFileName);
                        }
                    }
                    ui.CurrentProcess++;
                }

                ui.Process = false;
                UpdateListLogs();
            });            
        }
    }
}
