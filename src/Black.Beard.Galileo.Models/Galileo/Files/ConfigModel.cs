using Bb.Galileo.Files.Schemas;
using System;

namespace Bb.Galileo.Models
{
    public class ConfigModel
    {

        public ConfigModel()
        {
            Targets = new TargetDefinition();
        }

        public string Si { get; set; }

        public string RestrictionNamePattern { get; set; } = @"([a-zA-Z]+[a-zA-Z0-9_]+)?";

        public TargetDefinition Targets { get; set; }

        /// <summary>
        /// Build an uri string
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public string GetUri(params string[] names)
        {
            var uri = new Uri("http://" + System.IO.Path.Combine(this.Si, "carto", "schemas").Replace("\\", "/"));
            var o = new Uri(uri, System.IO.Path.Combine(names).Replace("\\", "/")).AbsoluteUri;
            return o;
        }

    }

}
