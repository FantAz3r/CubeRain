using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLifeTime = 2;
    [SerializeField] private int _maxLifeTime = 5;

    public event Action<Cube> EndLifeTime;
    private WaitForSeconds wait;

    private void OnEnable()
    {
        wait = new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            StartCoroutine(CubeLife());
        }
    }

    private IEnumerator CubeLife()
    {
        yield return wait;
        EndLifeTime?.Invoke(this);
    }
}
