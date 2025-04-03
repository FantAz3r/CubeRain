using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : BaseEntity
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;

    protected override void Awake()
    {
        base.Awake();
        Material material = _renderer.material;
        material.SetFloat("_Mode", 3);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    }

    private void OnEnable()
    {
        StartCoroutine(LivingCoroutine());
    }

    protected override IEnumerator LivingCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _delay)
        {
            float alpha = Mathf.MoveTowards(1, 0, elapsedTime / _delay);
            _renderer.material.color = new Color(0, 0,0, alpha);
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