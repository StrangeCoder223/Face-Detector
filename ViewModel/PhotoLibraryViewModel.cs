using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.Windows;

namespace FaceDetector
{
    public class PhotoLibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Photo> Photos
        {
            get => _photos;
            set
            {
                _photos = value;
                OnPropertyChange();
            }
        }

        public DelegateCommand AddPhotoCommand
        {
            get => _addPhotoCommand ??= new DelegateCommand(AddNewPhoto);
        }

        public DelegateCommand RemovePhotoCommand
        {
            get => _removePhotoCommand ??= new DelegateCommand(RemovePhoto);
                
        }

        public DelegateCommand RemoveAllPhotoCommand
        {
            get => _removeAllPhotoCommand ??= new DelegateCommand(RemoveAllPhoto);

        }

        public Photo SelectedPhoto 
        { 
            get => _selectedPhoto;
            set 
            {
                _selectedPhoto = value;
                if (_searcher.Detect(_selectedPhoto.Path))
                {
                    _selectedPhoto.Result = "Лицо обнаружено!";
                }
                else
                {
                    _selectedPhoto.Result = "Нет лица!";
                }
                OnPropertyChange();
            }
        }

        private DelegateCommand _addPhotoCommand;
        private DelegateCommand _removePhotoCommand;
        private DelegateCommand _removeAllPhotoCommand;

        private Photo _selectedPhoto;

        private ObservableCollection<Photo> _photos;

        private FaceSearcher _searcher;

        public PhotoLibraryViewModel()
        {
            _photos = new ObservableCollection<Photo>();
            _searcher = new FaceSearcher();
        }

        private void AddNewPhoto(object arg)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Фото (*.jpg) |*.jpg";
            fileDialog.Multiselect = true;
            fileDialog.ShowDialog();
            foreach (var fileName in fileDialog.FileNames)
            {
                Photos.Add(new Photo(fileName, fileName));
            }
            
        }

        private void RemovePhoto(object arg)
        {
             Photos.Remove(SelectedPhoto);
        }

        private void RemoveAllPhoto(object arg)
        {
            Photos.Clear();
        }

        private void OnPropertyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
