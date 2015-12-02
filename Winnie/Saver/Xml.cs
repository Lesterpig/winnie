using System;
using System.Xml.Serialization;
using Core;

namespace Saver
{
    public class Xml
    {
        public static void GameToXml(Game g)
        {
            GameData game = new GameData(g);
        }

        public static Game XmlToGame()
        {
            return null;
        }
    }
}

