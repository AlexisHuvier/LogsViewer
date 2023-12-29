using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsViewer.Manager
{
    public class LogsManager
    {
        private Dictionary<string, string> _logCache = [];

        public LogsManager()
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");
        }

        public string[] ListLogs()
        {
            return Directory.GetFiles("Logs");
        }

        public string OpenLogs(string path)
        {
            if(!_logCache.ContainsKey(path))
            {
                var content = File.ReadAllText(Path.Join("Logs", path));
                _logCache.Add(path, content);
            }
                
            return _logCache[path];
        }
    }
}
