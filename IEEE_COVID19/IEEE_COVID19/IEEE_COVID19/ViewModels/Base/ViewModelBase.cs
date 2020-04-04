using System.Threading.Tasks;
using IEEE_COVID19.Services.NavigationService;

namespace IEEE_COVID19.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {

        protected readonly INavigationService NavigationService;

        private bool _isBusy;
        private string _title;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        protected ViewModelBase()
        {
            NavigationService = ViewModelLocator.Resolve<INavigationService>();

            //var settingsService = ViewModelLocator.Resolve<ISettingsService>();

            //GlobalSetting.Instance.BaseIdentityEndpoint = settingsService.IdentityEndpointBase;
            //GlobalSetting.Instance.BaseGatewayShoppingEndpoint = settingsService.GatewayShoppingEndpointBase;
            //GlobalSetting.Instance.BaseGatewayMarketingEndpoint = settingsService.GatewayMarketingEndpointBase;
        }

        public virtual Task Initialize(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}

