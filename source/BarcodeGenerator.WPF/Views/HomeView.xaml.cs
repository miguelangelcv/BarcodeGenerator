using BarcodeGenerator.ViewModels;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace BarcodeGenerator.WPF.Views
{
    [MvxViewFor(typeof(HomeViewModel))]
    public partial class HomeView : MvxWpfView
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}