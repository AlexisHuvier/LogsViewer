using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsViewer.UI
{
    public class Viewer
    {
        public string Text { get; set; } = "Sélectionnez un log...";

        public void Draw(BaseUI ui)
        {
            if (ImGui.BeginChild("Viewer"))
            {
                ImGui.TextUnformatted(Text);
                ImGui.EndChild();
            }
        }
    }
}
