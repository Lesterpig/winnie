﻿using System;
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
			 * NUDE BODY
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
			{"BlackPant", new Rectangle(440, 104, 80, 24)},

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
