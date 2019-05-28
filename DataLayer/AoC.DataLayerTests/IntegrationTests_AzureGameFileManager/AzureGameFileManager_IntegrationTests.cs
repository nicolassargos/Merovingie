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
    public class AzureGameFileManager_IntegrationTests
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
        public void IntegrationTest_DeleteGame()
        {
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);

            var gameList0 = azGameFileManager.GetGameFiles().ToList();

            var descriptor = azGameFileManager.ReadGame("mygame.xml");
            var newfileInAzure = azGameFileManager.SaveGame(descriptor, "DeleteGameTest.xml");
            var gameList1 = azGameFileManager.GetGameFiles().ToList();

            azGameFileManager.DeleteGame("DeleteGameTest.xml");

            var gameList2 = azGameFileManager.GetGameFiles().ToList();

            Assert.IsFalse(gameList2.Any(x=> x.Name == "DeleteGameTest.xml"));
        }

        [TestMethod()]
        public void IntegrationTest_GetGameFiles()
        {
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);

            var gameList0 = azGameFileManager.GetGameFiles().ToList();

            Assert.IsTrue(gameList0.Count>0);//TODO ASSERT Enought?
        }

        [TestMethod]
        public void IntegrationTest_GetGameFiles_v2()
        {
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);


            //azGameFileManager.DeleteGame(gameDetails[0].Name);
            var descriptor = new GameDescriptor();
            var newfileInAzure = azGameFileManager.SaveGame(descriptor, "ListOfFilesTest.xml");

            var gameDetails0 = azGameFileManager.GetGameFiles().ToList();
            azGameFileManager.DeleteGame("ListOfFilesTest.xml");
            var gameDetails1 = azGameFileManager.GetGameFiles().ToList();

            Assert.IsTrue(gameDetails0.Any(x => x.Name == "ListOfFilesTest.xml"));
            Assert.IsFalse(gameDetails1.Any(x => x.Name == "ListOfFilesTest.xml"));

        }

        [TestMethod()]
        public void IntegrationTest_ReadGame()
        {
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);

            var descriptor = azGameFileManager.ReadGame("mygame.xml");

            Assert.IsTrue(descriptor.GoldMines.Count>0); //TODO ASSERT Enought?
        }

        [TestMethod()]
        public void IntegrationTest_SaveGame()
        {
            var mockConfiguration = GetMockConfiguration();
            var azGameFileManager = new AzureGameFileManager(mockConfiguration);

            var gameList0 = azGameFileManager.GetGameFiles().ToList();

            var descriptor = azGameFileManager.ReadGame("mygame.xml");
            var newfileInAzure = azGameFileManager.SaveGame(descriptor, "SaveGameTest.xml");
            var gameList1 = azGameFileManager.GetGameFiles().ToList();

            azGameFileManager.DeleteGame("SaveGameTestTest.xml");

            var gameList2 = azGameFileManager.GetGameFiles().ToList();

            Assert.IsTrue(gameList1.Any(x => x.Name == "SaveGameTest.xml"));//TODO ASSERT
        }

    }
}