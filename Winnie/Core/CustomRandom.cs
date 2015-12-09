using System;

namespace Core
{   
    /// <summary>
    /// A wrapper around Core.Random to ignore random when testing the library.
    /// </summary>
    public class CustomRandom
    {

        /// <summary>
        /// Random mode.
        /// Normal: just the default system random.
        /// High: always return highest value.
        /// Low: always return lowest value + 1.
        /// </summary>
        public enum Mode { NORMAL, HIGH, LOW }

        private Mode _mode;
        private System.Random _random;

        /// <summary>
        /// Initializes a new instance of the <see cref="Core.CustomRandom"/> class.
        /// </summary>
        /// <param name="m">The random mode.</param>
        public CustomRandom(Mode m)
        {
            this._mode = m;
            this._random = new System.Random();
        }

        /// <summary>
        /// Get a random int in the interval [a,b[
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">b.</param>
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

