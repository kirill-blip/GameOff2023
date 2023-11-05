using Stride.Engine;

namespace GameOff2023
{
    class GameOff2023App
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
