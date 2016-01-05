using System;
using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI
{
	public abstract class Unit
	{
		protected int gender;
		protected Random rnd;
		protected string body;
		protected string pant;
		protected string shoes;
		protected string shirt;
		protected string hair;
		protected string hat;
		protected string shield;
		protected string weapon;

		public static int Seed { get; set; }

		public static Unit New(Core.Unit u) {
			Seed++;
			if (u.Race is Core.Elf)
				return new ElfUnit (Seed);
			if (u.Race is Core.Human)
				return new HumanUnit (Seed);
			if (u.Race is Core.Orc) {
				return new OrcUnit (Seed);
			}
			return null;
		}

		public Unit(int seed) {
			this.rnd = new Random (seed);
			generateCharacter ();
		}

		public void generateCharacter() {
			gender = rnd.Next (0,2);
			SetBody ();
			SetPant ();
			SetShirt ();
			SetHair ();
			SetWeapon ();
		}

		protected abstract void SetBody();
		protected abstract void SetPant();
		protected abstract void SetShirt();
		protected abstract void SetHair ();
		protected abstract void SetWeapon ();

		public void Blit(SpriteBatch spriteBatch, Texture2D character, int squareSize, int x, int y) {
			string[] parts = {body,pant,shirt,hair,weapon};
			foreach (string part in parts) {
				spriteBatch.Draw(character, new Rectangle(x,y,squareSize,squareSize), UnitBinding.GetTexture(part), Color.White);
			}
		}
	}
}

