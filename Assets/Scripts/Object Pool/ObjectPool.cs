using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public bool AutoExpand { get; set; }

    private readonly T _prefab;
    private readonly Transform _container;
    private List<T> _pool;

    public ObjectPool(T prefab, int size, Transform container)
    {
        _prefab = prefab;
        _container = container;

        CreatePool(size);
    }

    private void CreatePool(int size)
    {
        _pool = new();

        for (int i = 0; i < size; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var newObject = Object.Instantiate(_prefab, _container);
        newObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(newObject);

        return newObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var poolElement in _pool)
        {
            if (!poolElement.gameObject.activeInHierarchy)
            {
                element = poolElement;
                poolElement.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T TakeFromPool()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (AutoExpand)
        {
            CreateObject(true);
        }

        throw new System.Exception("The pool is empty!");
    }
}
