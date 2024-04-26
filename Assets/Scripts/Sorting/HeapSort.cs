using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeapSort : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        int count = _valueCount;
        yield return StartCoroutine(Heapify(_toSort, count));

        int end = _valueCount - 1;
        while(end > 0)
        {
            yield return new WaitForFixedUpdate();
            SwapTwoElementsByIndex(end, 0);
            _toSort[end].ChangeColor(Color.green);
            yield return StartCoroutine(SiftDown(_toSort, 0, end));
            end--;
        }
        _toSort[0].ChangeColor(Color.green);
        _sortManager.SortFinished(_sortIndex);
    }

    IEnumerator Heapify(ValueVisualiser[] array, int count)
    {
        int start = Parent(count - 1) + 1;

        while (start > 0)
        {
            yield return new WaitForFixedUpdate();
            start--;
            yield return StartCoroutine(SiftDown(array, start, count));
        }
    }

    IEnumerator SiftDown(ValueVisualiser[] array, int root, int end)
    {
        while (LeftChild(root) < end)
        {
            yield return new WaitForFixedUpdate();

            int child = LeftChild(root);
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

    private static int Parent(int i)
    {
        return (i - 1) / 2;
    }

    private static int LeftChild(int i)
    {
        return 2 * i + 1;
    }
}
