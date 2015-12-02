using System;

namespace Core
{   

    /* A wrapper around Core.Random to ignore random when testing the library */
    public class CustomRandom
    {
    
        public enum Mode { NORMAL, HIGH, LOW }

        private Mode _mode;
        private System.Random _random;

        public CustomRandom(Mode m)
        {
            this._mode = m;
            this._random = new System.Random();
        }

        public int Next(int a, int b)
        {
            switch (this._mode)
            {
                case Mode.HIGH:
                    return b - 1;
                case Mode.LOW:
                    return a + 1;
                default:
                    return this._random.Next(a, b);
            }
        }
    }
}

