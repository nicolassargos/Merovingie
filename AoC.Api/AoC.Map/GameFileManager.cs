using Common.Dto;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AoC.Map
{
    public class GameFileManager
    {
        private static string GameFolder { get { 
                var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var _gameFolder = System.IO.Path.Combine(myDocs, "MerovingieGames");
                if (!Directory.Exists(_gameFolder))
                {
                    Directory.CreateDirectory(_gameFolder);
                }
                return _gameFolder;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="fileName"></param>
        public static void SaveGame(IGameDescriptor game, string fileName)
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(IGameDescriptor));

            var path = System.IO.Path.Combine(GameFolder, fileName,".xml");
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, game);
            file.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static IGameDescriptor ReadGame(string fileName)
        {
            var path = System.IO.Path.Combine(GameFolder, fileName, ".xml");

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(IGameDescriptor));


            var game = (IGameDescriptor)writer.Deserialize(new FileStream(path, FileMode.Open));

            return game;
        }

        /// <summary>
        /// Get the list of all games
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GameDetailsDto> GetGames()
        {
            var gameList = new List<GameDetailsDto>();
            var directoryInfos = new DirectoryInfo(GameFolder);

            foreach (var gamefile in directoryInfos.GetFiles("*.xml"))
            {
                gameList.Add(new GameDetailsDto(){ Name = gamefile.Name, Path = gamefile.Directory.ToString(), CreationDate = gamefile.CreationTime });
            }

            return gameList;
        }
    }
}
