using AoC.Common.Descriptors;
using AoC.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.DataLayer
{
    public static class GameFileManagerTools
    {
        const string GAMEFILE_EXTENSION = ".xml";

        public static bool IsGameFileNameValidWithThrow(this string FileName)
        {
            if (string.IsNullOrWhiteSpace(FileName))
                throw new ArgumentNullException("SaveGame: File name cannot be null");

            var extension = (new FileSystem()).Path.GetExtension(FileName);
            if (extension != GAMEFILE_EXTENSION)
            {
                if (string.IsNullOrEmpty(extension)) FileName += GAMEFILE_EXTENSION;
                else throw new FormatException($"SaveGame: file extension {extension} is incorrect. Use xml");
            }
            return true;
        }
        public static bool IsGameDescriptorValidWithThrow(this IGameDescriptor game)
            => (game != null) ? true : throw new ArgumentNullException("SaveGame: GameDescriptor cannot be null");

        public static Stream GetGameDescriptorStream(this IGameDescriptor game)
        {
            Stream ReturnValue = new MemoryStream(); // MemoryStream();

            // Initialise le Serializer
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));

            try
            {
                    writer.Serialize(ReturnValue, game);
                    ReturnValue.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                throw new Exception("GetGameDescriptorStream: Unable to 'Serialize' the game file.", ex);
            }

            return ReturnValue;
        }

        public static IGameDescriptor GetGameDescriptor(this Stream gameStream)
        {
            gameStream.Seek(0, SeekOrigin.Begin);

            IGameDescriptor ReturnValue = new GameDescriptor();

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));

            try
            {
                ReturnValue = (GameDescriptor)writer.Deserialize(gameStream);
            }
            catch (Exception ex)
            {

                throw new Exception("GetGameDescriptorStream: Unable to 'Deserialize' the game file.", ex);
            }

            return ReturnValue;
        }

    }
}
