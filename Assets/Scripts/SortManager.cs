using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    [SerializeField] UIManager _uiManager;

    [Header("Sorting values visualisation:")]
    public int howManyValues = 500;
    public GameObject valueVisualPrefab;
    public ValueVisualBehavior[] sortingValues;

    [Header("Sort types options:")]
    public int[] choosenSortTypes = { 0, 0 };
    public bool isComparingSorts = false;

    [Header("Sorting algorithms")]
    public SortsList _sortsList;

    [Header("Sorts on going")]
    public bool isSort1OnGoing = false;
    public bool isSort2OnGoing = false;

    float _startX;

    void Start()
    {
        float totalWidth = howManyValues * 0.03f;
        _startX = -(totalWidth / 2f) + (0.03f / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSort1OnGoing)
        {
            UpdateVisualValuesPositions(1);
        }
        if (isSort2OnGoing)
        {
            UpdateVisualValuesPositions(2);
        }
    }

    void UpdateVisualValuesPositions(int valuesArrayIndex)
    {
        for (int i = 0; i < howManyValues; i++)
        {
            // Calculate position for the current line
            float xPos = _startX + (i * 0.03f);
            sortingValues[i].gameObject.transform.localPosition = new Vector3(
                xPos, sortingValues[i].gameObject.transform.localPosition.y, sortingValues[i].gameObject.transform.localPosition.z);
        }
    }

    public void StartSorting()
    {
        if(sortingValues.Length > 0) 
        {
            foreach (ValueVisualBehavior vvb in sortingValues)
            {
                Destroy(vvb.gameObject);
            }
        }

        sortingValues = new ValueVisualBehavior[howManyValues];

        for (int i = 0; i < howManyValues; i++)
        {
            float xPos = _startX + (i * 0.03f);

            sortingValues[i] = Instantiate(valueVisualPrefab, new Vector3(xPos, -5f, 0f), Quaternion.identity).GetComponent<ValueVisualBehavior>();
            sortingValues[i].visualiserValue = Random.Range(1, 101);
        }

        _sortsList.sortingAlgorithms[choosenSortTypes[0]].Init(this, 1, sortingValues);
    }

    public void SortFinished(int sortIndex)
    {
        if (sortIndex == 1) { isSort1OnGoing = false; UpdateVisualValuesPositions(1); }
        if (sortIndex == 2) { isSort2OnGoing = false; UpdateVisualValuesPositions(2); }

        if (isComparingSorts && !isSort1OnGoing && !isSort2OnGoing)
        {
            _uiManager.SortingFinished();
        }

        if(!isComparingSorts && !isSort1OnGoing)
        {
            _uiManager.SortingFinished();
        }
    }
}
