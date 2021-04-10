using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Linq;
using System.Reflection;

namespace Bb.Commands
{

    public static partial class Command
    {

        static Command()
        {

            /// Command._access = "('" + string.Join("','", Enum.GetNames(typeof(AccessModuleEnum))) + "')";

        }


        /// <summary>
        /// Initializes the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public static CustomCommandLineApplication Initialize(this CustomCommandLineApplication app)
        {

            AnsiConsole.GetError(true);

            app.HelpOption(HelpFlag);
            app.VersionOption(VersionFlag, Constants.ShortVersion, Constants.LongVersion);

            app.Name = Constants.Name;
            app.Description = Constants.ProgramHelpDescription;
            app.ExtendedHelpText = Constants.ExtendedHelpText;

            var methods = typeof(Command).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(c => c.Name != "Initialize").ToList();

            methods = methods.Where(c =>
                       typeof(CustomCommandLineApplication).IsAssignableFrom(c.ReturnType)
                    && c.GetParameters() is ParameterInfo[] u
                    && u.Length == 1
                    && typeof(CommandLineApplication).IsAssignableFrom(u[0].ParameterType)
                  ).ToList();

            foreach (MethodInfo method in methods)
                try
                {
                    method.Invoke(null, new object[] { app });
                }
                catch (Exception e)
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                        System.Diagnostics.Debugger.Break();
                    throw;
                }

            return app;

        }



        // public static BbClientHttp Client => new BbClientHttp(new Uri(Helper.Parameters.ServerUrl));

        public static object Result { get; internal set; }

        public const string HelpFlag = "-? |-h |--help";
        public const string VersionFlag = "-v |--version";
        public static string _access;


    }


}
