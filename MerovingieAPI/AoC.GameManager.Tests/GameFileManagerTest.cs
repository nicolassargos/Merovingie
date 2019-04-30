using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using AoC.Api.Domain;

namespace AoC.MerovingieFileManager.Tests
{
    [TestClass]
    public class GameFileManagerTest
    {
        [TestInitialize]
        public void Init()
        {
        }

        #region SaveGame

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveGame_ThrowArgumentNullException_IfGameIsNull()
        {
            IGameDescriptor gameDescriptor = null;
            string fileName = "newGame";

            GameFileManager.SaveGame(gameDescriptor, fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveGame_ThrowArgumentNullException_IfFilenameIsNull()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = null;

            GameFileManager.SaveGame(gameDescriptor, fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveGame_ThrowArgumentNullException_IfFilenameIsEmpty()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "";

            GameFileManager.SaveGame(gameDescriptor, fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveGame_ThrowArgumentNullException_IfFilenameIsWhiteSpaces()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "   ";

            GameFileManager.SaveGame(gameDescriptor, fileName);
        }


        [TestMethod]
        public void SaveGame_CreatesFile_IfFilenameDoesntExists()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "qsdfghjklm123456789";

            // Checks if "newGame.xml" already exists
            int nbFileOccurences_beforeSave = GameFileManager.GetNumberOfFileIterations(fileName);

            string newFilePath = GameFileManager.SaveGame(gameDescriptor, fileName);

            int nbFileOccurences_afterSave = GameFileManager.GetNumberOfFileIterations(fileName);

            Assert.AreEqual(nbFileOccurences_beforeSave + 1, nbFileOccurences_afterSave);

            CleanDirectory(newFilePath);
        }

        [TestMethod]
        public void SaveGame_CreatesNewFile_IfFilenameAlreadyExists()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "newGame";

            // Checks if "newGame.xml" already exists
            int nbFileOccurences_beforeSave = GameFileManager.GetNumberOfFileIterations(fileName);

            string newFilePath = GameFileManager.SaveGame(gameDescriptor, fileName);

            int nbFileOccurences_afterSave = GameFileManager.GetNumberOfFileIterations(fileName);

            //
            Assert.AreEqual(nbFileOccurences_beforeSave + 1, nbFileOccurences_afterSave);

            CleanDirectory(newFilePath);
        }

        #endregion

        #region ReadGame
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadGame_ThrowArgumentNullException_IfFilenameIsNull()
        {
            string fileName = null;

            GameFileManager.ReadGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadGame_ThrowArgumentNullException_IfFilenameIsEmpty()
        {
            string fileName = "";

            GameFileManager.ReadGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadGame_ThrowArgumentNullException_IfFilenameIsWhiteSpaces()
        {
            string fileName = "     ";

            GameFileManager.ReadGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ReadGame_ThrowFormatException_IfFilenameIsNotXML()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "qsdfghjklm123456789";

            //
            string newFilePath = GameFileManager.SaveGame(gameDescriptor, fileName);

            Assert.ThrowsException<FormatException>(() => GameFileManager.ReadGame("qsdfghjklm123456789.vcx"));

            CleanDirectory(newFilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ReadGame_ThrowFormatException_IfFilenameIsMissingExtension()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "qsdfghjklm123456789";

            //
            string newFilePath = GameFileManager.SaveGame(gameDescriptor, fileName);

            Assert.ThrowsException<FormatException>(() => GameFileManager.ReadGame("qsdfghjklm123456789"));

            CleanDirectory(newFilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ReadGame_ThrowFileNotFoundException_IfFileDoesntExist()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "qsdfghjklm123456789";

            var mockFileSystem = new MockFileSystem();
            GameFileManager.FileSystemDI = mockFileSystem;

            // Renvoie ~/qsdfghjklm123456789.xml
            string newFilePath = GameFileManager.SaveGame(gameDescriptor, fileName);

            // Vérifie le fichier de nom ~/qsdfghjklm123456789.xml.xml
            Assert.ThrowsException<FileNotFoundException>(() => GameFileManager.ReadGame(newFilePath));

            CleanDirectory(newFilePath);
        }

        #endregion

        #region GetFiles

        [TestMethod]
        public void GetFiles_ReturnsListOfFiles_FromGameDirectory()
        {

            var mockFileSystem = new MockFileSystem();

            var mockInputFile = new MockFileData("line1\nline2\nline3");

            mockFileSystem.AddFile(Path.Combine(GameFileManager.GameFolder, "in1.xml"), mockInputFile);
            mockFileSystem.AddFile(Path.Combine(GameFileManager.GameFolder, "in2.xml"), mockInputFile);
            mockFileSystem.AddFile(Path.Combine(GameFileManager.GameFolder, "in3.xml"), mockInputFile);

            GameFileManager.FileSystemDI = mockFileSystem;

            var fileInfo = GameFileManager.GetFiles("*.xml");

            Assert.IsNotNull(fileInfo);
            Assert.AreEqual(3, fileInfo.Length);
        }

        #endregion

        #region GetGameFiles

        [TestMethod]
        public void GetGameFiles_ReturnsListOfFiles_Ok()
        {

            var mockFileSystem = new MockFileSystem();

            var mockInputFile = new MockFileData("line1\nline2\nline3");

            mockFileSystem.AddFile(Path.Combine(GameFileManager.GameFolder, "in1.xml"), mockInputFile);
            mockFileSystem.AddFile(Path.Combine(GameFileManager.GameFolder, "in2.xml"), mockInputFile);

            GameFileManager.FileSystemDI = mockFileSystem;

            var gameDetails = GameFileManager.GetGameFiles().ToList();

            Assert.IsNotNull(gameDetails);
            Assert.AreEqual(2, gameDetails.Count);
        }

        #endregion

        #region DeleteGame

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteGame_ThrowArgumentNullException_IfFilenameIsNull()
        {
            string fileName = null;

            GameFileManager.DeleteGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DeleteGame_ThrowsFormatException_IfFileNameDoesntExists()
        {
            string fileName = "opiuytreza";

            GameFileManager.DeleteGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void DeleteGame_ThrowsFileNotFoundException_IfFileNameDoesntExists()
        {
            string fileName = "opiuytreza.xml";

            GameFileManager.DeleteGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void DeleteGame_DeletesFile_Ok()
        {
            string fileName = "in1.xml";

            var mockFileSystem = new MockFileSystem();
            GameFileManager.FileSystemDI = mockFileSystem;
            var mockInputFile = new MockFileData("line1\nline2\nline3");

            mockFileSystem.AddFile(Path.Combine(GameFileManager.GameFolder, fileName), mockInputFile);
            mockFileSystem.AddFile(Path.Combine(GameFileManager.GameFolder, "in2.xml"), mockInputFile);

            GameFileManager.DeleteGame(fileName);

            var gameDetails = GameFileManager.GetGameFiles().ToList();

            Assert.IsNotNull(gameDetails);
            Assert.AreEqual(1, gameDetails.Count);
        }

        #endregion



        private void CleanDirectory(string newFileName)
        {
            if (!string.IsNullOrWhiteSpace(newFileName) && File.Exists(newFileName)) File.Delete(newFileName);
        }
    }
}
