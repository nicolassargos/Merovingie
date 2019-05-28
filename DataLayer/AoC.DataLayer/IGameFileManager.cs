using System.Collections.Generic;
using System.IO.Abstractions;
using AoC.Common.Descriptors;
using AoC.Common.Interfaces;
using Common.Dto;

namespace AoC.DataLayer
{
    public interface IGameFileManager
    {
        IEnumerable<GameDetailsDto> GetGameFiles();
        IGameDescriptor ReadGame(string fileName);
        string SaveGame(IGameDescriptor game, string fileName);
        void DeleteGame(string fileName);
    }
}