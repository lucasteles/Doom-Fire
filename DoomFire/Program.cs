using DoomFire;

var fire = new FireData(320, 168);
using var window = new Game1(fire, 1280, 672);

window.Run();