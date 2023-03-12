using UnityEngine;
using UnityEngine.UI;

public class DynamicCrosshair : MonoBehaviour
{
    private RectTransform reticle;
    private bool _pressed;
    public float restingSize;
    public float minSize;
    public float speed;
    private float currentSize;
    public float ReturnTime;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressed = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _pressed = false;
        }
        if (_pressed)
        {
            currentSize = Mathf.Lerp(currentSize, minSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }
        reticle.sizeDelta = new Vector2(currentSize, currentSize);
    }
}