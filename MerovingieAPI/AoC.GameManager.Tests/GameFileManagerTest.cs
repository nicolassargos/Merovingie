using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoC.MerovingieFileManager;
using Domain;
using System;
using System.IO;

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
        public void ReadGame_ThrowArgumentNullException_IfFilenameIsNull ()
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

            Assert.ThrowsException<FormatException>( () => GameFileManager.ReadGame("qsdfghjklm123456789.vcx"));
            
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

            // Renvoie ~/qsdfghjklm123456789.xml
            string newFilePath = GameFileManager.SaveGame(gameDescriptor, fileName);

            // Vérifie le fichier de nom ~/qsdfghjklm123456789.xml.xml
            Assert.ThrowsException<FileNotFoundException>(() => GameFileManager.ReadGame(newFilePath));

            CleanDirectory(newFilePath);
        }

        #endregion

        #region MyRegion

        #endregion

        private void CleanDirectory(string newFileName)
        {
            if (!string.IsNullOrWhiteSpace(newFileName) && File.Exists(newFileName)) File.Delete(newFileName);
        }
    }
}
