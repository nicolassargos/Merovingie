using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AoC.Map
{
    public class GameFileManager
    {
        public static void SaveGame(GameDescriptor game, string fileName)
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//" + fileName + ".xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, game);
            file.Close();
        }

        public static GameDescriptor ReadGame(string fileName)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//" + fileName + ".xml";

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(GameDescriptor));


            var game = (GameDescriptor)writer.Deserialize(new FileStream(path, FileMode.Open));

            return game;
        }
    }
}
