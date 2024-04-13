using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionSort : MonoBehaviour
{
    
    ValueVisualBehavior[] _toSort;
    int _valueCount;

    public void Init(ValueVisualBehavior[] values)
    {
        _toSort = values;
        _valueCount = values.Length;
        StartCoroutine(SelectionSortCorutine());
    }

    IEnumerator SelectionSortCorutine()
    {
        float timer = 0;

        for(int i=0; i < _valueCount - 1; i++) 
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            Debug.Log(timer);
            int minIndex = i;

            for(int j=i+1; j < _valueCount; j++)
            {
                if (_toSort[j].visualiserValue <= _toSort[minIndex].visualiserValue)
                {
                    minIndex = j;
                }
            }

            if (i != minIndex) { SwapTwoElementsByIndex(i, minIndex); }
        }
    }

    void SwapTwoElementsByIndex(int a, int b)
    {
        ValueVisualBehavior temp = _toSort[a];
        _toSort[a] = _toSort[b];
        _toSort[b] = temp;
    }
}
