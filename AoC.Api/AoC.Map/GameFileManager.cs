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
            if (game == null) throw new ArgumentNullException();

            // Initialise le Serializer
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));

            // Créé le chemin du fichier de sauvegarde
            var path = System.IO.Path.Combine(GameFolder, fileName+".xml");

            // Vérifie qu'un fichier portant le même nom n'existe pas déjà
            if (File.Exists(path))
            {
                var nbFileOccurences = Directory.GetFiles(GameFolder, "*"+fileName+"*").Count();
                path = path.Insert(path.Length - 4, nbFileOccurences.ToString());
            }

            FileStream file = File.Create(path);

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
            if (fileName == null) throw new ArgumentNullException("File name is empty");

            if (fileName.Substring(fileName.Length-4) != ".xml") throw new ArgumentException("File name has no valid extension");

            IGameDescriptor game = null;

            var path = System.IO.Path.Combine(GameFolder, fileName);

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));

            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                game = (IGameDescriptor)writer.Deserialize(fileStream);
            }

            
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public static void DeleteGame(string fileName)
        {
            var path = System.IO.Path.Combine(GameFolder, fileName);

            if (!File.Exists(path)) throw new ArgumentOutOfRangeException();

            File.Delete(path);
        }
    }
}
