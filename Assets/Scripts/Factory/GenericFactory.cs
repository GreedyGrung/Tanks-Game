using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    public T GetNewInstance(Transform spawn)
    {
        return Instantiate(_prefab, spawn.position, Quaternion.identity);
    }
}