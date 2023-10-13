namespace DoomFire;

public static class Extensions
{
    public static FireWind Toggle(this FireWind wind) =>
        (FireWind) (((int) wind + 1) % 3);
}