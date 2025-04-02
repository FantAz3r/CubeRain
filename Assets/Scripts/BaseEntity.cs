using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField] protected int _minLifeTime = 2;
    [SerializeField] protected int _maxLifeTime = 5;

    protected WaitForSeconds _wait;
    protected float _delay;
    protected Renderer _renderer;
    protected Color _originalColor;

    public event Action<BaseEntity> TimeEnded;

    protected virtual void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _delay = Random.Range(_minLifeTime, _maxLifeTime + 1);
        _wait = new WaitForSeconds(_delay);
        _originalColor = _renderer.material.color;
        _renderer.material.SetFloat("_Mode", 3);
    }

    protected void TimeEnd(BaseEntity entity)
    {
        TimeEnded?.Invoke(entity);
    }

    protected abstract IEnumerator LivingCoroutine();
}