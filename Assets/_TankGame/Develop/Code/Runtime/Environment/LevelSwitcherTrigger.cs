using TankGame.Runtime.Infrastructure.StateMachine;
using TankGame.Runtime.Infrastructure.StateMachine.Interfaces;
using TankGame.Runtime.Utils.Enums.Generated;
using TankGame.Runtime.Utils;
using TankGame.Runtime.Utils.Attributes;
using UnityEngine;
using Zenject;

namespace TankGame.Runtime.Environment
{
    public class LevelSwitcherTrigger : MonoBehaviour
    {
        [SceneNameSelector]
        [SerializeField] private string _sceneName;

        private IGameStateMachine _stateMachine;
        private bool _triggered;

        [Inject]
        private void Construct(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_triggered)
            {
                return;
            }

            if (other.gameObject.CompareTag(Tags.Player))
            {
                _stateMachine.Enter<LoadLevelState, string>(_sceneName);
                _triggered = true;
            }
        }
    }
}