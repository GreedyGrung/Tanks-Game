using System.Collections.Generic;
using UnityEngine;

namespace _TankGame.App.StaticData.UI
{
    [CreateAssetMenu(menuName = "Static Data/UI panels static data", fileName = "UIPanelsStaticData")]
    public class UIPanelsStaticData : ScriptableObject
    {
        [SerializeField] private List<UIPanelConfig> _configs;

        public IReadOnlyList<UIPanelConfig> Configs => _configs;
    }
}