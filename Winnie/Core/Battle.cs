using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
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

        public new void Execute()
        {
            throw new System.NotImplementedException();
        }

        public new Battle ReverseExecute()
        {
            throw new System.NotImplementedException();
        }
    }
}