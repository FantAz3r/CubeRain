using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PointGenerator : MonoBehaviour
{
    [SerializeField] private float _spawnZoneWidth;
    [SerializeField] private float _spawnZoneHeight = 15f;
    [SerializeField] private float _spawnZoneDepth;
    [SerializeField] private Collider _spawnCollider;

    private void Awake()
    {
        _spawnCollider = GetComponent<Collider>();

        if (_spawnCollider != null)
        {
            _spawnZoneWidth = _spawnCollider.bounds.size.x;
            _spawnZoneDepth = _spawnCollider.bounds.size.z;
        }
    }

    public Vector3 GenerateSpawnPoint()
    {
        float randomX = Random.Range(-_spawnZoneWidth / 2, _spawnZoneWidth / 2);
        float randomZ = Random.Range(-_spawnZoneDepth / 2, _spawnZoneDepth / 2);
        return new Vector3(randomX, _spawnZoneHeight, randomZ);
    }
}
