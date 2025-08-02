using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.Runtime.Infrastructure.Services.AssetManagement;
using TankGame.Runtime.Infrastructure.Services.StaticData;
using TankGame.Runtime.UI;
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

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData, DiContainer container)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _container = container;
        }

        public async Task CreateUIRootAsync()
        {
            GameObject uiRootObject = await _assetProvider.Instantiate(Constants.UIRootAddress);
            _uiRoot = uiRootObject.transform;
            _uiRoot.position = Vector3.zero;
            _uiRoot.localScale = Vector3.one;
        }

        public Dictionary<UIPanelId, UIPanelBase> CreateUIPanels()
            => new()
            {
                { UIPanelId.VictoryPanel, CreateVictoryPanel() },
                { UIPanelId.FailurePanel, CreateFailurePanel() }
            };

        private UIPanelBase CreateFailurePanel()
        {
            var config = _staticData.ForUIPanel(UIPanelId.FailurePanel);
            var panel = _container
                .InstantiatePrefab(config.Prefab, Vector3.zero, Quaternion.identity, _uiRoot)
                .GetComponent<UIPanelBase>();
            panel.gameObject.SetActive(false);

            return panel;
        }

        private UIPanelBase CreateVictoryPanel()
        {
            var config = _staticData.ForUIPanel(UIPanelId.VictoryPanel);
            var panel = _container
                .InstantiatePrefab(config.Prefab, Vector3.zero, Quaternion.identity, _uiRoot)
                .GetComponent<UIPanelBase>();
            panel.gameObject.SetActive(false);

            return panel;
        }
    }
}