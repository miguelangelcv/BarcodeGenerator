using BarcodeGenerator.ViewModels;
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
            RegisterAppStart<HomeViewModel>();
        }
    }
}