using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoC.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using AoC.Common.Interfaces;
using AoC.Common.Descriptors;

namespace AoC.DataLayer.Tests
{
    [TestClass()]
    public class AzureGameFileManager_SaveGame_IntegrationTests
    {
        private IConfiguration GetMockConfiguration()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            //TEMPLATE mockConfiguration.SetupGet(p => p[It.IsAny<string>()]).Returns("foo");
            mockConfiguration.SetupGet(p => p["BlobStorage:Account"]).Returns("merovingiestorage");
            mockConfiguration.SetupGet(p => p["BlobStorage:Key"]).Returns("cDrV04V65aWqRPrkC+Eu71OqdnqPZx2wNNRQMRr/5vEanHHRpi0AJIJ9Did5cw7jnjoAiVGDnvpLPShp1+z4Sg==");
            mockConfiguration.SetupGet(p => p["BlobStorage:StorageConnectionString"]).Returns("DefaultEndpointsProtocol=https;AccountName=merovingiestorage;AccountKey=cDrV04V65aWqRPrkC+Eu71OqdnqPZx2wNNRQMRr/5vEanHHRpi0AJIJ9Did5cw7jnjoAiVGDnvpLPShp1+z4Sg==;EndpointSuffix=core.windows.net");
            mockConfiguration.SetupGet(p => p["BlobStorage:StorageUrl"]).Returns("https://merovingiestorage.blob.core.windows.net/merovingiefiles");
            mockConfiguration.SetupGet(p => p["BlobStorage:ContainerName"]).Returns("merovingiefiles");

            return mockConfiguration.Object;
        }

        [TestMethod()]
        public void azSaveGame_ReturnAzureFileList_WhenCorrectValuesIsPRovided()
        {
            //Arrange
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);
            //Act

            //Assert
        }

        [TestMethod]
        public void azSaveGame_ThrowArgumentNullException_IfGameIsNull()
        {
            //Arrange
            var mockConfiguration = GetMockConfiguration();

            var azGameFileManager = new AzureGameFileManager(mockConfiguration);
            IGameDescriptor gameDescriptor = null;
            string fileName = "newGame";

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => azGameFileManager.SaveGame(gameDescriptor, fileName));
        }

        [TestMethod]
        public void azSaveGame_ThrowArgumentNullException_IfFilenameIsNull()
        {
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);

            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = null;

            Assert.ThrowsException<ArgumentNullException>(() => azGameFileManager.SaveGame(gameDescriptor, fileName));
        }

        [TestMethod]
        public void azSaveGame_ThrowArgumentNullException_IfFilenameIsEmpty()
        {
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);

            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "";

            Assert.ThrowsException<ArgumentNullException>(() => azGameFileManager.SaveGame(gameDescriptor, fileName));
        }

        [TestMethod]
        public void azSaveGame_ThrowArgumentNullException_IfFilenameIsWhiteSpaces()
        {
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);

            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "   ";

            Assert.ThrowsException<ArgumentNullException>(() => azGameFileManager.SaveGame(gameDescriptor, fileName));
        }

        [TestMethod]
        public void azSaveGame_CreatesFile_IfFilenameDoesntExists()
        {
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);

            IGameDescriptor gameDescriptor = new GameDescriptor();
            string fileName = "qsdfghjklm123456789.xml";

            //var mockFileSystem = new MockFileSystem();
            //GameFileManagerStatic.FileSystemDI = mockFileSystem;
            //var mockInputFile = new MockFileData("line1\nline2\nline3");

            //mockFileSystem.AddFile(Path.Combine(GameFileManagerStatic.GameFolder, fileName), mockInputFile);

            //int nbFileOccurences_beforeSave = GameFileManagerStatic.GetNumberOfFileIterations(fileName);

            string newFilePath = azGameFileManager.SaveGame(gameDescriptor, fileName);

            //int nbFileOccurences_afterSave = GameFileManagerStatic.GetNumberOfFileIterations(fileName);

            Assert.IsTrue(newFilePath.Length > 1);

        }
    }
}