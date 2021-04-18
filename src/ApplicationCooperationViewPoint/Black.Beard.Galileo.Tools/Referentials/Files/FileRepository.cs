using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
                new FileWatcher(this, "*.schema.json"),
                new FileWatcher(this, "*.defs.json"),
                new FileWatcher(this, "*.entities.json"),
            };

            this._items = new Dictionary<string, FileModel>();

        }

        public void StartListener()
        {

            foreach (var item in _watchers)
                item.StartListen();

            int count = 0;
            lock (_lock)
                foreach (var item in this._items.Values.Where(c => c.FailedToLoad))
                {
                    var r = Update(item, FileTracingEnum.TrySecondChance);
                    if (r.FailedToLoad)
                    {
                        count++;
                    }
                }

            if (count > 0)
                Trace.WriteLine($"{count} file(s) can't be loaded");

        }

        public void StopListener()
        {

            foreach (var item in _watchers)
                item.StopListen();

        }

        public DirectoryInfo Folder { get => this._folder; }

        public IDiagnostic Diagnostic { get => this._diagnostic; }



        internal Transactionfile AddFile(FileModel item, FileTracingEnum trace)
        {
            lock (_lock)
            {
                if (!this._items.ContainsKey(item.FullPath))
                    this._items.Add(item.FullPath, item);
                Transactionfile transaction = Update(item, trace);
                return transaction;
            }
        }

        internal IEnumerable<FileModel> GetFiles()
        {
            foreach (var item in _items.Values)
                yield return item;
        }

        internal Transactionfile RemoveFile(FileModel item)
        {

            if (this._items.ContainsKey(item.FullPath))
            {
                lock (_lock)
                {
                    this._items.Remove(item.FullPath);
                    Transactionfile transaction = this.Models.RemoveFile(item);
                    return transaction;
                }
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
            var result = this.Models.Add(item, trace);
            item.FailedToLoad = (result == null) || result.FailedToLoad;
            return result;
        }

        private DirectoryInfo _folder;
        private readonly IDiagnostic _diagnostic;

        public ModelRepository Models { get; }

        private readonly List<FileWatcher> _watchers;
        private readonly Dictionary<string, FileModel> _items;
        private volatile object _lock = new object();

        internal void EvaluateSchema(Transactionfile result)
        {
            lock (_lock)
                foreach (var item in _items)
                    if (item.Value.Schema != null)
                    {
                        if (item.Value.Schema.IsValidExistingFile && item.Value.Schema.FilePath == result.File.FullPath)
                        {

                            try
                            {
                                var payload = item.Value.Load();
                                this.Models.SchemaValidator.Evaluate(item.Value, payload);
                            }
                            catch (Exception e2)
                            {
                                Diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Error, File = item.Value.FullPath, Text = e2.Message, Exception = e2 });
                            }

                        }
                    }
                    else
                    {

                    }

        }

    }

}