using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace winnie
{
    public class Battle : Action
    {
        public Unit Target
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Unit Winner
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int LifeLost
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public Battle ReverseExecute()
        {
            throw new System.NotImplementedException();
        }
    }
}