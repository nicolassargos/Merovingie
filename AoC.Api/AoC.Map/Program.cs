using AoC.Api.Domain;
using Common.Struct;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Map
{
    public class Program
    {
        // Reçoit en paramètre les options de création d'une partie
        static void Main(string[] gameOptions)
        {
            GameDescriptor game;
            if (gameOptions.Count() == 0) game = GameGenerator.GenerateMap();
            else
            {
                // TODO: implémenter les options de création
                game = new GameDescriptor();
            }


            GameFileManager.SaveGame(game, "game1");
        }
    }


}
