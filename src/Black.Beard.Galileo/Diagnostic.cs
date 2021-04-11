using Bb;
using Bb.Galileo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Black.Beard.Galileo
{

    public class Diagnostic : IDiagnostic
    {

        public void Append(DiagnositcMessage message)
        {

            string filepath = string.Empty;
            string exception = string.Empty;

            if (message.File != null)
            {
                FileInfo file = new FileInfo(message.File);
                filepath = $" file {file.Directory.Name}\\{file.Name}";
            }
            if (message.Exception != null)
                exception = $"exception {message.Exception.GetType().Name}.";

            if (message.Severity == SeverityEnum.Error)
                Bb.CommandLine.Outs.Output.WriteLineError($"{exception}{filepath}. {message.Text}");
         
            else
                Bb.CommandLine.Outs.Output.WriteLineStandard(message.Text);
        }

    }

}
