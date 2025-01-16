using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _objects;
    private Stack<T> _availableObjects;

    public ObjectPool(T prefab, int startPoolSize)
    {
        _prefab = prefab;
        _objects = new List<T>();
        _availableObjects = new Stack<T>();

        for (int i = 0; i < startPoolSize; i++)
        {
            T obj = GameObject.Instantiate(_prefab);
            _availableObjects.Push(obj);
            _objects.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }

    public T Get()
    {
        if (_availableObjects.Count > 0)
        {
            T obj = _availableObjects.Pop();
            return obj;
        }
        else
        {
            T @object = Create();
            _objects.Add(@object);
            return @object;
        }
    }

    public void Release(T obj)
    {
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;

        if (obj.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        obj.gameObject.SetActive(false);
        _availableObjects.Push(obj);
    }

    private T Create()
    {
        T obj = GameObject.Instantiate(_prefab);
        _objects.Add(obj);
        return obj;
    }
}
