using TMPro;
using UnityEngine;

public abstract class StatisticViewer<T> : MonoBehaviour where T : BaseEntity
{
    [SerializeField] private TMP_Text _textEntitySpawn; 
    [SerializeField] private TMP_Text _textEntityInstantiate; 
    [SerializeField] private TMP_Text _textEntityActive;
    [SerializeField] private Spawner<T> _spawner;

    private int _entitySpawnCount = 0;
    private int _entityInstantiateCount = 0;
    private int _entityActiveCount = 0;

    protected virtual void OnEnable()
    {
        _spawner.EntitySpawned += ViewSpawnCount;
        _spawner.EntityInstantiated += ViewInstantiateCount;
        _spawner.EntityActivated += IncreaseActiveCount;
        _spawner.EntityDeactivated += DecreaseActiveCount;
    }

    private void DecreaseActiveCount()
    {
        _entityActiveCount--;
        _textEntityActive.text = _entityActiveCount.ToString();
    }

    private void IncreaseActiveCount()
    {
        _entityActiveCount++;
        _textEntityActive.text = _entityActiveCount.ToString();
    }

    private void ViewInstantiateCount()
    {
        _entityInstantiateCount++;
        _textEntityInstantiate.text = _entityInstantiateCount.ToString();
    }

    private void ViewSpawnCount()
    {
        _entitySpawnCount++;
        _textEntitySpawn.text = _entitySpawnCount.ToString();
    }

    protected virtual void OnDisable()
    {
        _spawner.EntitySpawned -= ViewSpawnCount;
        _spawner.EntityInstantiated -= ViewInstantiateCount;
        _spawner.EntityActivated -= IncreaseActiveCount;
        _spawner.EntityDeactivated -= DecreaseActiveCount;
    }
}

