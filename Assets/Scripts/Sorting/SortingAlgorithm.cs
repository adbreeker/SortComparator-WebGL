using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingAlgorithm : MonoBehaviour
{
    protected SortManager _sortManager;
    protected int _sortIndex = 1;

    protected ValueVisualBehavior[] _toSort;
    protected int _valueCount;

    public void Init(SortManager sortManager, int sortIndex, ValueVisualBehavior[] valuesToSort)
    {
        _sortManager = sortManager;
        _sortIndex = sortIndex;

        _toSort = valuesToSort;
        _valueCount = valuesToSort.Length;


        if (sortIndex == 1) { sortManager.isSort1OnGoing = true; }
        if (sortIndex == 2) { sortManager.isSort2OnGoing = true; }
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
