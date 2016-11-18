using Stateless;
using System;
using System.Collections.Generic;

namespace Telling.Core.StateMachine
{
    public class ExtendedStateMachine<State, Trigger> : StateMachine<State, Trigger>
    {
        Dictionary<Trigger, TriggerWithParameters<string>> _navigationTriggers;

        public ExtendedStateMachine(State initialState) : base(initialState)
        {
            Initialize();
        }

        public ExtendedStateMachine(Func<State> stateAccessor, Action<State> stateMutator) : base(stateAccessor, stateMutator)
        {
            Initialize();
        }

        void Initialize()
        {
            _navigationTriggers = new Dictionary<Trigger, TriggerWithParameters<string>>();
        }

        public void FireNavigationTrigger(Trigger trigger)
        {
            if (_navigationTriggers.ContainsKey(trigger))
            {
                Fire(_navigationTriggers[trigger], trigger.ToString());
            }
        }

        public void AddNavigationTrigger(Trigger trigger, TriggerWithParameters<string> triggerWithParameters)
        {
            if (!_navigationTriggers.ContainsKey(trigger))
            {
                _navigationTriggers.Add(trigger, triggerWithParameters);
            }
        }

        public TriggerWithParameters<string> GetNavigationTrigger(Trigger trigger)
        {
            return _navigationTriggers[trigger];
        }
    }
}
