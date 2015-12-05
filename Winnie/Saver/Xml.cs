using System;
using System.IO;
using System.Xml.Serialization;
using Core;

namespace Saver
{
    public class Xml
    {
        public static void GameToXml(Game g, Stream output)
        {
            GameData data = new GameData(g);
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            serializer.Serialize(output, data);
        }

        public static Game XmlToGame(Stream input)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            GameData data = (GameData) serializer.Deserialize(input);

            return data.Rebuild();
        }
    }
}

