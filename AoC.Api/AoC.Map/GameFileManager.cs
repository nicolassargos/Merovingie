using AoC.Common.Descriptors;
using AoC.Common.Interfaces;
using AoC.DataLayer;

using Common.Dto;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace AoC.MerovingieFileManager
{
    public class GameFileManager : IGameFileManager
    {
        const string FOLDER_NAME = "MerovingieGames";
        const string GAMEFILE_EXTENSION = ".xml";
        const string GAMEFILE_EXTENSION_PATTERN = "*.xml";

        public IFileSystem fileSystemDI = new FileSystem();

        public string GameFolder
        {
            get
            {
                var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var _gameFolder = System.IO.Path.Combine(myDocs, FOLDER_NAME);
                if (!this.fileSystemDI.Directory.Exists(_gameFolder))
                {
                    try
                    {
                        this.fileSystemDI.Directory.CreateDirectory(_gameFolder);
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
        public string SaveGame(IGameDescriptor game, string fileName)
        {
            if (game == null) throw new ArgumentNullException("SaveGame: GameDescriptor cannot be null");
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("SaveGame: File name cannot be null");

            var extension = this.fileSystemDI.Path.GetExtension(fileName);
            if (extension != GAMEFILE_EXTENSION)
            {
                if (string.IsNullOrEmpty(extension)) fileName += GAMEFILE_EXTENSION;
                else throw new FormatException($"SaveGame: file extension {extension} is incorrect. Use xml");
            }

            // Initialise le Serializer
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));

            // Créé le chemin du fichier de sauvegarde
            string path = GetFullPath(fileName);

            try
            {
                //FileSystemDI.File.OpenWrite(path).Close();

                using (Stream file = fileSystemDI.FileStream.Create(path, FileMode.Create))
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
        public IGameDescriptor ReadGame(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("ReadGame: File name is empty");
            if (System.IO.Path.GetExtension(fileName) != GAMEFILE_EXTENSION)
                fileName += GAMEFILE_EXTENSION;

            //throw new FormatException("ReadGame: File name has no valid extension");

            IGameDescriptor game = null;
            string path = GetFullPath(fileName);

            if (!this.fileSystemDI.File.Exists(path)) throw new FileNotFoundException($"ReadGame: file {path} not found");

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));

            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    game = (IGameDescriptor)writer.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return game;
        }

        /// <summary>
        /// Get the list of all games
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GameDetailsDto> GetGameFiles()
        {
            var gameList = new List<GameDetailsDto>();
            var filesInfos = GetFiles(GAMEFILE_EXTENSION_PATTERN);

            foreach (var gamefile in filesInfos)
            {
                gameList.Add(new GameDetailsDto() { Name = gamefile.Name, Path = gamefile.Directory.ToString(), CreationDate = gamefile.CreationTime });
            }

            return gameList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteGame(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException($"DeleteGame: File name {fileName} is empty");
            if (fileName.Substring(fileName.Length - 4) != ".xml") throw new FormatException($"DeleteGame: File name {fileName} has no valid extension");

            var path = System.IO.Path.Combine(this.GameFolder, fileName);

            if (!this.fileSystemDI.File.Exists(path)) throw new FileNotFoundException($"DeleteGame: File {fileName} not found");

            this.fileSystemDI.File.Delete(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFullPath(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException($"GetFullPath: File name {fileName} is empty");
            if (string.IsNullOrEmpty(Path.GetExtension(fileName))) throw new ArgumentException($"GetFullPath: File name {fileName} does not have a valid extension");

            return System.IO.Path.Combine(this.GameFolder, fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetNewVersionOfFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("GetNewVersionOfFile: File name is empty");

            var path = this.GetFullPath(fileName);

            if (string.IsNullOrWhiteSpace(path)) throw new NullReferenceException("GetNewVersionOfFile: GetFullPath returned a null value");

            // Vérifie qu'un fichier portant le même nom n'existe pas déjà
            // Si oui, incrémente la version du fichier
            if (this.fileSystemDI.File.Exists(path))
            {
                var nbFileOccurences = this.fileSystemDI.Directory.GetFiles(this.GameFolder, fileName + "*.xml").Count();
                path = path.Insert(path.Length - 4, nbFileOccurences.ToString());
            }

            return path;
        }

        /// <summary> 
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int GetNumberOfFileIterations(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("GetNumberOfFileIterations: File name is empty");

            return this.fileSystemDI.Directory.GetFiles(this.GameFolder, fileName + GAMEFILE_EXTENSION_PATTERN).Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public IFileInfo[] GetFiles(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) throw new ArgumentNullException("GetFiles: Pattern is null or empty");

            return this.fileSystemDI.DirectoryInfo.FromDirectoryName(this.GameFolder).GetFiles(pattern);
        }
    }
}
