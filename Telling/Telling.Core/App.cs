using System;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
//using Stateless;
using Telling.Core.StateMachine;
using Telling.Core.ViewModels.Sessions;
using MvvmCross.Core.ViewModels;
using Telling.Core.Validation;

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

            //CreatableTypes()
            //    .EndingWith("Manager")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();

            //Mvx.LazyConstructAndRegisterSingleton(() => new StateMachine<State, Trigger>(State.Launch));
            //ConfigureStateMachine();

            Mvx.RegisterType<IValidateRequest, ValidateRequest>();

            RegisterAppStart<SessionListingViewModel>();
        }

        //private void ConfigureStateMachine()
        //{
        //    var stateMachine = Mvx.Resolve<StateMachine<State, Trigger>>();

        //    stateMachine.Configure(State.Launch)
        //        .Permit(Trigger.List, State.List);

        //    stateMachine.Configure(State.List)
        //        .Permit(Trigger.Add, State.Add);

        //    stateMachine.Configure(State.Add)
        //        .Permit(Trigger.List, State.List);
        //}
    }
}
