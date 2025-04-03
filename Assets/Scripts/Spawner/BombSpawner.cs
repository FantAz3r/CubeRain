using System;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeDisappeared += SpawnEntity;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeDisappeared -= SpawnEntity;
    }
}
