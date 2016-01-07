using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGUI
{
    class HudShow : Blittable
    {

        // Constants
        static Color COLOR_CURRENT_PLAYER = Color.White;
        static Color COLOR_WAITING_PLAYER = new Color(220, 220, 220);

        static float NOTIFICATION_DURATION = 5;
        static float NOTIFICATION_FADE_START = 4;

        Game1 game;
        int sizeX;
        int sizeY;

        // Cache values
        IList<int> scores;

        public HudShow(Game1 g)
        {
            game = g;
            scores = new List<int>();
            RefreshDataCache();
            RefreshWindowBounds();
            Notification = "";
        }

        float NotificationDuration = 0;
        private string _notification;
        public string Notification
        {
            get { return _notification; }
            set { _notification = value; if (value != "") { NotificationDuration = NOTIFICATION_DURATION; } }
        }

        public void UpdateTime(float qty)
        {
            if (NotificationDuration > 0)
            {
                NotificationDuration -= qty;
                if (NotificationDuration <= 0)
                {
                    Notification = "";
                }
            }
        }

        /// <summary>
        /// Refresh local cache for costy calls
        /// </summary>
        public void RefreshDataCache()
        {
            scores.Clear();
            foreach(Player p in game.GameModel.Players)
            {
                scores.Add(p.Score);
            }
        }

        public void RefreshWindowBounds()
        {
            var bounds = game.GraphicsDevice.Viewport.Bounds;
            sizeX = bounds.Right;
            sizeY = bounds.Bottom;
        }

        public void Blit()
        {
            RefreshWindowBounds();
            drawScores();
            drawRounds();
            drawNotification();
        }

        private void drawScores()
        {
            for (int i = 0, y = 10; i < game.GameModel.Players.Length; i++, y += 40)
            {
                bool playing = game.GameModel.CurrentPlayerIndex == i;
                drawString(
                    game.GameModel.Players[i].Name + " : " + scores[i],
                    10,
                    y,
                    playing ? 1 : 0.7f,
                    0,
                    playing ? COLOR_CURRENT_PLAYER : COLOR_WAITING_PLAYER
                );
            }
        }

        private void drawRounds()
        {
            string text = "Round " + (game.GameModel.CurrentTurn + 1) + "/" + game.GameModel.Turns;
            drawString(text, sizeX - 170, 10, 0.8f, 0, Color.White);
        }

        private void drawNotification()
        {
            float textSize = game.MainFont.MeasureString(Notification).X;
            float alpha = (NOTIFICATION_DURATION - NotificationDuration) < NOTIFICATION_FADE_START
                        ? 1
                        : NotificationDuration / (NOTIFICATION_DURATION - NOTIFICATION_FADE_START);
            drawString(Notification, sizeX - textSize - 10, 50, 1, 0, new Color(Color.Blue, alpha));
        }

        private void drawString(string text, float x, float y, float scale, float rotation, Color color)
        {
            Vector2 position = new Vector2(x, y);
            Vector2 origin = new Vector2(0, 0);
            game.HUDBatch.DrawString(game.MainFont, text, position, color, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}
