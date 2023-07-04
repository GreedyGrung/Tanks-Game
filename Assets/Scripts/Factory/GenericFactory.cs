using UnityEngine;
using Zenject;

public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [Inject] private DiContainer _diContainer;

    public T GetNewInstance(Transform spawn)
    {
        GameObject gameObject = _diContainer.InstantiatePrefab(_prefab, spawn.position, Quaternion.identity, null);
        T objectOfType = gameObject.GetComponent<T>();
        return objectOfType;
    }
}