using System;
using System.Collections.Generic;
using System.IO;

namespace Bb.Galileo.Files
{


    public class FileRepository
    {

        public FileRepository(string folder, IDiagnostic diagnostic, ModelRepository modelRepository)
        {
         
            this._folder = new DirectoryInfo(folder);
            this._diagnostic = diagnostic;
            this.Models = modelRepository;

            _watchers = new List<FileWatcher>()
            {
                new FileWatcher(this, "*.defs.json"),
                new FileWatcher(this, "*.entities.json"),
            };

            this._items = new Dictionary<string, FileModel>();

        }

        public void StartListener()
        {

            foreach (var item in _watchers)
                item.StartListen();

        }

        public void StopListener()
        {

            foreach (var item in _watchers)
                item.StopListen();

        }

        public DirectoryInfo Folder { get => this._folder; }

        public IDiagnostic Diagnostic { get => this._diagnostic; }
        


        internal void AddFile(FileModel item)
        {
            if (!this._items.ContainsKey(item.FullPath))
                this._items.Add(item.FullPath, item);
            Update(item);
        }

        internal IEnumerable<FileModel> GetFiles()
        {
            foreach (var item in _items.Values)
                yield return item;
        }

        internal void RemoveFile(FileModel item)
        {
            if (!this._items.ContainsKey(item.FullPath))
                this._items.Remove(item.FullPath);
        }

        internal void RemoveFile(string item)
        {
            if (!this._items.ContainsKey(item))
                this._items.Remove(item);
        }


        internal FileModel Getfile(string fullPath)
        {
            _items.TryGetValue(fullPath, out FileModel item);
            return item;
        }

        internal void Update(FileModel item)
        {
            this.Models.Add(item);
        }

        private DirectoryInfo _folder;
        private readonly IDiagnostic _diagnostic;

        public ModelRepository Models { get; }

        private readonly List<FileWatcher> _watchers;
        private readonly Dictionary<string, FileModel> _items;

    }

}