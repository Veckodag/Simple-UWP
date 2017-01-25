using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using MyNotepad.Models;

namespace MyNotepad.Services
{
  public class FileServices
  {
    public async Task SaveAsync(FileInfo model)
    {
      if (model != null)
        await FileIO.WriteTextAsync(model.Ref, model.Text);
    }

    public async Task<Models.FileInfo> LoadAsync(Windows.Storage.StorageFile file)
    {
      return new FileInfo
      {
        Text = await FileIO.ReadTextAsync(file),
        Name = file.DisplayName,
        Ref = file
      };

    }
  }
}
