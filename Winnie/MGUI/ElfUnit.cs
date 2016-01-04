using System;

namespace MGUI
{
	public class ElfUnit : Unit
	{
		public ElfUnit (int seed) : base(seed)
		{
			SetBody ();
			SetPant ();
		}

		protected void SetBody() {
			string[] skins = {"MaleCaucasian", "FemaleCaucasian"};
			body = skins[rnd.Next(0,2)];
		}

		protected void SetPant() {
			string[] pants = {"BlackPant", "BrownPant", "WhitePant", "YellowPant", "RedPant", "BluePant", "PurplePant", "GreenPant"};
			pant = pants [rnd.Next (0, 8)];
		}
	}
}

