using AoC.Api.Domain;
using AoC.Common.Descriptors;
using AoC.Common.Interfaces;
using Common.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace AoC.DataLayer
{
    public static class AzureGameFileManagerStatic
    {
        public static IConfiguration Config { get; set; }

        public static IEnumerable<GameDetailsDto> GetGameFiles()
        {
            var azFileManager = new AzureGameFileManager(Config);
            return azFileManager.GetGameFiles();
        }
        public static IGameDescriptor ReadGame(string fileName)
        {
            var azFileManager = new AzureGameFileManager(Config);
            return azFileManager.ReadGame(fileName);
        }
        public static string SaveGame(IGameDescriptor game, string fileName)
        {
            var azFileManager = new AzureGameFileManager(Config);
            return azFileManager.SaveGame(game, fileName);
        }
        public static void DeleteGame(string fileName)
        {
            var azFileManager = new AzureGameFileManager(Config);
            azFileManager.DeleteGame(fileName);
        }
    }
}
