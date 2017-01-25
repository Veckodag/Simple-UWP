using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using MyNotepad.Models;
using MyNotepad.Services;

namespace MyNotepad.Viewmodels
{
  public class MainPageViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private FileInfo _file;

    public FileInfo File
    {
      get { return _file; }
      set
      {
        _file = value;
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(File)));
      }
    }

    FileServices _fileServices = new FileServices();

    public async void Save()
    {
      await _fileServices.SaveAsync(File);
    }

    public async void Open()
    {

      var picker = new FileOpenPicker
      {
        ViewMode = PickerViewMode.List,
        SuggestedStartLocation = PickerLocationId.DocumentsLibrary
      };

      picker.FileTypeFilter.Add(".txt");

      var file = await picker.PickSingleFileAsync();

      if (file == null)
        await new Windows.UI.Popups.MessageDialog("No file selected.").ShowAsync();
      else
        File = await _fileServices.LoadAsync(file);
    }
  }
}
