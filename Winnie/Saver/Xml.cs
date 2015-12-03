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
            GameData game = new GameData(g);
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            serializer.Serialize(output, game);
        }

        public static Game XmlToGame()
        {
            return null;
        }
    }
}

