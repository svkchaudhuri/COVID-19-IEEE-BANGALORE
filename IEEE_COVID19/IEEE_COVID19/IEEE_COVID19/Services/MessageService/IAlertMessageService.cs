using System.Threading.Tasks;

namespace IEEE_COVID19.Services.MessageService
{
    public interface IAlertMessageService
    {
        Task ShowAlert(string title, string message, string closeButton = "Ok");
        Task<bool> ShowAlertForConfirmation(string title, string message, string accept = "Ok", string cancel = "Cancel");
    }
}

