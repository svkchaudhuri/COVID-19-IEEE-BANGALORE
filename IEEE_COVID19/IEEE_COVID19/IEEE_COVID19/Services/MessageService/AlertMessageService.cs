using System;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace IEEE_COVID19.Services.MessageService
{
    public class AlertMessageService : IAlertMessageService
    {
        public async Task ShowAlert(string title, string message, string closeButton = "Ok")
        {
            await Application.Current.MainPage.DisplayAlert(title, message, closeButton);
        }

        public async Task<bool> ShowAlertForConfirmation(string title, string message, string accept = "Ok", string cancel = "Cancel")
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
