using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IEEE_COVID19.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : MasterDetailPage
    {
       public MainView()
        {
            InitializeComponent();
            
           
            MasterBehavior = MasterBehavior.Popover;
        }
    }
}