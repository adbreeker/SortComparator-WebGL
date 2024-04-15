using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionSort : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        for (int i = 0; i < _valueCount - 1; i++)
        {
            yield return new WaitForFixedUpdate();

            int minIndex = i;

            _toSort[i].ChangeColor(Color.red);

            for (int j = i + 1; j < _valueCount; j++)
            {
                yield return new WaitForFixedUpdate();

                _toSort[j].ChangeColor(Color.red, 0);

                if (_toSort[j] < _toSort[minIndex])
                {
                    minIndex = j;
                }
            }

            yield return new WaitForFixedUpdate();
            if (i != minIndex)
            {
                SwapTwoElementsByIndex(i, minIndex);
                _toSort[i].ChangeColor(Color.green);
                _toSort[minIndex].ChangeColor(Color.white);
            }
            else { _toSort[minIndex].ChangeColor(Color.green); }
        }

        _toSort[_valueCount - 1].ChangeColor(Color.green);

        _sortManager.SortFinished(_sortIndex);
    }

}
