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
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;
            Debug.Log(timer);
            int minIndex = i;

            _toSort[i].ChangeColor(Color.red);

            for(int j=i+1; j < _valueCount; j++)
            {
                yield return new WaitForFixedUpdate();

                _toSort[j].ChangeColor(Color.red, 0);

                if (_toSort[j].visualiserValue < _toSort[minIndex].visualiserValue)
                {
                    minIndex = j;
                }
            }

            yield return new WaitForFixedUpdate();
            SwapTwoElementsByIndex(i, minIndex);
        }

        _toSort[_valueCount-1].ChangeColor(Color.green);
    }

    void SwapTwoElementsByIndex(int a, int b)
    {
        if(a != b)
        {
            ValueVisualBehavior temp = _toSort[a];
            _toSort[a] = _toSort[b];
            _toSort[b] = temp;

            _toSort[a].ChangeColor(Color.green);
            _toSort[b].ChangeColor(Color.white);
        }
        else
        {
            _toSort[a].ChangeColor(Color.green);
        }
    }
}
