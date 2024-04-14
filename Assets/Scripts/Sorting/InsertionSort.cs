using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSort : SortingAlgorithm
{

    protected override IEnumerator SortingCoroutine()
    {
        float timer = 0;

        int j=_valueCount-1;

        for (int i = 1; i < _valueCount; i++)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;

            j = i;

            while(j > 0 && _toSort[j-1].visualiserValue > _toSort[j].visualiserValue)
            {
                yield return new WaitForFixedUpdate();

                _toSort[j].ChangeColor(Color.red, 0);

                SwapTwoElementsByIndex(j - 1, j);
                if (i == _valueCount - 1) { _toSort[j].ChangeColor(Color.green); }
                j--;
            }
        }

        while(j >= 0)
        {
            yield return new WaitForFixedUpdate();
            _toSort[j].ChangeColor(Color.green);
            j--;
        }

        _sortManager.SortFinished(_sortIndex);
    }
  
}
