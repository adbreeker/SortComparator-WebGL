using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        int boundTop = _valueCount - 1;
        while (boundTop > 0)
        {
            yield return new WaitForFixedUpdate();
            int newBoundTop = 0;
            for (int i = 0; i < boundTop; i++)
            {
                yield return new WaitForFixedUpdate();
                _toSort[i].ChangeColor(Color.red, 0);
                if (_toSort[i] > _toSort[i + 1])
                {
                    newBoundTop = i + 1;
                    SwapTwoElementsByIndex(i, i + 1);
                }
            }
            ColorArea(boundTop, newBoundTop, Color.green);
            boundTop = newBoundTop;
        }

        _sortManager.SortFinished(_sortIndex);
    }

    IEnumerator SortingCoroutineUnoptimized()
    {
        bool swapped;
        do
        {
            swapped = false;
            yield return new WaitForFixedUpdate();
            for(int i = 0; i<_valueCount-1; i++)
            {
                yield return new WaitForFixedUpdate();
                _toSort[i].ChangeColor(Color.red, 0);
                if (_toSort[i] > _toSort[i+1])
                {
                    swapped = true;
                    SwapTwoElementsByIndex(i, i + 1);
                }
            }
        }while (swapped);
        ColorArea(0, _valueCount-1, Color.green);
        _sortManager.SortFinished(_sortIndex);
    }
}
