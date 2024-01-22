namespace Karts.Infrastructure;

public static class EntryPoint
{
    public static void Main()
    {
        var game = new Engine.Karts();
        
        game.Run();
    }
}