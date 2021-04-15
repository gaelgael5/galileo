﻿using System;
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
        


        internal void AddFile(FileModel item, FileTracingEnum trace)
        {
            if (!this._items.ContainsKey(item.FullPath))
                this._items.Add(item.FullPath, item);
            Transactionfile transaction = Update(item, trace);
        }

        internal IEnumerable<FileModel> GetFiles()
        {
            foreach (var item in _items.Values)
                yield return item;
        }

        internal Transactionfile RemoveFile(FileModel item)
        {
            if (!this._items.ContainsKey(item.FullPath))
            {
                this._items.Remove(item.FullPath);
                Transactionfile transaction = this.Models.RemoveFile(item);
                return transaction;
            }

            return new Transactionfile() { File = item, Trace = FileTracingEnum.Deleted };

        }

        internal Transactionfile RemoveFile(string item)
        {
            var file = Getfile(item);
            if (file != null)
                return RemoveFile(file);

            return null;
        }


        internal FileModel Getfile(string fullPath)
        {
            _items.TryGetValue(fullPath, out FileModel item);
            return item;
        }

        internal Transactionfile Update(FileModel item, FileTracingEnum trace)
        {
            return this.Models.Add(item, trace);
        }

        private DirectoryInfo _folder;
        private readonly IDiagnostic _diagnostic;

        public ModelRepository Models { get; }

        private readonly List<FileWatcher> _watchers;
        private readonly Dictionary<string, FileModel> _items;

    }

}