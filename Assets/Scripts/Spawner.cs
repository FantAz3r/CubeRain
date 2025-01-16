using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _startPoolSize = 5;
    [SerializeField] private float _spawnTime = 1f;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private bool _isRain;
    [SerializeField] private PointGenerator _spawnZone;

    private ObjectPool<Cube> _cubePool;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_spawnTime);
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
            yield return _wait;
            SpawnCube();
        }
    }

    private void SpawnCube()
    {
        Cube cube = _cubePool.Get();
        cube.LifeTimeEnded += Release;
        cube.transform.position = _spawnZone.GenerateSpawnPoint();
        cube.gameObject.SetActive(true);

    }

    private void Release(Cube cube)
    {
        cube.LifeTimeEnded -= Release;
        _cubePool.Release(cube);
    }
}
