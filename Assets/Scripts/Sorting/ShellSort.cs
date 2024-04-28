using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSort : SortingAlgorithm
{
    protected override IEnumerator SortingCoroutine()
    {
        int[] gaps = GetGaps();
        foreach (int gap in gaps) 
        {
            yield return new WaitForFixedUpdate();
            for (int i = gap; i<_valueCount; i++)
            {
                _toSort[i].ChangeColor(Color.red, 0);
                yield return new WaitForFixedUpdate();
                ValueVisualiser temp = _toSort[i];
                int j;
                for(j = i; (j>=gap) && (_toSort[j-gap] > temp); j-= gap)
                {
                    ColorArea(j, j - gap, Color.yellow, 0);
                    _toSort[j].ChangeColor(Color.red, 0);
                    _toSort[j - gap].ChangeColor(Color.red, 0);
                    yield return new WaitForFixedUpdate();
                    _toSort[j] = _toSort[j-gap];
                }
                _toSort[j] = temp;
            }
        }
        ColorArea(0, _valueCount - 1, Color.green);
        _sortManager.SortFinished(_sortIndex);
    }

    int[] GetGaps()
    {
        if(_valueCount < 1000)
        {
            int[] gapsCiura = { 701, 301, 132, 57, 23, 10, 4, 1 };
            return gapsCiura;
        }
        else
        {
            float gamma = 2.2436f;
            List<int> gapsLee = new List<int>();
            for(int k = 1; gapsLee.Count == 0 || gapsLee[k-2] < _valueCount; k++)
            {
                int gap = Mathf.CeilToInt((Mathf.Pow(gamma, k) - 1) / (gamma - 1));
                gapsLee.Add(gap);
            }
            gapsLee.Reverse();
            return gapsLee.ToArray();
        }
    }
}
