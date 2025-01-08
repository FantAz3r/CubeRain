using UnityEngine;

public class Painter : MonoBehaviour
{
    private bool _hasChangedColor = false;

    private void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasChangedColor)
        {
            Color newColor = Random.ColorHSV();
            GetComponent<Renderer>().material.color = newColor;
            _hasChangedColor = true;
        }
    }

    private void OnDisable()
    {
        _hasChangedColor = false;
    }
}
