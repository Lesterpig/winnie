﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace winnie
{
    public abstract class Race
    {
        protected int life;
        protected int armor;
        protected int attack;

        public float getMovePoints()
        {
            throw new System.NotImplementedException();
        }
    }
}