using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Models;
using NJsonSchema;
using NJsonSchema.Generation;
using System;
using System.Text.RegularExpressions;

namespace Bb.Galileo.Files.Schemas
{
    internal static class SchemaHelper
    {


        static SchemaHelper()
        {


            RegexOptions regexOptions = RegexOptions.None;
            _regex = new Regex(@"\w+\.(entities|links)\.schema\.json", regexOptions);

            _settings = new NJsonSchema.Generation.JsonSchemaGeneratorSettings()
            {
                FlattenInheritanceHierarchy = true,
                GenerateEnumMappingDescription = true,
            };

            _settings.ActualSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public static SchemaReference GetSchemaReference(this Newtonsoft.Json.Linq.JObject self)
        {
            var schema = (string)self.SelectToken("$schema");
            return GetSchemaReference(schema);
        }

        public static SchemaReference GetSchemaReference(this string schema)
        {

            var extension = ".schema.json";

            if (schema.ToLower().EndsWith((nameof(MetaDefinitions) + extension).ToLower()))
                return new SchemaReference() { Kind = KindSchemaEnum.SchemaDefinitions };

            if (schema.ToLower().EndsWith((nameof(LayersDefinition) + extension).ToLower()))
                return new SchemaReference() { Kind = KindSchemaEnum.SchemaLayerDefinitions };

            foreach (Match match in _regex.Matches(schema))
                if (match.Success)
                {
                    var p = match.Value;
                    if (p.ToLower().EndsWith(extension))
                        p = p.Substring(0, p.Length - extension.Length).Trim('.');

                    var i = p.Split('.');


                    var r = new SchemaReference()
                    {
                        Type = i[0],
                        Kind = ResolveKind(i[1]),
                        Schema = schema,
                    };

                    return r;
                }

            return new SchemaReference();

        }

        private static KindSchemaEnum ResolveKind(string v)
        {
            KindSchemaEnum result = KindSchemaEnum.Undefined;

            if (v.ToLower() == "entities")
                result = KindSchemaEnum.Entity;

            else if (v.ToLower() == "links")
                result = KindSchemaEnum.Relationship;

            else if (v.ToLower() == "defs")
                result = KindSchemaEnum.Definition;

            return result;
        }



        /// <summary>
        /// Generates JSON schema for a given C# class using Newtonsoft.Json.Schema library.
        /// </summary>
        /// <param name="myType">class type</param>
        /// <returns>a string containing JSON schema for a given class type</returns>
        public static JsonSchema GenerateSchemaForClass(Type myType, string name)
        {
            var _schema = JsonSchema.FromType(myType, _settings);
            return _schema;
        }

        private static readonly JsonSchemaGeneratorSettings _settings;
        private static readonly Regex _regex;

    }



}
