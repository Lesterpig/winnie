using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace winnie
{
    public class Action
    {

        public ActionType ActionType
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }

    public enum ActionType
    {
        Battle,
        Move
    }
}