using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;

namespace Bb.ApplicationCooperationViewPoint
{

    internal sealed partial class ApplicationCooperationViewPointPackage
    {

        protected override System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {

            return base.InitializeAsync(cancellationToken, progress);

            var c = new AsyncServiceCreatorCallback( (serviceContainer, _cancellationToken, type) => 
            {
                return null;
            });
            this.AddService(typeof(ReferentialResolver), c);



        }


    }

}

