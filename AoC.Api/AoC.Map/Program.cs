using AoC.Api.Domain;
using System.Linq;

namespace AoC.MerovingieFileManager
{
    public class Program
    {
        // Reçoit en paramètre les options de création d'une partie
        static void Main(string[] gameOptions)
        {
            IGameDescriptor game;
            if (gameOptions.Count() == 0) game = GameGenerator.GenerateDefaultMap();
            else
            {
                // TODO: implémenter les options de création
                game = new GameDescriptor2();
            }


            GameFileManager.SaveGame(game, "game1");
        }
    }


}
