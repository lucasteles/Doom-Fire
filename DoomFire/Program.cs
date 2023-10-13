using DoomFire;

var fire = new FireData(320, 168);
using var window = new GlWindow(1280, 672, fire);
window.Run(30);