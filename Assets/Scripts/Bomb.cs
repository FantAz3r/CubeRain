using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : BaseEntity
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;

    private void OnEnable()
    {
        _renderer.material.color = Color.black;
        StartCoroutine(LivingCoroutine());
    }

    protected override IEnumerator LivingCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _delay)
        {
            float alpha = Mathf.MoveTowards(1, 0, elapsedTime / _delay);
            _renderer.material.color = new Color(0,0,0, alpha);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        TimeEnd(this);
        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rigibbody = hit.GetComponent<Rigidbody>();

            if (rigibbody != null)
            {
                rigibbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }
}