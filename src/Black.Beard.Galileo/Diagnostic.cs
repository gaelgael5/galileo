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

            if (message.Severity == SeverityEnum.Error)
            {
                FileInfo file = new FileInfo(message.File);
                Bb.CommandLine.Outs.Output.WriteLineError($"exception on file {file.Directory.Name}\\{file.Name}. {message.Text}");
            }
            else
            {
                Bb.CommandLine.Outs.Output.WriteLineStandard(message.Text);
            }
        }

    }

}
