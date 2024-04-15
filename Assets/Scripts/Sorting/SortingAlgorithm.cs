using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingAlgorithm : MonoBehaviour
{
    protected SortManager _sortManager;
    protected int _sortIndex = 1;

    protected ValueVisualiser[] _toSort;
    protected int _valueCount;

    public void Init(SortManager sortManager, int sortIndex, ValueVisualiser[] valuesToSort)
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
        ValueVisualiser temp = _toSort[a];
        _toSort[a] = _toSort[b];
        _toSort[b] = temp;
    }

    protected virtual void ColorArea(int indexFrom, int indexTo, Color color)
    {
        if (indexFrom < indexTo)
        {
            for (int i = indexFrom; i <=indexTo; i++)
            {
                _toSort[i].ChangeColor(color);
            }
        }
        else
        {
            for (int i = indexFrom; i >= indexTo; i--)
            {
                _toSort[i].ChangeColor(color);
            }
        }
    }
}
