using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Viewpoints;
using Bb.Galileo.Models;
using NJsonSchema;
using NJsonSchema.Generation;
using System;
using System.IO;
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

        public static SchemaReference GetSchemaReference(this Newtonsoft.Json.Linq.JObject self, FileModel file, string rootSchema)
        {

            var schema = (string)self.SelectToken("$schema");

            if (!string.IsNullOrEmpty(schema))
            {
                var schemaItem = GetSchemaReference(schema);


                if (schema.ToLower().StartsWith("http://"))
                {
                    schemaItem.SchemaIdKind = SchemaIdKindEnum.Url;
                    if (schemaItem.Kind == KindSchemaEnum.Schema)
                    {
                        schemaItem.Type = (string)self.SelectToken("id");
                        if (schemaItem.Type.ToLower().StartsWith(rootSchema.ToLower()))
                        {
                            var o = schemaItem.Type.Substring(rootSchema.Length).Trim('/').Split('/');
                            if (o[0].ToLower() == "links")
                            {
                                schemaItem.Type = o[1];
                                schemaItem.Kind = KindSchemaEnum.SchemaLink;
                            }
                            else if (o[0].ToLower() == "entities")
                            {
                                schemaItem.Type = o[1];
                                schemaItem.Kind = KindSchemaEnum.SchemaEntity;
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        var folderTargetFile = new FileInfo(file.FullPath).Directory.FullName;
                        var _file = new FileInfo(Path.Combine(folderTargetFile, schema));
                        schemaItem.FilePath = _file.FullName;
                        schemaItem.SchemaIdKind = SchemaIdKindEnum.File;
                        schemaItem.IsValidExistingFile = _file.Exists;
                    }
                    catch (Exception)
                    {

                    }

                }

                return schemaItem;
            }

            return null;

        }

        private static SchemaReference GetSchemaReference(this string schema)
        {

            var extension = ".schema.json";

            if (schema.ToLower() == @"http://json-schema.org/draft-04/schema#")
                return new SchemaReference() { Kind = KindSchemaEnum.Schema, Schema = schema };

            if (schema.ToLower().EndsWith((nameof(MetaDefinitions) + extension).ToLower()))
                return new SchemaReference() { Kind = KindSchemaEnum.SchemaDefinitions, Schema = schema };

            if (schema.ToLower().EndsWith((nameof(LayersDefinition) + extension).ToLower()))
                return new SchemaReference() { Kind = KindSchemaEnum.SchemaLayerDefinitions, Schema = schema };

            if (schema.ToLower().EndsWith((nameof(CooperationViewpoint) + extension).ToLower()))
                return new SchemaReference() { Kind = KindSchemaEnum.CooperationViewpoint, Schema = schema };

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

            return new SchemaReference()
            {
                Kind = KindSchemaEnum.Undefined,
                Schema = schema,
            };

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
