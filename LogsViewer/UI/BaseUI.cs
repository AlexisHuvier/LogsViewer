using ImGuiNET;
using LogsViewer.Manager;
using SharpEngine.Core;
using SharpEngine.Core.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogsViewer.UI
{
    public class BaseUI
    {
        public LogsManager Manager = new();

        public void Draw(Window window)
        {
            var io = ImGui.GetIO();

            ImGui.SetNextWindowSize(new System.Numerics.Vector2(io.DisplaySize.X, io.DisplaySize.Y));
            ImGui.SetNextWindowPos(new System.Numerics.Vector2(0));
            if (ImGui.Begin("All", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize))
            {
                ImGui.Columns(2, "Main", true);
                ImGui.SetColumnWidth(0, io.DisplaySize.X * 0.25f);
                ImGui.SetColumnWidth(1, io.DisplaySize.X * 0.75f);
                if (ImGui.BeginChild("Logs"))
                {
                    ImGui.Button("Importer");
                    ImGui.SameLine();
                    ImGui.Button("Tout Supprimer");

                    ImGui.Separator();

                    foreach (var logs in Manager.ListLogs())
                        ImGui.Text(logs);

                    ImGui.EndChild();
                }
                ImGui.NextColumn();
                if (ImGui.BeginChild("Viewer"))
                {
                    ImGui.Text("VIEWER");
                    ImGui.EndChild();
                }
                ImGui.End();
            }

            DebugManager.SeRenderImGui(window);
        }
    }
}
