using MvvmCross.Platform.IoC;
using Telling.Core.ViewModels.Sessions;

namespace Telling.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Manager")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<SessionListingViewModel>();
        }
    }
}
