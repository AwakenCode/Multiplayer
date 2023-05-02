using System;
using System.Collections.Generic;
using Common;
using Infrastructure.Boot;
using Service;
using Service.Factory;
using Service.Network;

namespace Infrastructure.States.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine(Services services, ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(coroutineRunner, services, this),
                [typeof(MainMenuState)] = new MainMenuState(
                    services.Resolve<SceneLoader>(),
                    services.Resolve<UIFactory>(),
                    services.Resolve<NetworkPlayer>())
            };
        }

        private IState _activeState;

        public void Enter<TState>() where TState : IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private IState ChangeState<TState>() where TState : IState
        {
            _activeState?.Exit();
            _activeState = GetState<TState>();
            return _activeState;
        }
        
        private IState GetState<TState>() where TState : IState => _states[typeof(TState)];
    }
}