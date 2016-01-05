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

		public int UnitSeed { get; set; }
		public Core.Unit UnitModel { get; set; }

		public static int GeneratorSeed { get; set; }

		public static Unit New(Core.Unit u) {

			Unit gen; 
			if (u.Race is Core.Elf)
				gen = new ElfUnit ();
			else if (u.Race is Core.Human)
				gen = new HumanUnit ();
			else if (u.Race is Core.Orc)
				gen = new OrcUnit ();
			else
				return null;

			gen.UnitSeed = GeneratorSeed++;
			gen.UnitModel = u;
			
			return gen;
		}

		public Unit() {
			this.rnd = new Random (UnitSeed);
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

		public void Blit(SpriteBatch spriteBatch, Texture2D character, int squareSize) {
			string[] parts = {body,pant,shirt,hair,weapon};
			foreach (string part in parts) {
				spriteBatch.Draw(character, new Rectangle(UnitModel.Tile.Point.x*squareSize*3+squareSize, UnitModel.Tile.Point.y*squareSize*3+squareSize, squareSize, squareSize), UnitBinding.GetTexture(part), Color.White);
			}
		}
	}
}

