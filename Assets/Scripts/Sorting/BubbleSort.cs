using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
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
