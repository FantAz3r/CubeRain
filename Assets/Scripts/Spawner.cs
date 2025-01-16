using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _startPoolSize = 5;
    [SerializeField] private float _spawnTime = 1f;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private ObjectPool<Cube> _cubePool;
    [SerializeField] private bool _isRain;
    [SerializeField] private PointGenerator _spawnZone;

    private WaitForSeconds wait;

    private void Awake()
    {
        wait = new WaitForSeconds(_spawnTime);
        _cubePool = new ObjectPool<Cube>(_cubePrefab, _startPoolSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnAfterDelay());
    }

    private IEnumerator SpawnAfterDelay()
    {
        while (true)
        {
            yield return wait;
            SpawnCube();
        }
    }

    private void SpawnCube()
    {
        Cube cube = _cubePool.Get();
        cube.EndLifeTime += Release;
        cube.transform.position = _spawnZone.GenerateSpawnPoint();
        cube.gameObject.SetActive(true);

    }

    private void Release(Cube cube)
    {
        cube.EndLifeTime -= Release;
        _cubePool.Release(cube);
    }
}
