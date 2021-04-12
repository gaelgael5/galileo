using Bb.CommandLine.Validators;
using Bb.Galileo.Files;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Models;
using Black.Beard.Galileo;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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

                        rep.ItemFileHasChanged = (repository, transaction) =>
                        {

                            switch (transaction.File.Schema.Kind)
                            {
                                
                                case Galileo.KindSchemaEnum.Entity:
                                    break;
                                

                                case Galileo.KindSchemaEnum.Relationship:
                                    break;


                                case Galileo.KindSchemaEnum.SchemaDefinitions:
                                    
                                    List<ModelDefinition> _items = transaction.Added.OfType<ModelDefinition>().ToList();
                                    _items.AddRange(transaction.Updated.OfType<ModelDefinition>());
                                    if (!string.IsNullOrEmpty(argSchemaTarget.Value))
                                        repository.SchemaManager.GenerateDefinition(_items, new DirectoryInfo(argSchemaTarget.Value));
                                    else
                                        repository.SchemaManager.GenerateDefinition(_items);

                                    if (!string.IsNullOrEmpty(argSchemaTarget.Value))
                                        rep.SchemaManager.GenerateSchemas(new DirectoryInfo(argSchemaTarget.Value));
                                    else
                                        rep.SchemaManager.GenerateSchemas();
                                    break;

                                case Galileo.KindSchemaEnum.SchemaLayerDefinitions:
                                    if (!string.IsNullOrEmpty(argSchemaTarget.Value))
                                        rep.SchemaManager.GenerateSchemas(new DirectoryInfo(argSchemaTarget.Value));
                                    else
                                        rep.SchemaManager.GenerateSchemas();
                                    break;

                                case Galileo.KindSchemaEnum.Definition:
                                case Galileo.KindSchemaEnum.Schema:
                                case Galileo.KindSchemaEnum.CooperationViewpoint:
                                case Galileo.KindSchemaEnum.Undefined:
                                default:
                                    break;
                            }
                      
                        };

                        rep.Initialize();


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
