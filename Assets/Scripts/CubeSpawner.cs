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
    }

    private void Start()
    {
        StartCoroutine(SpawnCorootine());
    }

    private IEnumerator SpawnCorootine()
    {
        while(enabled)
        {
            yield return _wait;
            SpawnEntity(_spawnZone.GenerateSpawnPoint());
        }
    }

    protected override void OnEntityLifeTimeEnded(BaseEntity entity)
    {
        base.OnEntityLifeTimeEnded(entity);
        CubeDisappeared?.Invoke(entity.transform.position);
    }
}
