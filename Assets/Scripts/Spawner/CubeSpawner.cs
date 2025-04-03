using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube> 
{
    [SerializeField] private float _spawnTime = 1f;
    [SerializeField] private PointGenerator _spawnZone;

    private WaitForSeconds _wait;

    public event Action<Vector3> CubeDisappeared;

    protected override void Awake()
    {
        base.Awake();
        _wait = new WaitForSeconds(_spawnTime);
        StartCoroutine(SpawnCorootine());
    }

    private IEnumerator SpawnCorootine()
    {
        while(enabled)
        {
            SpawnEntity(_spawnZone.GenerateSpawnPoint());
            yield return _wait;
        }
    }

    protected override void OnEntityLifeTimeEnded(BaseEntity entity)
    {
        CubeDisappeared?.Invoke(entity.transform.position);
        base.OnEntityLifeTimeEnded(entity);
    }
}
