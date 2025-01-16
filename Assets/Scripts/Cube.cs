using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLifeTime = 2;
    [SerializeField] private int _maxLifeTime = 5;

    public event Action<Cube> LifeTimeEnded;
    private WaitForSeconds _wait;
    private bool _hasChangedColor = false;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _wait = new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime + 1));
    }

    private void OnEnable()
    {
        _renderer.material.color = Color.yellow;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            StartCoroutine(LivingCube());

            if (_hasChangedColor == false)
            {
                Color newColor = Random.ColorHSV();
                _renderer.material.color = newColor;
                _hasChangedColor = true;
            }
        }
    }

    private void OnDisable()
    {
        _hasChangedColor = false;
    }

    private IEnumerator LivingCube()
    {
        yield return _wait;
        LifeTimeEnded?.Invoke(this);
    }
}
