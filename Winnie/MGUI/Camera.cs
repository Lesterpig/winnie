using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MGUI
{
	public class Camera : Camera2D
	{
		public int CameraAcceleration { get; set; }

		public Camera(GraphicsDevice g) : base(g) { CameraAcceleration = 600; } 

		public void MoveUp(float delta) {
			Move(new Vector2(0, -delta * CameraAcceleration * 1/Zoom));
		}
		public void MoveDown(float delta) {
			Move(new Vector2(0, delta * CameraAcceleration * 1/Zoom));
		}

		public void MoveLeft(float delta) {
			Move(new Vector2(-delta * CameraAcceleration * 1/Zoom, 0));
		}

		public void MoveRight(float delta) {
			Move (new Vector2 (delta * CameraAcceleration * 1/Zoom, 0));
		}
	}
}

