namespace TextBasedAdventureGame;
using Spectre.Console;

internal class Program
{
    static void Main()
    {
        
        GameEngine engine = new GameEngine();
        engine.ShowGameName();
        engine.GetPlayerName();
        engine.ShowIntroduction();
        engine.ShowInformationForPlayer();
        engine.BuildAllLevels();
        engine.PlayGame();
    }
}
