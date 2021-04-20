using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Datas
{
    public class Entities<T> : List<T>, IDocumentReferential
       where T : ReferentialBase
    {

        [JsonRequired]
        public string Target { get; set; }

        public bool HasChangedOnLoading { get; internal set; }

    }


}
