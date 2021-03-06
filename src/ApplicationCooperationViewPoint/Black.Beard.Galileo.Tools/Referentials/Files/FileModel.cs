using System;
using System.IO;

namespace Bb.Galileo.Files
{
    public class FileModel
    {


        public FileModel()
        {
        }


        public string FullPath { get => _file.FullName; }

        public bool Exist { get => _file.Exists; }

        

        public FileRepository Parent { get; private set; }

        public SchemaReference Schema { get; internal set; }
        public bool FailedToLoad { get; internal set; }
        public string Filter { get; internal set; }

        public virtual FileModel Initialize(FileInfo file, FileRepository parent)
        {
            this._file = file;
            this.Parent = parent;
            return this;
        }

        internal void Refresh()
        {
            this._file.Refresh();
        }

     
        internal Newtonsoft.Json.Linq.JObject Load()
        {
            if (_file.WaitForFile(new TimeSpan(0, 0, 0, 5)))
            {
                return (Newtonsoft.Json.Linq.JObject)_file
                    .LoadContentFromFile()
                    .ConvertToJson()
                    ;
            }
         
            return null;

        }

        public override string ToString()
        {
            return _file.FullName;
        }

        private FileInfo _file;

    }



}
