using Bb.Galileo.Models;
using Newtonsoft.Json;

namespace Bb.Galileo.Files.Viewpoints
{
    public class CooperationBase : IBase
    {

        [JsonRequired]
        public string Name { get; set; }


    }

}
