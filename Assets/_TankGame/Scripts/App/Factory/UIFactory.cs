using Assets.Scripts.Services.AssetManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }
        public void CreateUIRoot() 
            => _uiRoot = _assetProvider.Instantiate(UIRootPath).transform;

        public Dictionary<UIPanelId, UIPanelBase> CreateUIPanels() 
            => new()
            {
                { UIPanelId.VictoryPanel, CreateVictoryPanel() },
                { UIPanelId.FailurePanel, CreateFailurePanel() }
            };

        private UIPanelBase CreateFailurePanel()
        {
            var config = _staticData.ForUIPanel(UIPanelId.FailurePanel);
            var panel = Object.Instantiate(config.Prefab, _uiRoot);
            panel.gameObject.SetActive(false);

            return panel;
        }

        private UIPanelBase CreateVictoryPanel()
        {
            var config = _staticData.ForUIPanel(UIPanelId.VictoryPanel);
            var panel = Object.Instantiate(config.Prefab, _uiRoot);
            panel.gameObject.SetActive(false);

            return panel;
        }
    }
}