using Newtonsoft.Json.Linq;
using System;

namespace Bb.Galileo.Files
{
    public class SchemaValidator
    {
        private readonly ModelRepository _parent;

        public SchemaValidator(ModelRepository modelRepository)
        {
            this._parent = modelRepository;
        }

        internal void Evaluate(FileModel file, JObject payload)
        {

            if (file.Schema.IsValidFile)
            {

                var schema = _parent.GetSchema(file);
                if (schema != null)
                {
                    var errors = schema.Validate(payload);
                    foreach (var error in errors)
                    {

                        var msg = new DiagnositcMessage()
                        {
                            Severity = SeverityEnum.Error,
                            File = file.FullPath,
                            Text = $"{error.Kind}, {error.Path}",
                        };

                        if (error.HasLineInfo)
                        {
                            msg.Line = error.LineNumber;
                            msg.Column = error.LinePosition;
                        }



                        _parent.Diagnostic.Append(msg);

                    }
                }
                else
                {

                    var msg = new DiagnositcMessage()
                    {
                        Severity = SeverityEnum.Warning,
                        File = file.FullPath,
                        Text = $"file {file.Schema.FilePath} not found",
                    };
                    _parent.Diagnostic.Append(msg);

                }

            }

        }

    }

}