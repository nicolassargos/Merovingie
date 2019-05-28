using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using AoC.Api.Domain;
using AoC.DataLayer;
using AoC.Common.Interfaces;
using AoC.Common.Descriptors;

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

            GameFileManagerStatic.SaveGame(gameDescriptor, fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveGame_ThrowArgumentNullException_IfFilenameIsNull()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = null;

            GameFileManagerStatic.SaveGame(gameDescriptor, fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveGame_ThrowArgumentNullException_IfFilenameIsEmpty()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "";

            GameFileManagerStatic.SaveGame(gameDescriptor, fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveGame_ThrowArgumentNullException_IfFilenameIsWhiteSpaces()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "   ";

            GameFileManagerStatic.SaveGame(gameDescriptor, fileName);
        }


        [TestMethod]
        public void SaveGame_CreatesFile_IfFilenameDoesntExists()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "qsdfghjklm123456789";

            var mockFileSystem = new MockFileSystem();
            GameFileManagerStatic.FileSystemDI = mockFileSystem;
            var mockInputFile = new MockFileData("line1\nline2\nline3");

            mockFileSystem.AddFile(Path.Combine(GameFileManagerStatic.GameFolder, fileName), mockInputFile);

            int nbFileOccurences_beforeSave = GameFileManagerStatic.GetNumberOfFileIterations(fileName);

            string newFilePath = GameFileManagerStatic.SaveGame(gameDescriptor, fileName);

            int nbFileOccurences_afterSave = GameFileManagerStatic.GetNumberOfFileIterations(fileName);

            Assert.AreEqual(nbFileOccurences_beforeSave + 1, nbFileOccurences_afterSave);

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

            GameFileManagerStatic.ReadGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadGame_ThrowArgumentNullException_IfFilenameIsEmpty()
        {
            string fileName = "";

            GameFileManagerStatic.ReadGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadGame_ThrowArgumentNullException_IfFilenameIsWhiteSpaces()
        {
            string fileName = "     ";

            GameFileManagerStatic.ReadGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ReadGame_ThrowFileNotFoundException_IfFileDoesntExist()
        {
            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "qsdfghjklm123456789.xml";

            var mockFileSystem = new MockFileSystem();
            GameFileManagerStatic.FileSystemDI = mockFileSystem;

            // Renvoie ~/qsdfghjklm123456789.xml
            //string newFilePath = GameFileManager.SaveGame(gameDescriptor, fileName);

            // Vérifie le fichier de nom ~/qsdfghjklm123456789
            Assert.ThrowsException<FileNotFoundException>(() => GameFileManagerStatic.ReadGame(fileName));
        }

        #endregion

        #region GetFiles

        [TestMethod]
        public void GetFiles_ReturnsListOfFiles_FromGameDirectory()
        {

            var mockFileSystem = new MockFileSystem();

            var mockInputFile = new MockFileData("line1\nline2\nline3");

            mockFileSystem.AddFile(Path.Combine(GameFileManagerStatic.GameFolder, "in1.xml"), mockInputFile);
            mockFileSystem.AddFile(Path.Combine(GameFileManagerStatic.GameFolder, "in2.xml"), mockInputFile);
            mockFileSystem.AddFile(Path.Combine(GameFileManagerStatic.GameFolder, "in3.xml"), mockInputFile);

            GameFileManagerStatic.FileSystemDI = mockFileSystem;

            var fileInfo = GameFileManagerStatic.GetFiles("*.xml");

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

            mockFileSystem.AddFile(Path.Combine(GameFileManagerStatic.GameFolder, "in1.xml"), mockInputFile);
            mockFileSystem.AddFile(Path.Combine(GameFileManagerStatic.GameFolder, "in2.xml"), mockInputFile);

            GameFileManagerStatic.FileSystemDI = mockFileSystem;

            var gameDetails = GameFileManagerStatic.GetGameFiles().ToList();

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

            GameFileManagerStatic.DeleteGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DeleteGame_ThrowsFormatException_IfFileNameDoesntExists()
        {
            string fileName = "opiuytreza";

            GameFileManagerStatic.DeleteGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void DeleteGame_ThrowsFileNotFoundException_IfFileNameDoesntExists()
        {
            string fileName = "opiuytreza.xml";

            GameFileManagerStatic.DeleteGame(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void DeleteGame_DeletesFile_Ok()
        {
            string fileName = "in1.xml";

            var mockFileSystem = new MockFileSystem();
            GameFileManagerStatic.FileSystemDI = mockFileSystem;
            var mockInputFile = new MockFileData("line1\nline2\nline3");

            mockFileSystem.AddFile(Path.Combine(GameFileManagerStatic.GameFolder, fileName), mockInputFile);
            mockFileSystem.AddFile(Path.Combine(GameFileManagerStatic.GameFolder, "in2.xml"), mockInputFile);

            GameFileManagerStatic.DeleteGame(fileName);

            var gameDetails = GameFileManagerStatic.GetGameFiles().ToList();

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
