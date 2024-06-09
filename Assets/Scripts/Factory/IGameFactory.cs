using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    public interface IGameFactory : IService
    {
        void CreateHud();
        GameObject CreateInput();
        GameObject CreatePlayer(GameObject at);
    }
}