using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using MyNotepad.Models;
using NotificationsExtensions;
using NotificationsExtensions.Toasts;

namespace MyNotepad.Services
{
  public class ToastService
  {
    public void ShowToast(FileInfo file, string message = "Success")
    {
      var image = @"C:\Users\fresan\OneDrive - Triona AB\UWPDemo\MyNotepad\Assets\mono-save.png";

      var content = new ToastContent
      {
        Launch = file.Ref.Path,
        Visual = new ToastVisual
        {
          #region Depricated
          //TitleText = new ToastText
          //{
          //  Text = message
          //},

          //BodyTextLine1 = new ToastText
          //{
          //  Text = file.Name
          //},
          //AppLogoOverride = new ToastAppLogo
          //{
          //  Source = new ToastImageSource(image)
          //}
          #endregion

          BindingGeneric = new ToastBindingGeneric
          {
            Children =
             {
               new AdaptiveText
               {
                 Text = message
               },
               new AdaptiveText
               {
                 Text = file.Name
               }
             },
            AppLogoOverride = new ToastGenericAppLogo
            {
              Source = image
            }
          }
        },
        Audio = new ToastAudio
        {
          Src = new Uri("ms-winsoundevent:Notification.IM")
        }

      };

      var notification = new ToastNotification(content.GetXml());
      var notifier = ToastNotificationManager.CreateToastNotifier();
      notifier.Show(notification);

    }
  }
}
