using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingAlgorithm : MonoBehaviour
{
    protected ValueVisualBehavior[] _toSort;
    protected int _valueCount;

    public void Init(ValueVisualBehavior[] values)
    {
        _toSort = values;
        _valueCount = values.Length;
        StartCoroutine(SortingCoroutine());
    }

    protected virtual IEnumerator SortingCoroutine()
    {
        yield return null;
    }

    protected virtual void SwapTwoElementsByIndex(int a, int b)
    {
        ValueVisualBehavior temp = _toSort[a];
        _toSort[a] = _toSort[b];
        _toSort[b] = temp;
    }
}
