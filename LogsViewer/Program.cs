using LogsViewer.UI;
using Raylib_cs;
using SharpEngine.Core;

namespace LogsViewer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);

            var baseUi = new BaseUI();
            var window = new Window(1200, 900, "Logs Viewer", SharpEngine.Core.Utils.Color.Black, null, true, true, true)
            {
                RenderImGui = baseUi.Draw
            };

            window.AddScene(new Scene());

            window.Run();
        }
    }
}
