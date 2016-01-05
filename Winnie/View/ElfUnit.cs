using System;

namespace MGUI
{
	public class ElfUnit : Unit
	{
		public ElfUnit(int seed) : base(seed) {}

		protected override void SetBody() {
			string[] skins = {"MaleCaucasian", "FemaleCaucasian"};
			body = skins[gender];
		}

		protected override void SetPant() {
			string[] pants = {"BluePant", "GreenPant"};
			pant = pants [rnd.Next (0, 2)];
		}

		protected override void SetShirt() {
			string[] shirts = {"BlueShirt1", "BlueShirt2", "BlueShirt3"};
			shirt = shirts [rnd.Next (0, 3)];
		}

		protected override void SetHair() {
			string[] hairs = {"BlondHair1", "BlondHair2"};
			hair = hairs [gender];
		}

		protected override void SetWeapon() {
			weapon = "Bow1";
		}
	}
}

