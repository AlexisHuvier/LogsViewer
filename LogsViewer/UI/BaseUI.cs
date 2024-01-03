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
        public LogsManager Manager;
        public LogsList LogsList = new();
        public Viewer Viewer = new();
        public bool Process = false;
        public int MaxProcess = 0;
        public int CurrentProcess = 0;

        public BaseUI()
        {
            Manager = new LogsManager(this);
        }

        public void Draw(Window window)
        {
            var io = ImGui.GetIO();

            ImGui.SetNextWindowSize(new System.Numerics.Vector2(io.DisplaySize.X, io.DisplaySize.Y));
            ImGui.SetNextWindowPos(new System.Numerics.Vector2(0));
            if (ImGui.Begin("All", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoBringToFrontOnFocus))
            {
                ImGui.Columns(2, "Main", true);
                ImGui.SetColumnWidth(0, io.DisplaySize.X * 0.25f);
                ImGui.SetColumnWidth(1, io.DisplaySize.X * 0.75f);

                LogsList.Draw(this);

                ImGui.NextColumn();
                
                Viewer.Draw(this);

                ImGui.End();
            }

            if(Process)
            {
                ImGui.SetNextWindowPos(new System.Numerics.Vector2(io.DisplaySize.X * 0.5f, io.DisplaySize.Y * 0.5f), ImGuiCond.Always, new System.Numerics.Vector2(0.5f));
                ImGui.SetNextWindowSize(new System.Numerics.Vector2(400, 75));

                if (ImGui.Begin("Progress", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar))
                {
                    ImGui.Text($"Progression... {CurrentProcess}/{MaxProcess}");
                    ImGui.ProgressBar(CurrentProcess / (float)MaxProcess, new System.Numerics.Vector2(-1, 0));
                    ImGui.End();
                }

            }

            DebugManager.SeRenderImGui(window);
        }
    }
}
