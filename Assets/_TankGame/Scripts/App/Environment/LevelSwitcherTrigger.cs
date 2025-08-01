using TankGame.App.Utils;
using TankGame.App.Editor;
using TankGame.App.Infrastructure.StateMachine;
using TankGame.App.Infrastructure.StateMachine.Interfaces;
using TankGame.Core.Utils.Enums.Generated;
using UnityEngine;
using Zenject;

namespace TankGame.App.Environment
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