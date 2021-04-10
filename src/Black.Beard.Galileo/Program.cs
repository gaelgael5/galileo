using System;

namespace Bb
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineService service = new CommandLineService();
            service.Main(args);
        }
    }
}
