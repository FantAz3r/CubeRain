using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _poolMaxSize = 20;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private float _repairRate = 0.2f;
    [SerializeField] private Cube _prefab;

    [SerializeField] private GameObject _spawnPlane;
    [SerializeField] private float _spawnZoneHeight = 15f;
    [SerializeField] private float _spawnZoneWidth;
    [SerializeField] private float _spawnZoneDepth;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_prefab),
        actionOnGet: (cube) => ActionOnGet(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false),
        actionOnDestroy: (cube) => Destroy(cube),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize) ;

        Collider planeRenderer = GetComponent<Collider>();

        if (planeRenderer != null)
        {
            _spawnZoneWidth = planeRenderer.bounds.size.x;
            _spawnZoneDepth = planeRenderer.bounds.size.z;
        }
    }

    private Vector3 GenerateSpawnPoint()
    {
        float randomX = Random.Range(-_spawnZoneWidth / 2, _spawnZoneWidth / 2);
        float randomZ = Random.Range(-_spawnZoneDepth / 2, _spawnZoneDepth / 2);
        return new Vector3(randomX, _spawnZoneHeight, randomZ);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GenerateSpawnPoint();
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.gameObject.SetActive(true);
    }

    void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repairRate);
    }

    public ObjectPool<Cube> GetPool() 
    {
        return _pool; 
    }

    private void GetCube()
    {
        _pool.Get();
    }

}
