using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.StartScreen;
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

    public async Task<FileInfo> LoadAsync(StorageFile file)
    {
      if (JumpList.IsSupported())
      {
        var jList = await JumpList.LoadCurrentAsync();
        jList.SystemGroupKind = JumpListSystemGroupKind.None;

        while (jList.Items.Count > 4)
          jList.Items.RemoveAt(jList.Items.Count - 1);

        if (jList.Items.All(x => x.Arguments != file.Path))
        {
          var jItem = JumpListItem.CreateWithArguments(file.Path, file.DisplayName);
          jList.Items.Add(jItem);
        }

        await jList.SaveAsync();

      }

      var mruList = StorageApplicationPermissions.MostRecentlyUsedList;

      while (mruList.Entries.Count >= mruList.MaximumItemsAllowed)
        mruList.Remove(mruList.Entries.First().Token);

      if (mruList.Entries.All(x => x.Metadata != file.Path))
        mruList.Add(file, file.Path);

      var futureAccessList = StorageApplicationPermissions.FutureAccessList;
      futureAccessList.Add(file);

      return new FileInfo
      {
        Text = await FileIO.ReadTextAsync(file),
        Name = file.DisplayName,
        Ref = file
      };
    }
  }
}
