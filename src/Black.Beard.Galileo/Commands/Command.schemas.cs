using Bb.CommandLine.Validators;
using Bb.Galileo.Files;
using Bb.Galileo.Files.Schemas;
using Black.Beard.Galileo;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace Bb.Commands
{

    public static partial class Command
    {

        public static CustomCommandLineApplication CommandSchemas(this CustomCommandLineApplication app)
        {

            var cmd = app.Command("generate", config =>
            {
                config.Description = "generate tools";
                config.HelpOption(HelpFlag);
            });

            /*
                exe generate schemas
            */
            cmd.Command("schemas", config =>
            {

                config.Description = "generates schemas";
                config.HelpOption(HelpFlag);

                var validator = new GroupArgument(config);

                var argSource = validator.Argument("<path>", "root path where your project is located."
                    , ValidatorExtension.EvaluateDirectoryPathIsValid
                    , ValidatorExtension.EvaluateRequired
                );

                var argSchemaTarget = validator.Argument("<schema path>", "path where the schemas are stored."
                   , ValidatorExtension.EvaluateDirectoryPathIsValid
               );

                var optHold = validator.OptionNoValue("--h", "hold the process. for listen change and regenerate every time");

                config.OnExecute(() =>
                {

                    if (!validator.Evaluate(out int errorNum))
                        return errorNum;

                    using (var rep = new ModelRepository(argSource.Value, new Diagnostic()))
                    {

                        rep.ItemFileHasChanged = (r, i) =>
                        {

                            bool result = false;
                            // Génerate definition from file has changed
                            if (!string.IsNullOrEmpty(argSchemaTarget.Value))
                                result = r.SchemaManager.GenerateDefinition(i, new DirectoryInfo(argSchemaTarget.Value));
                            else
                                result = r.SchemaManager.GenerateDefinition(i);

                            if (result)
                                r.Diagnostic.Append(new Galileo.DiagnositcMessage()
                                {
                                    Text = $"Schema {i.Name} is refreshed",
                                });

                        };

                        rep.Initialize();


                        // Génerate meta shemas
                        if (!string.IsNullOrEmpty(argSchemaTarget.Value))
                            rep.SchemaManager.GenerateSchemas(new DirectoryInfo(argSchemaTarget.Value));
                        else
                            rep.SchemaManager.GenerateSchemas();


                        if (optHold.HasValue())
                        {
                            Console.WriteLine("Please enter to quit");
                            Console.ReadLine();
                        }

                    }

                    return 0;

                });
            });

            return app;

        }


    }
}
