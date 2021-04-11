using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bb.Galileo.Files
{


    public class FileWatcher
    {

        public FileWatcher(FileRepository parent, string filter)
        {
            this._parent = parent;
            this._filter = filter;
        }


        private void InitializeFilewatcher()
        {

            this._watcher = new FileSystemWatcher(_parent.Folder.FullName)
            {
                Filter = _filter,
                IncludeSubdirectories = true,
                EnableRaisingEvents = false,
                NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size,

            };

        }

        public virtual void StartListen()
        {

            if (this._watcher == null)
                lock (_lockFile)
                    if (this._watcher == null)
                        InitializeFilewatcher();

            if (!this._watcher.EnableRaisingEvents)
                lock (_lockFile)
                    if (!this._watcher.EnableRaisingEvents)
                    {
                        this._watcher.Changed += OnChanged;
                        this._watcher.Created += OnCreated;
                        this._watcher.Deleted += OnDeleted;
                        this._watcher.Renamed += OnRenamed;
                        this._watcher.Error += OnError;

                        this._watcher.EnableRaisingEvents = true;
                    }

            RefreshAdd();

        }

        public virtual void StopListen()
        {

            if (this._watcher != null)
                if (this._watcher.EnableRaisingEvents)
                    lock (_lockFile)
                        if (this._watcher.EnableRaisingEvents)
                        {
                            this._watcher.EnableRaisingEvents = false;
                            this._watcher.Changed -= OnChanged;
                            this._watcher.Created -= OnCreated;
                            this._watcher.Deleted -= OnDeleted;
                            this._watcher.Renamed -= OnRenamed;
                            this._watcher.Error -= OnError;
                        }

        }

        private void RefreshAdd()
        {

            lock (_lockFile)
            {

                HashSet<string> _h2 = new HashSet<string>();
                List<FileModel> toRemove = new List<FileModel>();
                foreach (FileModel item in _parent.GetFiles())
                {
                    item.Refresh();
                    if (item.Exist)
                        toRemove.Add(item);
                    else
                        _h2.Add(item.FullPath);
                }

                foreach (var item in toRemove)
                    Remove(item);

                foreach (var item in this._parent.Folder.GetFiles(_filter, SearchOption.AllDirectories))
                    if (_h2.Add(item.FullName))
                        Add(item, FileTracingEnum.Loading);

            }

        }

        private void Add(FileInfo file, FileTracingEnum trace)
        {
            _parent.AddFile(new FileModel()
                .Initialize(file, this._parent), trace);
        }

        private void Remove(FileModel file)
        {
            _parent.RemoveFile(file);
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            var ex = e.GetException();
            _parent.Diagnostic.Append(new DiagnositcMessage() { Text = ex.Message, Exception = ex });
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            _parent.Diagnostic.Append(new Galileo.DiagnositcMessage()
            {
                Text = $"update {e.FullPath} is renamed",
            });

            _parent.RemoveFile(e.OldFullPath);
            Add(new FileInfo(e.FullPath), FileTracingEnum.Renamed);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            _parent.Diagnostic.Append(new Galileo.DiagnositcMessage()
            {
                Text = $"update {e.FullPath} is deleted",
            });

            _parent.RemoveFile(e.FullPath);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _parent.Diagnostic.Append(new Galileo.DiagnositcMessage()
            {
                Text = $"update {e.FullPath} is created",
            });

            Add(new FileInfo(e.FullPath), FileTracingEnum.OnCreated);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {

                _parent.Diagnostic.Append(new Galileo.DiagnositcMessage()
                {
                    Text = $"update {e.FullPath} is updated",
                });

                Transactionfile transaction = _parent.Update(_parent.Getfile(e.FullPath), FileTracingEnum.Changed);

            }
        }



        private readonly FileRepository _parent;
        private readonly string _filter;
        private FileSystemWatcher _watcher;
        private volatile object _lockFile = new object();

    }



    public enum FileTracingEnum
    {
        Loading,
        Renamed,
        OnCreated,
        Changed,
        Deleted,
    }


}
