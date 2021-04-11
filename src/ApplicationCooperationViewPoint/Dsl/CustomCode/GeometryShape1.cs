using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.ApplicationCooperationViewPoint
{

    public partial class ConceptShape
    {


        protected static ArrayList CustomOutlineDashPattern
        {
            get
            {
                if (customOutlineDashPattern == null)
                    customOutlineDashPattern = new ArrayList(new float[] { 4.0F, 2.0F, 1.0F, 3.0F });
                return customOutlineDashPattern;
            }
        }

        private static ArrayList customOutlineDashPattern;


    }

    public partial class ConceptElementShape
    {

        protected static ArrayList CustomOutlineDashPattern
        {
            get
            {
                if (customOutlineDashPattern == null)
                    customOutlineDashPattern = new ArrayList(new float[] { 4.0F, 2.0F, 1.0F, 3.0F });
                return customOutlineDashPattern;
            }
        }

        private static ArrayList customOutlineDashPattern;


    }

    public partial class ConceptSubElementShape
    {


        protected static ArrayList CustomOutlineDashPattern
        {
            get
            {
                if (customOutlineDashPattern == null)
                    customOutlineDashPattern = new ArrayList(new float[] { 4.0F, 2.0F, 1.0F, 3.0F });
                return customOutlineDashPattern;
            }
        }


        private static ArrayList customOutlineDashPattern;


    }

    public partial class CooperationShape
    {


        protected static ArrayList CustomOutlineDashPattern
        {
            get
            {
                if (customOutlineDashPattern == null)
                    customOutlineDashPattern = new ArrayList(new float[] { 4.0F, 2.0F, 1.0F, 3.0F });
                return customOutlineDashPattern;
            }
        }

        private static ArrayList customOutlineDashPattern;


    }

    public partial class RelationshipShape
    {


        protected static ArrayList CustomOutlineDashPattern
        {
            get
            {
                if (customOutlineDashPattern == null)
                    customOutlineDashPattern = new ArrayList(new float[] { 4.0F, 2.0F, 1.0F, 3.0F });
                return customOutlineDashPattern;
            }
        }

        private static ArrayList customOutlineDashPattern;


    }

}
