using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(ReturnToPoolAfterDelay());
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        float waitTime = Random.Range(_minLifeTime, _maxLifeTime);
        yield return new WaitForSeconds(waitTime);

        Spawner spawner = FindObjectOfType<Spawner>();

        if (spawner != null)
        {
            spawner.GetPool().Release(this);
        }
    }
}

