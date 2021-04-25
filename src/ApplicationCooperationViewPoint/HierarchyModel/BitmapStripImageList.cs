using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;

namespace devtm.Documentation.HierarchyModel
{

    public abstract class BitmapStripImageList : ImageList
    {
        // Fields
        private BitmapSource _imageStrip;
        private readonly string _resourceName;

        // Methods
        protected BitmapStripImageList(string resourceName, int width, int height)
            : base(width, height)
        {
            this._resourceName = resourceName;
        }

        protected override bool RealizeImage(int index, out BitmapSource image)
        {
            if ((index < 0) || (index >= this.BaseImageCount))
            {
                image = null;
                return false;
            }
            int x = index * base.IconWidth;
            Int32Rect sourceRect = new Int32Rect(x, 0, base.IconWidth, base.IconHeight);
            image = new CroppedBitmap(this._imageStrip, sourceRect);
            image.Freeze();
            return true;
        }

        // Properties
        protected int BaseImageCount
        {
            get
            {
                return (this.ImageStrip.PixelWidth / base.IconWidth);
            }
        }

        private BitmapSource ImageStrip
        {
            get
            {
                if (this._imageStrip == null)
                {
                    this._imageStrip = new BitmapImage(new Uri(this._resourceName));
                    this._imageStrip.Freeze();
                }
                return this._imageStrip;
            }
        }
    }


}
