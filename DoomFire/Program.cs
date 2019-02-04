using System;

namespace DoomFire
{

    class Program
    {
        [STAThread]
        static void Main()
        {

            var fire = new FireData(60, 40);

            using (var x = new GLWindow(800, 600, fire))
                x.Run(30);

        }
    }
}
