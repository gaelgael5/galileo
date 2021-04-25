using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace devtm.Documentation.HierarchyModel
{

    public abstract class ImageList
    {
        // Fields
        private Dictionary<int, BitmapSource> _bitmapCache;
        private int _height;
        private int _width;

        // Methods
        protected ImageList(int width, int height)
        {
            this._width = width;
            this._height = height;
        }

        protected abstract bool RealizeImage(int index, out BitmapSource image);
        public virtual BitmapSource TryGetImage(int index)
        {
            BitmapSource source;
            if (!this.Cache.TryGetValue(index, out source) && this.RealizeImage(index, out source))
            {
                this.Cache[index] = source;
            }
            return source;
        }

        // Properties
        protected IDictionary<int, BitmapSource> Cache
        {
            get
            {
                return (this._bitmapCache = this._bitmapCache ?? new Dictionary<int, BitmapSource>());
            }
        }

        public int IconHeight
        {
            get
            {
                return this._height;
            }
        }

        public int IconWidth
        {
            get
            {
                return this._width;
            }
        }
    }


}
