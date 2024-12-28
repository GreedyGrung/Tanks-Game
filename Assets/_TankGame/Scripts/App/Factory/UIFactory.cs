using System.Collections.Generic;
using System.Threading.Tasks;
using TankGame.App.Infrastructure.Services.StaticData;
using TankGame.App.UI;
using TankGame.Core.Services.AssetManagement;
using TankGame.Core.Utils;
using TankGame.Core.Utils.Enums;
using UnityEngine;

namespace TankGame.App.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public async Task CreateUIRootAsync()
        {
            GameObject uiRootObject = await _assetProvider.Instantiate(Constants.UIRootAddress);
            _uiRoot = uiRootObject.transform;
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