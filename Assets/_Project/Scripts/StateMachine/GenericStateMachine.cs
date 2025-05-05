using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.StateMachine
{
    public abstract class GenericStateMachine<TState> where TState : IState
    {
        private readonly Dictionary<Type, TState> _states = new();
        private TState _currentState;

        public void Register<T>(T state) where T : TState
        {
            _states[typeof(T)] = state;
        }

        public void Enter<T>() where T : TState
        {
            _currentState?.Exit();
            if (_states.TryGetValue(typeof(T), out var newState))
            {
                _currentState = newState;
                _currentState.Enter();
            }
            else
            {
                Debug.LogError($"State {typeof(T).Name} not registered.");
            }
        }

        public void Tick()
        {
            _currentState?.Tick();
        }
    }
}