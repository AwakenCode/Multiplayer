using Infrastructure.States;
using Infrastructure.States.StateMachine;
using Service;
using UnityEngine;

namespace Infrastructure.Boot
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;
        
        private void Start()
        {
            _gameStateMachine = new GameStateMachine(Services.Container, this);
            _gameStateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}