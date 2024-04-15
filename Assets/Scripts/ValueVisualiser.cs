using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueVisualiser : MonoBehaviour
{
    public int visualiserValue = 1;

    [SerializeField] SpriteRenderer _renderer;

    private void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, visualiserValue/10f, transform.localScale.y);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (visualiserValue / 20f), transform.localPosition.z);
    }

    public static bool operator <(ValueVisualiser a, ValueVisualiser b)
    {
        return a.visualiserValue < b.visualiserValue;
    }

    public static bool operator >(ValueVisualiser a, ValueVisualiser b)
    {
        return a.visualiserValue > b.visualiserValue;
    }

    public void ChangeColor(Color color)
    {
        StopAllCoroutines();
        _renderer.color = color;
    }

    public void ChangeColor(Color color, float time)
    {
        StartCoroutine(TimedColorChange(color, time));
    }

    IEnumerator TimedColorChange(Color color, float time)
    {
        Color previousColor = _renderer.color;

        if (time == 0)
        {
            _renderer.color = color;
            yield return new WaitForFixedUpdate();
            _renderer.color = previousColor;
        }
        else
        {
            _renderer.color = color;
            yield return new WaitForSeconds(time);
            _renderer.color = previousColor;
        }
    }
}
