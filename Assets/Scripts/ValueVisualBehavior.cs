using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueVisualBehavior : MonoBehaviour
{
    public int visualiserValue = 1;

    private void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, visualiserValue/10f, transform.localScale.y);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (visualiserValue / 20f), transform.localPosition.z);
    }
}
