using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSort : SortingAlgorithm
{
    int partitionIndex;

    protected override IEnumerator SortingCoroutine()
    {
        int maxDepth = Mathf.FloorToInt(Mathf.Log(_valueCount, 2)) * 2;
        yield return StartCoroutine(IntroSortCoroutine(_toSort, 0, _valueCount - 1, maxDepth));
        ColorArea(0, _valueCount - 1, Color.green);
        _sortManager.SortFinished(_sortIndex);
    }

    IEnumerator IntroSortCoroutine(ValueVisualiser[] array, int startIndex, int endIndex, int maxDepth)
    {
        int n = endIndex - startIndex + 1;
        if (n < 16)
        {
            yield return StartCoroutine(InsertionSortCoroutine(array, startIndex, endIndex));
        }
        else if (maxDepth == 0)
        {
            yield return StartCoroutine(HeapSortCoroutine(array, startIndex, endIndex));
        }
        else
        {
            yield return StartCoroutine(Partition(startIndex, endIndex));

            int partitionIndexTemp = partitionIndex;

            yield return StartCoroutine(IntroSortCoroutine(array, startIndex, partitionIndexTemp - 1, maxDepth - 1));
            yield return StartCoroutine(IntroSortCoroutine(array, partitionIndexTemp + 1, endIndex, maxDepth - 1));
        }
    }

    IEnumerator InsertionSortCoroutine(ValueVisualiser[] array, int startIndex, int endIndex)
    {
        for (int i = startIndex + 1; i <= endIndex; i++)
        {
            ValueVisualiser key = array[i];
            int j = i - 1;
            while (j >= startIndex && array[j] > key)
            {
                array[j + 1] = array[j];
                j = j - 1;
            }
            array[j + 1] = key;
            yield return null; // Yield null for one frame to visualize each step
        }
    }

    IEnumerator HeapSortCoroutine(ValueVisualiser[] array, int startIndex, int endIndex)
    {
        int n = endIndex - startIndex + 1;

        for (int i = n / 2 - 1; i >= startIndex; i--)
        {
            yield return StartCoroutine(Heapify(array, n, i, startIndex));
        }

        for (int i = n - 1; i >= startIndex; i--)
        {
            ValueVisualiser temp = array[startIndex];
            array[startIndex] = array[startIndex + i];
            array[startIndex + i] = temp;

            yield return StartCoroutine(Heapify(array, i, startIndex, startIndex));
        }
    }

    IEnumerator Heapify(ValueVisualiser[] array, int n, int i, int startIndex)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && array[startIndex + left] > array[startIndex + largest])
        {
            largest = left;
        }

        if (right < n && array[startIndex + right] > array[startIndex + largest])
        {
            largest = right;
        }

        if (largest != i)
        {
            ValueVisualiser temp = array[startIndex + i];
            array[startIndex + i] = array[startIndex + largest];
            array[startIndex + largest] = temp;

            yield return null; // Yield null for one frame to visualize each step

            yield return StartCoroutine(Heapify(array, n, largest, startIndex));
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
