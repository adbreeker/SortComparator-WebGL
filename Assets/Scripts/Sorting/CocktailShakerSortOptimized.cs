using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CocktailShakerSortOptimized : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        //bounds between which the algorithm operates, (the bound is new start point, not last index before start point)
        int boundTop = _valueCount - 1;
        int boundBot = 0;
        while(boundBot < boundTop)
        {
            yield return new WaitForFixedUpdate();

            int newBound = boundBot;
            for(int i = boundBot; i<boundTop; i++)
            {
                yield return new WaitForFixedUpdate();
                _toSort[i].ChangeColor(Color.red, 0);
                if (_toSort[i] > _toSort[i+1])
                {
                    newBound = i + 1;
                    SwapTwoElementsByIndex(i, i + 1);
                }
            }
            ColorArea(boundTop, newBound, Color.green);
            boundTop = newBound - 1;

            for(int i = boundTop; i>boundBot; i--)
            {
                yield return new WaitForFixedUpdate();
                _toSort[i].ChangeColor(Color.red, 0);
                if (_toSort[i] < _toSort[i-1])
                {
                    newBound = i - 1;
                    SwapTwoElementsByIndex(i, i-1);
                }
            }
            ColorArea(boundBot, newBound, Color.green);
            boundBot = newBound + 1;
            
        }
        ColorArea(boundBot, boundTop, Color.green);

        _sortManager.SortFinished(_sortIndex);
    }
}
