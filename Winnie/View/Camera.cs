using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MGUI
{
    public class Camera : Camera2D
    {
        GraphicsDevice graphicsDevice;

        public Camera(GraphicsDevice g) : base(g)
        {
            graphicsDevice = g;
        }

        public void Adjust(Vector2 point, Vector2 diff, Vector2 move)
        {
            Vector2 max = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            Vector2 current = WorldToScreen(point);
            if (max.Y < current.Y + diff.Y)
            {
                Move(new Vector2(0, move.Y));
            }
            else if (0 > current.Y)
            {
                Move(new Vector2(0, -move.Y));
            }

            if (max.X < current.X + diff.X)
            {
                Move(new Vector2(move.X, 0));
            }
            else if (0 > current.X)
            {
                Move(new Vector2(-move.X, 0));
            }
        }
    }
}

