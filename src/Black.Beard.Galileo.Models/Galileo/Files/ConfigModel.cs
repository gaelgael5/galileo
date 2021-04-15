using Bb.Galileo.Files.Schemas;
using System;

namespace Bb.Galileo.Models
{
    public class ConfigModel
    {
        private string _root;

        public ConfigModel()
        {

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
            var p = System.IO.Path.Combine(names);
            p = System.IO.Path.Combine(GetRoot(), p).Replace("\\", "/");
            return p;
        }

        /// <summary>
        /// Build an uri string
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public string GetRoot()
        {
            if (string.IsNullOrEmpty(this._root))
                this._root = "http://" + System.IO.Path.Combine(this.Si, "carto", "schemas").Replace("\\", "/");
            return this._root;
        }

    }

}
