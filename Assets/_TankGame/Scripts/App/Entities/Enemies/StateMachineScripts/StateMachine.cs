using System;
using System.Collections.Generic;

namespace _TankGame.App.Entities.Enemies.StateMachineScripts
{
    public class StateMachine
    {
        private readonly Dictionary<Type, State> _states = new();
        
        public State CurrentState { get; private set; }

        public void Initialize<T>() where T : State
        {
            CurrentState = _states[typeof(T)];
            CurrentState.Enter();
        }
        
        public void RegisterState<T>(T state) where T : State
        {
            _states[typeof(T)] = state;
        }

        public void ChangeState<T>() where T : State
        {
            var newState = _states[typeof(T)];
            
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}