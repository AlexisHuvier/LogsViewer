using System;
using System.Collections.Generic;
using System.IO;
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

        public void DeleteLogs()
        {
            foreach (var log in _logCache.Keys)
                if (File.Exists(Path.Join("Logs", log)))
                    File.Delete(Path.Join("Logs", log));
            _logCache.Clear();
            UpdateListLogs();
        }
    }
}
