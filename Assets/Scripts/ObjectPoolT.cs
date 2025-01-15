using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _objects;
    private Stack<T> _avalubleObjects;

    public ObjectPool(T prefab, int startPoolSize)
    {
        _prefab = prefab;
        _objects = new List<T>();

        for (int i = 0; i < startPoolSize; i++)
        {
            var obj = GameObject.Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            _objects.Add(obj);
        }
        _avalubleObjects = new Stack<T>(_objects);
    }

    public T Get()
    {
        if(_avalubleObjects.Count > 0 )
        {
            return _avalubleObjects.Pop();
        }
        
        T @object = Create();
        _objects.Add(@object);
        return @object;
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        _avalubleObjects.Push(obj);
    }

    private T Create()
    {
        T obj = GameObject.Instantiate(_prefab);
        _objects.Add(obj);
        return obj;
    }
}
