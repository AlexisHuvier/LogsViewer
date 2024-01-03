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
        private string _editName = "";

        public void Draw(BaseUI ui)
        {
            if (ImGui.BeginChild("Logs"))
            {
                if (ImGui.Button("Importer") && !ui.Process)
                    ui.Manager.ImportLogs(ui);
                ImGui.SameLine();
                if (ImGui.Button("Tout Supprimer") && !ui.Process)
                    ui.Manager.DeleteLogs(ui);
                ImGui.SameLine();
                if (ImGui.Button("Rafraichir") && !ui.Process)
                    ui.Manager.UpdateListLogs();

                ImGui.Separator();

                var copy = new List<string>(ui.Manager.GetLogs());

                foreach (var logs in copy)
                {
                    if (ImGui.Selectable(logs) && !ui.Process)
                        ui.Viewer.Text = ui.Manager.OpenLogs(logs);
                    if (ImGui.IsItemHovered() && ImGui.IsMouseClicked(ImGuiMouseButton.Right) && !ui.Process)
                    {
                        _editName = logs;
                        ImGui.OpenPopup("Context Menu - " + logs);
                    }

                    if (ImGui.BeginPopup("Context Menu - " + logs))
                    {
                        ImGui.InputText("Nom", ref _editName, 75);
                        if (ImGui.Button("Rename"))
                            ui.Manager.RenameLog(logs, _editName);
                        if (ImGui.Button("Supprimer"))
                            ui.Manager.DeleteLogs(logs);
                        ImGui.EndPopup();
                    }
                }

                ImGui.EndChild();
            }
        }
    }
}
