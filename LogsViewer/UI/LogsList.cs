using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsViewer.UI
{
    public class LogsList
    {
        public void Draw(BaseUI ui)
        {
            if (ImGui.BeginChild("Logs"))
            {
                if (ImGui.Button("Importer"))
                    ui.Manager.ImportLogs();
                ImGui.SameLine();
                if (ImGui.Button("Tout Supprimer"))
                    ui.Manager.DeleteLogs();
                ImGui.SameLine();
                if (ImGui.Button("Rafraichir"))
                    ui.Manager.UpdateListLogs();

                ImGui.Separator();

                var copy = new List<string>(ui.Manager.GetLogs());

                foreach (var logs in copy)
                {
                    if (ImGui.Selectable(logs))
                        ui.Viewer.Text = ui.Manager.OpenLogs(logs);
                    if (ImGui.IsItemHovered() && ImGui.IsMouseClicked(ImGuiMouseButton.Right))
                        ImGui.OpenPopup("Context Menu - " + logs);

                    if (ImGui.BeginPopup("Context Menu - " + logs))
                    {
                        if (ImGui.Selectable("Supprimer"))
                            ui.Manager.DeleteLogs(logs);
                        ImGui.EndPopup();
                    }
                }

                ImGui.EndChild();
            }
        }
    }
}
