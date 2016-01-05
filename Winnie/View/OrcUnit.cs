using System;

namespace MGUI
{
	public class OrcUnit : Unit
	{
		public OrcUnit(int seed) : base(seed) {}

		protected override void SetBody() {
			string[] skins = {"MaleOrc", "FemaleOrc"};
			body = skins[gender];
		}

		protected override void SetPant() {
			string[] pants = {"BrownPant", "BlackPant"};
			pant = pants [rnd.Next (0, 2)];
		}

		protected override void SetShirt() {
			string[] shirts = {"BrownShirt1", "BrownShirt2", "BrownShirt3"};
			shirt = shirts [rnd.Next (0, 3)];
		}

		protected override void SetHair() {
			string[] hairs = {"DarkHair1", "DarkHair2"};
			hair = hairs [gender];
		}

		protected override void SetWeapon() {
			weapon = "Mace1";
		}
	}
}

