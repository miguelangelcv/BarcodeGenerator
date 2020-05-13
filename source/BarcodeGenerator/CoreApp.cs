using BarcodeGenerator.Services;
using BarcodeGenerator.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace BarcodeGenerator
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterServices();
            RegisterAppStart<HomeViewModel>();
        }

        private void RegisterServices()
        {
            Mvx.IoCProvider.RegisterSingleton(new BarcodeService());
            Mvx.IoCProvider.RegisterSingleton(new ClipboardService());
        }
    }
}