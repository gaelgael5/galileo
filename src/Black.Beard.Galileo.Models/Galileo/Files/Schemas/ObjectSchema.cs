using Bb.Galileo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Galileo.Files.Schemas
{

    public class ObjectBaseSchema : IBase
    {

        [Description("schema's reference")]
        [JsonRequired]
        public string Name { get; set; }

        [JsonIgnore]
        public FileModel File { get; internal set; }


    }


}
