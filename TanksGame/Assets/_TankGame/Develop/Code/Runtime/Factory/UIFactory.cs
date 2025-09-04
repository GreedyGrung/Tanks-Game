using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TankGame.Runtime.Infrastructure.Services.AssetManagement;
using TankGame.Runtime.Infrastructure.Services.StaticData;
using TankGame.Runtime.UI.Common;
using TankGame.Runtime.UI.Panels;
using TankGame.Runtime.Utils;
using TankGame.Runtime.Utils.Enums;
using UnityEngine;
using Zenject;

namespace TankGame.Runtime.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly DiContainer _container;

        private Transform _uiRoot;
        private Transform _hintsRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData, DiContainer container)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _container = container;
        }

        public async UniTask CreateUIRootAsync()
        {
            GameObject uiRootObject = await _assetProvider.Instantiate(Constants.UIRootAddress);
            _uiRoot = uiRootObject.transform;
            _uiRoot.position = Vector3.zero;
            _uiRoot.localScale = Vector3.one;
        }

        public async UniTask CreateHintsRootAsync()
        {
            GameObject hintsRootObject = await _assetProvider.Instantiate(Constants.HintsRootAddress);
            _hintsRoot = hintsRootObject.transform;
            _hintsRoot.position = Vector3.zero;
            _hintsRoot.localScale = Vector3.one;
        }

        public Dictionary<UIPanelId, UIPanelBase> CreateUIPanels()
            => new()
            {
                { UIPanelId.VictoryPanel, CreateVictoryPanel() },
                { UIPanelId.FailurePanel, CreateFailurePanel() },
                { UIPanelId.PausePanel, CreatePausePanel() },
            };

        public async UniTask<UIGenericHint> CreateGenericHint()
        {
            var prefab = await _assetProvider.Load<GameObject>(Constants.GenericHint);
            var hint = Object
                .Instantiate(prefab, _hintsRoot)
                .GetComponent<UIGenericHint>();
            hint.gameObject.SetActive(false);
            
            return hint;
        }

        private UIPanelBase CreateFailurePanel()
        {
            var config = _staticData.ForUIPanel(UIPanelId.FailurePanel);
            var panel = _container
                .InstantiatePrefab(config.Prefab, _uiRoot)
                .GetComponent<UIPanelBase>();
            panel.gameObject.SetActive(false);

            return panel;
        }

        private UIPanelBase CreateVictoryPanel()
        {
            var config = _staticData.ForUIPanel(UIPanelId.VictoryPanel);
            var panel = _container
                .InstantiatePrefab(config.Prefab, _uiRoot)
                .GetComponent<UIPanelBase>();
            panel.gameObject.SetActive(false);

            return panel;
        }

        private UIPanelBase CreatePausePanel()
        {
            var config = _staticData.ForUIPanel(UIPanelId.PausePanel);
            var panel = Object
                .Instantiate(config.Prefab, Vector3.zero, Quaternion.identity)
                .GetComponent<UIPanelBase>();
            panel.gameObject.SetActive(false);
            _container.InjectGameObject(panel.gameObject);

            return panel;
        }
    }
}