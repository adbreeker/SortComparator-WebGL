using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MergeSort : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        yield return StartCoroutine(MergeSortCoroutine(0, _valueCount - 1));
        ColorArea(0, _valueCount - 1, Color.green);
        _sortManager.SortFinished(_sortIndex);
    }

    IEnumerator MergeSortCoroutine(int left, int right)
    {
        yield return new WaitForFixedUpdate();
        if (left < right)
        {
            int mid = (left + right) / 2;

            // Sort first and second halves
            yield return StartCoroutine(MergeSortCoroutine(left, mid));
            yield return StartCoroutine(MergeSortCoroutine( mid + 1, right));

            // Merge the sorted halves
            yield return StartCoroutine(MergeCoroutine(left, mid, right));
        }
    }

    IEnumerator MergeCoroutine(int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        ValueVisualiser[] L = new ValueVisualiser[n1];
        ValueVisualiser[] R = new ValueVisualiser[n2];

        ColorArea(left, right, Color.yellow);

        // Copy data to temp arrays L[] and R[]
        for (int i = 0; i < n1; ++i)
        {
            yield return new WaitForFixedUpdate();
            L[i] = _toSort[left + i];
        }
            
        for (int j = 0; j < n2; ++j)
        {
            yield return new WaitForFixedUpdate();
            R[j] = _toSort[mid + 1 + j];
        }

        int iL = 0, iR = 0;
        int k = left;
        while (iL < n1 && iR < n2)
        {
            yield return new WaitForFixedUpdate();
            if (L[iL] <= R[iR])
            {
                _toSort[k] = L[iL];
                iL++;
            }
            else
            {
                _toSort[k] = R[iR];
                iR++;
            }
            k++;
        }

        // Copy remaining elements of L[] if any
        while (iL < n1)
        {
            yield return new WaitForFixedUpdate();
            _toSort[k] = L[iL];
            iL++;
            k++;
        }

        // Copy remaining elements of R[] if any
        while (iR < n2)
        {
            yield return new WaitForFixedUpdate();
            _toSort[k] = R[iR];
            iR++;
            k++;
        }

        ColorArea(left, right, Color.white);
    }

}
