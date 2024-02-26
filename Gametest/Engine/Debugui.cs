using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ImGuiNET;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Gametest
{
    public class DebugUI
    {
        private List<Slider> sliderList;
        private int fontTexture;

        public DebugUI()
        {
            sliderList = new List<Slider>();

            // Initialize ImGui
            ImGui.CreateContext();
            ImGuiIOPtr io = ImGui.GetIO();
            io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out int width, out int height, out _);
            fontTexture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, fontTexture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba,
                PixelType.UnsignedByte, pixels);
            io.Fonts.SetTexID((IntPtr)fontTexture);
        }

        public void Draw()
        {
            ImGui.NewFrame();
            ImGui.ShowDemoWindow();

            ImGui.Begin("Hello, world!");
            ImGui.Text("This is some useful text.");

            foreach (var slider in sliderList)
            {
                slider.Draw();
            }

            ImGui.End();

            ImGui.Render();
            RenderImDrawData(ImGui.GetDrawData());
        }

        private void RenderImDrawData(ImDrawDataPtr draw_data)
        {
            if (draw_data.CmdListsCount == 0)
                return;

            // Setup orthographic projection matrix
            var io = ImGui.GetIO();
            var orthoProjection =
                Matrix4.CreateOrthographicOffCenter(0.0f, io.DisplaySize.X, io.DisplaySize.Y, 0.0f, -1.0f, 1.0f);
            GL.UseProgram(0);
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.ScissorTest);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindSampler(0, 0);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            // Render command lists
            for (int n = 0; n < draw_data.CmdListsCount; n++)
            {
                ImDrawListPtr cmd_list = draw_data.CmdListsRange[n];

                GL.BufferData(BufferTarget.ArrayBuffer, cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>(),
                    cmd_list.VtxBuffer.Data, BufferUsageHint.StreamDraw);
                GL.BufferData(BufferTarget.ElementArrayBuffer, cmd_list.IdxBuffer.Size * sizeof(ushort),
                    cmd_list.IdxBuffer.Data, BufferUsageHint.StreamDraw);

                for (int cmd_i = 0; cmd_i < cmd_list.CmdBuffer.Size; cmd_i++)
                {
                    ImDrawCmdPtr pcmd = cmd_list.CmdBuffer[cmd_i];

                    if (pcmd.UserCallback != IntPtr.Zero)
                    {
                        throw new NotImplementedException("ImDrawCallback not implemented");
                    }
                    else
                    {
                        GL.BindTexture(TextureTarget.Texture2D, pcmd.TextureId.ToInt32());
                        GL.Scissor((int)pcmd.ClipRect.X, (int)(io.DisplaySize.Y - pcmd.ClipRect.W),
                            (int)(pcmd.ClipRect.Z - pcmd.ClipRect.X), (int)(pcmd.ClipRect.W - pcmd.ClipRect.Y));
                        GL.DrawElements(PrimitiveType.Triangles, (int)pcmd.ElemCount, DrawElementsType.UnsignedShort,
                            (IntPtr)(pcmd.IdxOffset * sizeof(ushort)));
                    }
                }
            }

            // Restore modified state
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.UseProgram(0);
            GL.Disable(EnableCap.ScissorTest);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void AddSlider(string name, ref float value, float min, float max)
        {
            sliderList.Add(new Slider(name, ref value, min, max));
        }

        public void Destroy()
        {
            GL.DeleteTexture(fontTexture);
            ImGui.DestroyContext();
        }
    }

    public class Slider
    {
        private string name;
        private float value;
        private float min;
        private float max;

        public Slider(string name, ref float value, float min, float max)
        {
            this.name = name;
            this.value = value;
            this.min = min;
            this.max = max;
        }

        public void Draw()
        {
            ImGui.SliderFloat(name, ref value, min, max);
        }
    }
}
