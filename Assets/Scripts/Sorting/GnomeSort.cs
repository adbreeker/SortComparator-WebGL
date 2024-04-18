using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeSort : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        int pos = 0;
        while(pos < _valueCount)
        {
            _toSort[pos].ChangeColor(Color.red, 0);
            yield return new WaitForFixedUpdate();
            if(pos == 0 || _toSort[pos] >= _toSort[pos-1])
            {
                _toSort[pos].ChangeColor(Color.yellow);
                pos++;
            }
            else
            {
                SwapTwoElementsByIndex(pos, pos - 1);
                pos--;
            }
        }

        ColorArea(0, pos-1, Color.green);
        _sortManager.SortFinished(_sortIndex);
    }
}
