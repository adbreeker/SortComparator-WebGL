using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : SortingAlgorithm
{
    int partitionIndex;

    protected override IEnumerator SortingCoroutine()
    {
        yield return StartCoroutine(QuickSortCoroutine(0, _toSort.Length - 1));
        _sortManager.SortFinished(_sortIndex);
    }

    private IEnumerator QuickSortCoroutine(int low, int high)
    {
        yield return new WaitForFixedUpdate();
        if (low < high)
        {
            yield return StartCoroutine(Partition(low, high));

            int partitionIndexTemp = partitionIndex;

            yield return StartCoroutine(QuickSortCoroutine(low, partitionIndexTemp - 1));
            yield return StartCoroutine(QuickSortCoroutine(partitionIndexTemp + 1, high));
        }
        else
        {
            ColorArea(high + 1, low, Color.green);
        }
    }

    IEnumerator Partition(int low, int high)
    {
        ValueVisualiser pivot = _toSort[high];
        pivot.ChangeColor(Color.red, 0);
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            yield return new WaitForFixedUpdate();
            pivot.ChangeColor(Color.red, 0);
            if (_toSort[j] < pivot)
            {
                i++;
                ColorArea(i, j, Color.yellow, 0);
                _toSort[j].ChangeColor(Color.red, 0);
                _toSort[i].ChangeColor(Color.red, 0);
                SwapTwoElementsByIndex(i, j);
            }
        }
        yield return new WaitForFixedUpdate();
        _toSort[i + 1].ChangeColor(Color.red, 0);
        _toSort[high].ChangeColor(Color.red, 0);
        SwapTwoElementsByIndex(i + 1, high);
        partitionIndex = i + 1;
    }
}
