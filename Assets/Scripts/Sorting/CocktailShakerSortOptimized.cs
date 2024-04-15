using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CocktailShakerSortOptimized : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        bool swapped = false;
        //bounds between which the algorithm operates, (the bound is new start point, not last index before start point)
        int boundTop = _valueCount - 1;
        int boundBot = 0;
        do
        {
            yield return new WaitForFixedUpdate();

            int newBound = boundTop;
            for(int i = boundBot; i<boundTop; i++)
            {
                yield return new WaitForFixedUpdate();
                _toSort[i].ChangeColor(Color.red, 0);
                if (_toSort[i] > _toSort[i+1])
                {
                    newBound = i;
                    SwapTwoElementsByIndex(i, i + 1);
                    swapped = true;
                }
            }
            ColorArea(boundTop, newBound + 1, Color.green);
            boundTop = newBound;

            if(!swapped)
            {
                break;
            }

            swapped = false;
            newBound = boundBot;
            for(int i = boundTop; i>boundBot; i--)
            {
                yield return new WaitForFixedUpdate();
                _toSort[i].ChangeColor(Color.red, 0);
                if (_toSort[i] < _toSort[i-1])
                {
                    newBound = i;
                    SwapTwoElementsByIndex(i, i-1);
                    swapped = true;
                }
            }
            ColorArea(boundBot, newBound - 1, Color.green);
            boundBot = newBound;
            
        } while (swapped);
        ColorArea(boundBot, boundTop, Color.green);

        _sortManager.SortFinished(_sortIndex);
    }
}
