using System;

namespace MatrixHelper
{
    public delegate void MatrixChangedEventHandler(object sender, MatrixChangedEventArgs e);

    public class MatrixChangedEventArgs : EventArgs
    {
        #region fields & ctor

        private readonly string _name;

        public MatrixChangedEventArgs(string name)
        {
            _name = name;
        }

        #endregion

        #region public

        public string Name => _name;

        #endregion
    }
}