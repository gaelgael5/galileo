using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using devtm.Documentation.HierarchyModel;
using Microsoft.VisualStudio.Language.Intellisense;

namespace devtm.Documentation.LanguageModel
{

    public class GlyphService : BitmapStripImageList, IGlyphService
    {
        // Fields
        private static GlyphService _instance;
        private const int PixelHeightPerItem = 0x10;
        private const int PixelWidthPerItem = 0x10;

        // Methods
        private GlyphService()
            : base(@"pack://application:,,,/devtm.Documentation.HierarchyModel;Component/Resource/Class", 0x10, 0x10)
        {

            var i = Resource.assign;

        }

        static GlyphService()
        {
            _instance = new GlyphService();
        }

        public ImageSource GetGlyph(StandardGlyphGroup group, StandardGlyphItem item)
        {

            if (group >= StandardGlyphGroup.GlyphGroupUnknown)            
                return null;


            int index = (group < StandardGlyphGroup.GlyphGroupError) ? ((int)group + ((int)item)) : ((int)group);
            return this.TryGetImage(index);
        }

        public static GlyphService GetService() 
        {

            var i = Resource.assign;

            return _instance; 
        }

      
    }



}
