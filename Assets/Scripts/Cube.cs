using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Cube : BaseEntity
{
    private bool _hasChangedColor = false;

    private void OnEnable()
    {
        _renderer.material.color = Color.yellow;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            if (_hasChangedColor == false)
            {
                Color newColor = Random.ColorHSV();
                _renderer.material.color = newColor;
                _hasChangedColor = true;
                StartCoroutine(LivingCoroutine());
            }
        }
    }

    private void OnDisable()
    {
        _hasChangedColor = false;
    }

    protected override IEnumerator LivingCoroutine()
    {
        yield return _wait;
        TimeEnd(this);
    }
}
