using System;

namespace DoomFire
{

    class Program
    {
        [STAThread]
        static void Main()
        {
            var fire = new FireData(320, 168);
            using var x = new GLWindow(1024, 768, fire);
            x.Run(30);
        }
    }
}
