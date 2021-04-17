using Bb.Galileo.Files.Schemas;
using System.Collections.Generic;
using System.Text;

namespace Bb.Galileo.Models
{

    public interface IBase
    {

        string Name { get; }

        

    }


    public interface IEvaluate
    {

        void Evaluate();

    }

}
