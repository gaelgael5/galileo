using Bb.CommandLine.Outs;
using Bb.Commands;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Linq;

namespace Bb
{

    public partial class CommandLineService
    {

        public CommandLineService()
        {
        }

        public void Main(params string[] args)
        {

            CommandLineApplication app = null;
            try
            {

                app = new CustomCommandLineApplication()
                    .Initialize();

                int result = app.Execute(args);

                Output.Flush();
                if (result > 0)
                {
                    Environment.ExitCode = CommandLineService.ExitCode = result;
                    CommandLineService.MustStop = true;
                }
            }
            catch (System.FormatException e2)
            {
                FormatException(app, e2);
            }
            catch (CommandParsingException e)
            {

                Output.WriteLineError(e.Message);
                Output.WriteLineError(e.StackTrace);
                Output.Flush();

                if (e.HResult > 0)
                    Environment.ExitCode = CommandLineService.ExitCode = e.HResult;

                app.ShowHelp();

                Environment.ExitCode = CommandLineService.ExitCode = 1;

            }
            catch (Exception e)
            {

                Output.WriteLineError(e.Message);
                Output.WriteLineError(e.StackTrace);
                Output.Flush();

                if (e.HResult > 0)
                    Environment.ExitCode = CommandLineService.ExitCode = e.HResult;

                Environment.ExitCode = CommandLineService.ExitCode = 1;

            }

        }

        private static void FormatException(CommandLineApplication app, FormatException e2)
        {
            Output.WriteLineError(e2.Message);
            Output.Flush();
            app.ShowHelp();
            Environment.ExitCode = CommandLineService.ExitCode = 2;
        }


        public static int ExitCode { get; private set; }

        public static bool MustStop { get; internal set; }

    }

}
