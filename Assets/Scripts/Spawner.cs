using UnityEngine;

public abstract class Spawner<T>: MonoBehaviour where T :BaseEntity
{
    [SerializeField] private int _startPoolSize = 5;
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(_prefab, _startPoolSize);
    }

    protected void SpawnEntity(Vector3 position)
    {
        _prefab = _pool.Get();
        _prefab.transform.position = position;
        _prefab.gameObject.SetActive(true);
        _prefab.TimeEnded += OnEntityLifeTimeEnded;
    }

    protected virtual void OnEntityLifeTimeEnded(BaseEntity entity)
    {
        entity.gameObject.SetActive(false);
    }
}