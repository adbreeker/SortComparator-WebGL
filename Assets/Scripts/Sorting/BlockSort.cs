using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSort : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        int power_of_two = FloorPowerOfTwo(_toSort.Length);
        float scale = _toSort.Length / (float)power_of_two;

        // Insertion sort 16–31 items at a time
        for (int merge = 0; merge < power_of_two; merge += 16)
        {
            yield return new WaitForFixedUpdate();
            int start = Mathf.FloorToInt(merge * scale);
            int end = Mathf.FloorToInt((merge + 16) * scale);
            yield return StartCoroutine(InsertionSort(start, end));
        }

        for (int length = 16; length < power_of_two; length += length)
        {
            yield return new WaitForFixedUpdate();
            for (int merge = 0; merge < power_of_two; merge += length * 2)
            {
                yield return new WaitForFixedUpdate();
                int start = Mathf.FloorToInt(merge * scale);
                int mid = Mathf.FloorToInt((merge + length) * scale);
                int end = Mathf.FloorToInt((merge + length * 2) * scale);

                if (_toSort[end - 1] < _toSort[start])
                {
                    // The two ranges are in reverse order, so a rotation is enough to merge them
                    yield return StartCoroutine(Rotate(mid - start, start, end));
                }
                else if (_toSort[mid - 1] > _toSort[mid])
                {
                    yield return StartCoroutine(Merge(start, mid, end));
                }
                // else the ranges are already correctly ordered
            }
        }

        ColorArea(0, _valueCount - 1, Color.green);
        _sortManager.SortFinished(_sortIndex);
    }

    IEnumerator InsertionSort(int start, int end)
    {
        for (int i = start + 1; i < end; i++)
        {
            yield return new WaitForFixedUpdate();
            ValueVisualiser current = _toSort[i];
            int j = i - 1;
            while (j >= start && _toSort[j] > current)
            {
                yield return new WaitForFixedUpdate();
                _toSort[j + 1] = _toSort[j];
                j--;
            }
            _toSort[j + 1] = current;
        }
    }

    IEnumerator Rotate(int d, int start, int end)
    {
        yield return new WaitForFixedUpdate();
        d %= (end - start);
        if (d < 0)
            d += (end - start);

        yield return StartCoroutine(Reverse(start, start + d));
        yield return StartCoroutine(Reverse(start + d, end));
        yield return StartCoroutine(Reverse(start, end));
    }

    IEnumerator Reverse(int start, int end)
    {
        end--;
        while (start < end)
        {
            yield return new WaitForFixedUpdate();
            SwapTwoElementsByIndex(start++, end--);
        }
    }

    IEnumerator Merge(int start, int mid, int end)
    {
        int n1 = mid - start;
        int n2 = end - mid;

        ValueVisualiser[] leftArray = new ValueVisualiser[n1];
        ValueVisualiser[] rightArray = new ValueVisualiser[n2];

        for (int i = 0; i < n1; i++)
        {
            yield return new WaitForFixedUpdate();
            leftArray[i] = _toSort[start + i];
        }
           

        for (int j = 0; j < n2; j++)
        {
            yield return new WaitForFixedUpdate();
            rightArray[j] = _toSort[mid + j];
        }
            

        int x = 0, y = 0, k = start;

        while (x < n1 && y < n2)
        {
            yield return new WaitForFixedUpdate();
            if (leftArray[x] <= rightArray[y])
                _toSort[k++] = leftArray[x++];
            else
                _toSort[k++] = rightArray[y++];
        }

        while (x < n1)
        {
            yield return new WaitForFixedUpdate();
            _toSort[k++] = leftArray[x++];
        }
            

        while (y < n2)
        {
            yield return new WaitForFixedUpdate();
            _toSort[k++] = rightArray[y++];
        }
            
    }

    int FloorPowerOfTwo(int num)
    {
        int power = 1;
        while (power * 2 <= num)
        {
            power *= 2;
        }
        return power;
    }
}
