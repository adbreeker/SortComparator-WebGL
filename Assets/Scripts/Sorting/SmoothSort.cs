using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SmoothSort : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        for (int i = 1; i < _valueCount; i++)
        {
            yield return StartCoroutine(HeapifyCoroutine(i));
        }

        for (int i = _valueCount - 1; i > 0; i--)
        {
            yield return new WaitForFixedUpdate();
            SwapTwoElementsByIndex(0, i);
            _toSort[i].ChangeColor(Color.green);
            yield return StartCoroutine(SiftDownCoroutine(i, 0));
        }
        _toSort[0].ChangeColor(Color.green);
        _sortManager.SortFinished(_sortIndex);
    }

    IEnumerator HeapifyCoroutine(int i)
    {
        yield return new WaitForFixedUpdate();
        while (i > 0 && _toSort[i] > _toSort[Parent(i)])
        {
            yield return new WaitForFixedUpdate();
            SwapTwoElementsByIndex(i, Parent(i));
            i = Parent(i);
        }
    }

    IEnumerator SiftDownCoroutine(int n, int i)
    {
        yield return new WaitForFixedUpdate();

        int maxIndex = i;
        int l = LeftChild(i);
        int r = RightChild(i);

        if (l < n && _toSort[l] > _toSort[maxIndex]) { maxIndex = l; }

        if (r < n && _toSort[r] > _toSort[maxIndex]) { maxIndex = r; }

        if (i != maxIndex)
        {
            SwapTwoElementsByIndex(i, maxIndex);
            yield return StartCoroutine(SiftDownCoroutine(n, maxIndex));
        }
    }

    int Parent(int i)
    {
        return (i - 1) / 2;
    }

    int LeftChild(int i)
    {
        return 2 * i + 1;
    }

    int RightChild(int i)
    {
        return 2 * i + 2;
    }
}
