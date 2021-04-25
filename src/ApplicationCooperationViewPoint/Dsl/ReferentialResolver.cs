using Bb.Galileo.Files;
using devtm.AutoMapper;
using Microsoft.VisualStudio.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.ApplicationCooperationViewPoint
{


    internal class ReferentialResolver
    {

        public ReferentialResolver()
        {

        }

        public static ReferentialResolver Instance 
        {
            get
            {
                if (_instance == null)
                    _instance = new ReferentialResolver();


                return _instance;
            }
        }

        public ModelRepository GetReferential(Store store)
        {
            var projectDte = store.GetProjectForStore();
            return GetReferential(projectDte);
        }

        public ModelRepository GetReferential(EnvDTE.Project projectDte)
        {
            var project = new VisualStudio.ParsingSolution.Project(projectDte);
            var fullpath = project.FullName;
            if (!string.IsNullOrEmpty(fullpath))
            {
                var directoryPath = new FileInfo(fullpath);
                return GetReferential(directoryPath);
            }

            return null;

        }

        public ModelRepository GetReferential(FileInfo directoryPath)
        {

            var fullname = directoryPath.Directory.FullName;
            if (!_referentials.TryGetValue(fullname, out ModelRepository model))
            {
                _referentials.Add(fullname, (model = new Bb.Galileo.Files.ModelRepository(fullname, new Diag())));
                model.Initialize();
            }

            return model;

        }

        private Dictionary<string, ModelRepository> _referentials = new Dictionary<string, ModelRepository>();
        private static volatile object _lock = new object();
        private static ReferentialResolver _instance;

    }

}
