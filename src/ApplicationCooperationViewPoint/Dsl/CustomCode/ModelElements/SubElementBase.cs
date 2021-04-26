using Bb.Galileo.Files;
using System.Linq;
using Bb.Galileo.Files.Datas;

namespace Bb.ApplicationCooperationViewPoint
{
    public partial class SubElementBase
    {


        private string GetReferenceSourceValue()
        {
            return _referenceSource;
        }


        private void SetReferenceSourceValue(string newValue)
        {
            _referenceSource = newValue;

            if (!string.IsNullOrEmpty(newValue))
            {
                var referential = ReferentialResolver.Instance.GetReferential(Store);
                if (referential != null)
                {
                    var query = new ResolveQuery(newValue);
                    var item = query.GetReferentials(referential)
                                     .OfType<ReferentialEntity>()
                                     .FirstOrDefault();
                    if (item != null)
                    {
                        this.Name = item.Name;
                        this.Type = item.TypeEntity;
                    }
                }
            }

        }


        private string _referenceSource;

    }


}

