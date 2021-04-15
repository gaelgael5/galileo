using System;

namespace Bb.Galileo
{
    public class DiagnositcMessage
    {

        public SeverityEnum Severity { get; set; }

        public string File { get; set; }

        public string Text { get; set; }

        public int Line { get; set; }

        public int Column { get; set; }

        public Exception Exception { get; set; }


    }


}
