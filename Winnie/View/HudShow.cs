using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MGUI
{
    class HudShow : Blittable
    {

        // Constants
        static Color COLOR_CURRENT_PLAYER = Color.White;
        static Color COLOR_WAITING_PLAYER = new Color(220, 220, 220);

        static float NOTIFICATION_DURATION = 5;
        static float NOTIFICATION_FADE_START = 4;

        static float EVENT_DURATION = 4;
        static float EVENT_FADEIN_DURATION = 0.5f;
        static float EVENT_FADEOUT_DURATION = 1f;
        static float EVENT_SHIFT_Y = 20;

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
            Event = g.GameModel.CurrentPlayer.Name + "'s turn!";
        }

        float NotificationDuration = 0;
        private string _notification;
        public string Notification
        {
            get { return _notification; }
            set { _notification = value; if (value != "") { NotificationDuration = NOTIFICATION_DURATION; } }
        }

        float EventDuration = 0;
        private string _event;
        public string Event
        {
            get { return _event; }
            set { _event = value; if (value != "") { EventDuration = EVENT_DURATION; } }
        }

        /// <summary>
        /// Update text timeouts for fades
        /// </summary>
        /// <param name="qty"></param>
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
            if (EventDuration > 0)
            {
                EventDuration -= qty;
                if (EventDuration <= 0)
                {
                    Event = "";
                }
            }
        }

        /// <summary>
        /// Refresh local cache for costy calls
        /// </summary>
        public void RefreshDataCache()
        {
            scores.Clear();
            foreach (Player p in game.GameModel.Players)
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
            blitScores();

            if (!gameEnded)
            {
                blitRounds();
                if (Notification != "") blitNotification();
                if (Event != "") blitEvent();
                if (game.SelectedUnit != null) blitUnitInfo();
            }
            else
            {
                blitEndInfo();
            }
        }

        void blitScores()
        {
            for (int i = 0, y = 10; i < game.GameModel.Players.Length; i++, y += 40)
            {
                bool playing = game.GameModel.CurrentPlayerIndex == i || gameEnded;
                blitString(
                    game.GameModel.Players[i].Name + " : " + scores[i],
                    10,
                    y,
                    playing ? 1 : 0.7f,
                    0,
                    playing ? COLOR_CURRENT_PLAYER : COLOR_WAITING_PLAYER
                );
            }
        }

        void blitRounds()
        {
            string text = "Round " + (game.GameModel.CurrentTurn + 1) + "/" + game.GameModel.Turns;
            blitString(text, sizeX - 170, 10, 0.8f, 0, Color.White);
        }

        void blitNotification()
        {
            float textSize = game.MainFont.MeasureString(Notification).X;
            float alpha = (NOTIFICATION_DURATION - NotificationDuration) < NOTIFICATION_FADE_START
                        ? 1
                        : NotificationDuration / (NOTIFICATION_DURATION - NOTIFICATION_FADE_START);
            blitString(Notification, sizeX - textSize - 10, 50, 1, 0, Color.Blue * alpha);
        }

        void blitEvent()
        {
            Vector2 textSize = game.HugeFont.MeasureString(Event);
            float x = (sizeX - textSize.X) / 2;
            float y = (sizeY - textSize.Y) / 2;
            float alpha = 1;
            float shift = 0;
            if (EVENT_DURATION - EVENT_FADEIN_DURATION < EventDuration)
            {
                alpha = (EVENT_DURATION - EventDuration) / EVENT_FADEIN_DURATION;
                shift = (alpha - 1) * EVENT_SHIFT_Y; // from top
            }
            else if (EVENT_FADEOUT_DURATION > EventDuration)
            {
                alpha = EventDuration / EVENT_FADEOUT_DURATION;
                shift = (1 - alpha) * EVENT_SHIFT_Y; // to bottom
            }
            blitString(game.HugeFont, Event, x, y + shift, 1, 0, Color.White * alpha);
        }

        void blitUnitInfo()
        {
            int h = 58;

            game.HudBatch.Draw(
                game.HudElements,
                new Rectangle(10, sizeY - h * 5, 48, 48),
                HudBinding.GetTexture("Heart"),
                Color.White
            );
            game.HudBatch.Draw(
                game.HudElements,
                new Rectangle(10, sizeY - h * 4, 48, 48),
                HudBinding.GetTexture("Sword"),
                Color.White
            );
            game.HudBatch.Draw(
                game.HudElements,
                new Rectangle(10, sizeY - h * 3, 48, 48),
                HudBinding.GetTexture("Shield"),
                Color.White
            );
            game.HudBatch.Draw(
                game.HudElements,
                new Rectangle(10, sizeY - h * 2, 48, 48),
                HudBinding.GetTexture("Arrow"),
                Color.White
            );

            blitString(game.SelectedUnit.Life + "/" + game.SelectedUnit.Race.Life, 70, sizeY - h * 5, 1, 0, Color.White);
            blitString(game.SelectedUnit.AttackPoints.ToString(), 70, sizeY - h * 4, 1, 0, Color.White);
            blitString(game.SelectedUnit.DefensePoints.ToString(), 70, sizeY - h * 3, 1, 0, Color.White);
            blitString(game.SelectedUnit.MovePoints.ToString("G2"), 70, sizeY - h * 2, 1, 0, Color.White);
        }

        void blitEndInfo()
        {
            string text = "Draw!";
            Player winner = game.GameModel.Winner;
            if (winner != null)
            {
                text = winner.Name + " won!";
            }

            Vector2 textSize = game.HugeFont.MeasureString(text);
            float x = (sizeX - textSize.X) / 2;
            float y = (sizeY - textSize.Y) / 2;
            blitString(game.HugeFont, text, x, y, 1, 0, Color.White);

            text = "Thank you for playing!";
            textSize = game.MainFont.MeasureString(text);
            x = (sizeX - textSize.X) / 2;
            y += 100;
            blitString(game.MainFont, text, x, y, 1, 0, Color.White);
        }

        void blitString(string text, float x, float y, float scale, float rotation, Color color)
        {
            blitString(game.MainFont, text, x, y, scale, rotation, color);
        }

        void blitString(SpriteFont font, string text, float x, float y, float scale, float rotation, Color color)
        {
            Vector2 position = new Vector2(x, y);
            Vector2 origin = new Vector2(0, 0);
            game.HudBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0);
        }

        bool gameEnded
        {
            get
            {
                return game.GameModel.Winner != null || game.GameModel.CurrentTurn >= game.GameModel.Turns;
            }
        }
    }
}
