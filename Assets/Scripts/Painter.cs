using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Painter : MonoBehaviour
{
    private bool _hasChangedColor = false;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

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
            }
        }
    }

    private void OnDisable()
    {
        _hasChangedColor = false;
    }
}
