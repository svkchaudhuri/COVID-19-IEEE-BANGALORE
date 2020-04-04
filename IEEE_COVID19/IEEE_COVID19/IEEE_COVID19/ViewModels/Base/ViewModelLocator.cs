
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Autofac;
using IEEE_COVID19.API;
using IEEE_COVID19.Services.MessageService;
using IEEE_COVID19.Services.NavigationService;
using IEEE_COVID19.Services.SettingsService;

namespace IEEE_COVID19.ViewModels.Base
{
    public class ViewModelLocator
    {
        private static readonly IContainer _container;
        private static ContainerBuilder _containerBuilder;
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        static ViewModelLocator()
        {
            _containerBuilder = new ContainerBuilder();
            _containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            _containerBuilder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();
            _containerBuilder.RegisterType<AlertMessageService>().As<IAlertMessageService>().SingleInstance();
            _containerBuilder.RegisterType<JsonDataProvider>().As<IJsonDataProvider>().SingleInstance();

            _containerBuilder.RegisterType<MainViewModel>().SingleInstance();
            _containerBuilder.RegisterType<MenuViewModel>().SingleInstance();
            _containerBuilder.RegisterType<HomeViewModel>().SingleInstance();
            _containerBuilder.RegisterType<NewsViewModel>().SingleInstance();
            _containerBuilder.RegisterType<ErrorViewModel>().SingleInstance();
            _containerBuilder.RegisterType<CovidInfoViewModel>().SingleInstance();
            _containerBuilder.RegisterType<CleanboardViewModel>().SingleInstance();
            _containerBuilder.RegisterType<SettingsViewModel>().SingleInstance();
            _containerBuilder.RegisterType<CoVizViewModel>().SingleInstance();
            _containerBuilder.RegisterType<NewsDetailViewModel>().SingleInstance();
            _containerBuilder.RegisterType<GuidanceViewModel>().SingleInstance();
            _containerBuilder.RegisterType<DonateViewModel>().SingleInstance();
            _containerBuilder.RegisterType<SymptomaticTestViewModel>().SingleInstance();
            _containerBuilder.RegisterType<DonateDetailsViewModel>().SingleInstance();
            _containerBuilder.RegisterType<WorkfromhomeViewModel>().SingleInstance();
            _containerBuilder.RegisterType<ImportantNoticeViewModel>().SingleInstance();
            _containerBuilder.RegisterType<PickerViewModel>().SingleInstance();
            _container = _containerBuilder.Build();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}

