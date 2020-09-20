using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using static System.Math;

namespace DoomFire
{
    public class GLWindow : GameWindow
    {

        readonly FireData fireData;
        readonly int sizeW;
        readonly int sizeH;

        public GLWindow(int width, int height, FireData fireData)
            : base(width, height)
        {

            Title = "Doom Fire";
            this.fireData = fireData;
            sizeW = Max((int)Ceiling((double)Width / fireData.Cols), 1);
            sizeH = Max((int)Ceiling((double)Height / fireData.Rows), 1);

        }

        protected override void OnLoad(EventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, Width, 0.0, Height, -1.0, 1.0);
            base.OnLoad(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            fireData.Update();
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            RenderTable();

            base.OnRenderFrame(e);
            SwapBuffers();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            var current = (int)fireData.Wind;
            fireData.SetWind((FireWind)((current + 1) % 3));


            base.OnKeyDown(e);
        }

        private void RenderTable()
        {
            for (var i = 0; i < fireData.Rows; i++)
                for (var j = 0; j < fireData.Cols; j++)
                {
                    var colorIndex = fireData[i, j];
                    var color = ColorPalete.Colors[colorIndex];
                    DrawBlock(j * sizeW, i * sizeH, color);
                }
        }

        void DrawBlock(int x, int y, Color color)
        {
            y = Height - y;
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            GL.Vertex2(x, y);
            GL.Vertex2(x + sizeW, y);
            GL.Vertex2(x + sizeW, y - sizeH);
            GL.Vertex2(x, y - sizeH);
            GL.End();
        }
    }
}
