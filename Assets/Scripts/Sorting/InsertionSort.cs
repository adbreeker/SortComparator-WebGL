using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSort : SortingAlgorithm
{

    protected override IEnumerator SortingCoroutine()
    {
        int j=_valueCount-1;

        for (int i = 1; i < _valueCount; i++)
        {
            yield return new WaitForFixedUpdate();

            j = i;

            while(j > 0 && _toSort[j-1] > _toSort[j])
            {
                yield return new WaitForFixedUpdate();

                _toSort[j].ChangeColor(Color.red, 0);

                SwapTwoElementsByIndex(j - 1, j);
                if (i == _valueCount - 1) { _toSort[j].ChangeColor(Color.green); }
                j--;
            }
            if (i != _valueCount - 1) { ColorArea(j, i, Color.yellow); }
        }

        ColorArea(j, 0, Color.green);

        _sortManager.SortFinished(_sortIndex);
    }
  
}
