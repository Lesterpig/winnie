using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI
{
	public class UnitBinding
	{
		private static Dictionary<string, Rectangle> binding = new Dictionary<string, Rectangle>()
		{
			/**
			 * BODY
			 */
			{"MaleCaucasian", new Rectangle(0,0,128,128)},
			{"FemaleCaucasian", new Rectangle(136, 0, 128, 128)},

			{"MaleMixedRace", new Rectangle(0, 136, 128, 128)},
			{"FemaleMixedRace", new Rectangle(136, 136, 128, 128)},

			{"MaleBlack", new Rectangle(0,272,128,128)},
			{"FemaleBlack", new Rectangle(136, 272,128,128)},

			{"MaleOrc", new Rectangle(0,408,128,128)},
			{"FemaleOrc", new Rectangle(136,408,128,128)},

			/**
			 * PANTS
			 */
			{"BlackPant", new Rectangle(416, 0, 128, 128)},
			{"BrownPant", new Rectangle(416, 136, 128, 128)},
			{"WhitePant", new Rectangle(416, 136*2, 128, 128)},
			{"YellowPant", new Rectangle(416, 136*3, 128, 128)},
			{"RedPant", new Rectangle(416, 136*5, 128, 128)},
			{"BluePant", new Rectangle(416,136*6, 128, 128)},
			{"PurplePant", new Rectangle(416, 136*7, 128, 128)},
			{"GreenPant", new Rectangle(416, 136*8, 128, 128)},

			/**
			 * SHIRT 
			 */
			{"RedShirt1", new Rectangle(824, 0, 128, 128)},
			{"RedShirt2", new Rectangle(1096, 136, 128, 128)},
			{"RedShirt3", new Rectangle(1232, 136, 128, 128)},

			{"BlueShirt1", new Rectangle(1368, 0, 128, 128)},
			{"BlueShirt2", new Rectangle(1640, 136, 128, 128)},
			{"BlueShirt3", new Rectangle(1776, 136, 128, 128)},

			{"BrownShirt1", new Rectangle(1368, 680, 128, 128)},
			{"BrownShirt2", new Rectangle(1640, 816, 128, 128)},
			{"BrownShirt3", new Rectangle(1776, 816, 128, 128)},

			/**
			 * HAIR
			 */
			{"BlondHair1", new Rectangle(2592, 680, 128, 128)},
			{"BlondHair2", new Rectangle(2864, 680, 128, 128)},
			{"DarkHair1", new Rectangle(3136, 680, 128, 128)},
			{"DarkHair2", new Rectangle(3406, 680, 128, 128)},
			{"BrownHair1", new Rectangle(2592, 136, 128, 128)},
			{"BrownHair2", new Rectangle(2864, 136, 128, 128)},


			/**
			 * WEAPON
			 */
			{"Bow1", new Rectangle(7216,0,128,128)},
			{"Mace1", new Rectangle(6536, 0, 128, 128)},
			{"Stick1", new Rectangle(5856, 272, 128, 128)},

			/**
			 * SHOES
			 */

			/**
			 * HAT
			 */

		};

		private UnitBinding ()
		{
		}


		public static Rectangle GetTexture(string name) {
			Rectangle res;
			if (!binding.TryGetValue (name, out res))
				res = new Rectangle (0, 0, 0, 0);

			return res;

		}
	}
}

