using Common.Dto;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AoC.MerovingieFileManager
{
    public static class GameFileManager
    {
        const string FOLDER_NAME = "MerovingieGames";
        const string GAMEFILE_EXTENSION = ".xml";
        const string GAMEFILE_EXTENSION_PATTERN = "*.xml";

        public static IFileSystem FileSystemDI = new FileSystem();

        public static string GameFolder { get { 
                var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var _gameFolder = System.IO.Path.Combine(myDocs, FOLDER_NAME);
                if (!FileSystemDI.Directory.Exists(_gameFolder))
                {
                    try
                    {
                        FileSystemDI.Directory.CreateDirectory(_gameFolder);
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("GameFolder: Unable to create the directory of game files. Check your write access?", ex);
                    }
                }
                return _gameFolder;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="fileName"></param>
        public static string SaveGame(IGameDescriptor game, string fileName)
        {
            if (game == null) throw new ArgumentNullException("GameDescriptor cannot be null");
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("File name cannot be null");

            // Initialise le Serializer
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));

            // Créé le chemin du fichier de sauvegarde
            string path = GetNewVersionOfFile(fileName);

            try
            {
                using (Stream file = FileSystemDI.FileStream.Create(path, FileMode.CreateNew))
                {
                    writer.Serialize(file, game);
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                throw new IOException("SaveGame: Unable to save the game file. Check your write access?", ex);
            }

            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static IGameDescriptor ReadGame(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("ReadGame: File name is empty");
            if (fileName.Substring(fileName.Length-4) != ".xml") throw new FormatException("ReadGame: File name has no valid extension");

            IGameDescriptor game = null;
            string path = GetFullPath(fileName);

            if (FileSystemDI.File.Exists(path)) throw new FileNotFoundException($"ReadGame: file {path} not found");

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));

            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    game = (IGameDescriptor)writer.Deserialize(fileStream);
                }
            }
            catch
            {

                throw;
            }
            
            return game;
        }

        /// <summary>
        /// Get the list of all games
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GameDetailsDto> GetGameFiles()
        {
            var gameList = new List<GameDetailsDto>();
            var filesInfos = GetFiles(GAMEFILE_EXTENSION_PATTERN);

            foreach (var gamefile in filesInfos)
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
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException($"DeleteGame: File name {fileName} is empty");
            if (fileName.Substring(fileName.Length - 4) != ".xml") throw new FormatException($"DeleteGame: File name {fileName} has no valid extension");

            var path = System.IO.Path.Combine(GameFolder, fileName);

            if (!FileSystemDI.File.Exists(path)) throw new FileNotFoundException($"DeleteGame: File {fileName} not found");

            FileSystemDI.File.Delete(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullPath(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException($"GetFullPath: File name {fileName} is empty");

            return System.IO.Path.Combine(GameFolder, fileName + GAMEFILE_EXTENSION);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetNewVersionOfFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("GetNewVersionOfFile: File name is empty");

            var path = GetFullPath(fileName);

            if (string.IsNullOrWhiteSpace(path)) throw new  NullReferenceException("GetNewVersionOfFile: GetFullPath returned a null value");

            // Vérifie qu'un fichier portant le même nom n'existe pas déjà
            // Si oui, incrémente la version du fichier
            if (FileSystemDI.File.Exists(path))
            {
                var nbFileOccurences = Directory.GetFiles(GameFolder, fileName + "*.xml").Count();
                path = path.Insert(path.Length - 4, nbFileOccurences.ToString());
            }

            return path;
        }

        /// <summary> 
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int GetNumberOfFileIterations(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("GetNumberOfFileIterations: File name is empty");

            return Directory.GetFiles(GameFolder, fileName + GAMEFILE_EXTENSION_PATTERN).Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static IFileInfo[] GetFiles(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) throw new ArgumentNullException("GetFiles: Pattern is null or empty");

            return FileSystemDI.DirectoryInfo.FromDirectoryName(GameFolder).GetFiles(pattern);
        }
    }
}
