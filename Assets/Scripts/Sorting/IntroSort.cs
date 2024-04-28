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
        yield return StartCoroutine(IntrosortRecursive(_toSort, maxDepth, 0, _valueCount - 1));
        _sortManager.SortFinished(_sortIndex);
    }

    IEnumerator IntrosortRecursive(ValueVisualiser[] array, int maxDepth, int low, int high)
    {
        yield return new WaitForFixedUpdate();

        int n = high - low + 1;

        if (n <= 16)
        {
            if (low > 0) { _toSort[low - 1].ChangeColor(Color.green); }
            yield return StartCoroutine(InsertionSort(low, high));
        }
        else if (maxDepth == 0)
        {
            Time.timeScale = 0.01f;
            yield return StartCoroutine(HeapSort(array, low, high));
        }
        else
        {
            yield return StartCoroutine(Partition(array, low, high));

            int partitionIndexTemp = partitionIndex;

            yield return StartCoroutine(IntrosortRecursive(array, maxDepth - 1, low, partitionIndexTemp - 1));
            yield return StartCoroutine(IntrosortRecursive(array, maxDepth - 1, partitionIndexTemp + 1, high));
        }
    }

    IEnumerator InsertionSort(int start, int end)
    {
        _toSort[start].ChangeColor(Color.yellow);

        for (int i = start + 1; i <= end; i++)
        {
            yield return new WaitForFixedUpdate();
            int j = i;
            while (j > start && _toSort[j - 1] > _toSort[j])
            {
                yield return new WaitForFixedUpdate();
                _toSort[j].ChangeColor(Color.red, 0);
                SwapTwoElementsByIndex(j - 1, j);
                j--;
            }
            ColorArea(j, i, Color.yellow);
        }

        ColorArea(start, end, Color.green);
    }

    IEnumerator HeapSort(ValueVisualiser[] array, int low, int high)
    {
        int count = high - low + 1;
        yield return StartCoroutine(Heapify(array, low, count));

        int end = high;
        while (end > low)
        {
            yield return new WaitForFixedUpdate();
            SwapTwoElementsByIndex(low, end);
            array[end].ChangeColor(Color.green);
            yield return StartCoroutine(SiftDown(array, low, end, low));
            end--;
        }
        array[low].ChangeColor(Color.green);
    }

    IEnumerator Heapify(ValueVisualiser[] array, int low, int count)
    {
        int start = Parent(count - 1) + low + 1;

        while (start > low)
        {
            yield return new WaitForFixedUpdate();
            start--;
            yield return StartCoroutine(SiftDown(array, start, low + count, low));
        }
    }

    IEnumerator SiftDown(ValueVisualiser[] array, int root, int end, int low)
    {
        while (LeftChild(root - low) < end - low)
        {
            yield return new WaitForFixedUpdate();

            int child = LeftChild(root - low) + low;
            ColorArea(root, child, Color.yellow, 0);
            if (child + 1 < end && array[child] < array[child + 1]) { child++; }

            if (array[root] < array[child])
            {
                array[root].ChangeColor(Color.red, 0);
                array[child].ChangeColor(Color.red, 0);
                SwapTwoElementsByIndex(root, child);
                root = child;
            }
            else
            {
                yield break;
            }
        }
    }

    IEnumerator Partition(ValueVisualiser[] array, int low, int high)
    {
        ValueVisualiser pivot = array[high];
        pivot.ChangeColor(Color.red, 0);
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            yield return new WaitForFixedUpdate();
            pivot.ChangeColor(Color.red, 0);
            if (array[j] < pivot)
            {
                i++;
                ColorArea(i, j, Color.yellow, 0);
                array[j].ChangeColor(Color.red, 0);
                array[i].ChangeColor(Color.red, 0);
                SwapTwoElementsByIndex(i, j);
            }
        }
        yield return new WaitForFixedUpdate();
        array[i + 1].ChangeColor(Color.red, 0);
        array[high].ChangeColor(Color.red, 0);
        SwapTwoElementsByIndex(i + 1, high);
        partitionIndex = i + 1;
    }

    private static int Parent(int i)
    {
        return (i - 1) / 2;
    }

    private static int LeftChild(int i)
    {
        return 2 * i + 1;
    }
}
