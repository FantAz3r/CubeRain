using System;
using UnityEngine;

public abstract class Spawner<T>: MonoBehaviour where T :BaseEntity
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    public event Action EntitySpawned;
    public event Action EntityInstantiated;
    public event Action EntityActivated;
    public event Action EntityDeactivated;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(_prefab);
        _pool.ObjectInstatiated += EntityInstatiate;
    }

    protected virtual void SpawnEntity(Vector3 position)
    {
        _prefab = _pool.Get();
        _prefab.transform.position = position;
        _prefab.TimeEnded += OnEntityLifeTimeEnded;
        EntityActivated?.Invoke();
        EntitySpawned?.Invoke();
    }

    protected virtual void OnEntityLifeTimeEnded(BaseEntity entity)
    {
        _pool.Release((T)entity);
        EntityDeactivated?.Invoke();
        entity.TimeEnded -= OnEntityLifeTimeEnded;
    }

    public void EntityInstatiate()
    {
        EntityInstantiated?.Invoke(); 
    }
}