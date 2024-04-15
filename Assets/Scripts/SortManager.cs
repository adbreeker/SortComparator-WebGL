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
    public ValueVisualBehavior[] sortingValues1;
    public ValueVisualBehavior[] sortingValues2;
    public Transform valuesHolderSolo;
    public Transform valuesHolderCompare1;
    public Transform valuesHolderCompare2;

    [Header("Sort types options:")]
    public int[] choosenSortTypes = { 0, 0 };
    public bool isComparingSorts = false;
    SortingAlgorithm _additionalSortingComponent;

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
        if(valuesArrayIndex == 1)
        {
            for (int i = 0; i < howManyValues; i++)
            {
                // Calculate position for the current line
                float xPos = _startX + (i * 0.03f);
                sortingValues1[i].gameObject.transform.localPosition = new Vector3(
                    xPos, sortingValues1[i].gameObject.transform.localPosition.y, sortingValues1[i].gameObject.transform.localPosition.z);
            }
        }
        if (valuesArrayIndex == 2)
        {
            for (int i = 0; i < howManyValues; i++)
            {
                // Calculate position for the current line
                float xPos = _startX + (i * 0.03f);
                sortingValues2[i].gameObject.transform.localPosition = new Vector3(
                    xPos, sortingValues2[i].gameObject.transform.localPosition.y, sortingValues2[i].gameObject.transform.localPosition.z);
            }
        }
    }

    public void StartSorting()
    {
        ClearValueVisualisers();

        if(!isComparingSorts)
        {
            sortingValues1 = new ValueVisualBehavior[howManyValues];
            for (int i = 0; i < howManyValues; i++)
            {
                float xPos = _startX + (i * 0.03f);

                sortingValues1[i] = Instantiate(valueVisualPrefab, new Vector3(xPos, -5f, 0f), Quaternion.identity, valuesHolderSolo).GetComponent<ValueVisualBehavior>();
                sortingValues1[i].visualiserValue = Random.Range(1, 101);
            }

            _sortsList.sortingAlgorithms[choosenSortTypes[0]].Init(this, 1, sortingValues1);
        }
        else
        {
            sortingValues1 = new ValueVisualBehavior[howManyValues];
            sortingValues2 = new ValueVisualBehavior[howManyValues];
            for (int i = 0; i < howManyValues; i++)
            {
                float xPos = _startX + (i * 0.03f);

                sortingValues1[i] = Instantiate(valueVisualPrefab, new Vector3(xPos, 0f, 0f), Quaternion.identity, valuesHolderCompare1).GetComponent<ValueVisualBehavior>();
                sortingValues1[i].visualiserValue = Random.Range(1, 101);
                sortingValues2[i] = Instantiate(valueVisualPrefab, new Vector3(xPos, -5f, 0f), Quaternion.identity, valuesHolderCompare2).GetComponent<ValueVisualBehavior>();
                sortingValues2[i].visualiserValue = sortingValues1[i].visualiserValue;
            }
            _additionalSortingComponent = (SortingAlgorithm)gameObject.AddComponent(_sortsList.sortingAlgorithms[choosenSortTypes[1]].GetType());
            _sortsList.sortingAlgorithms[choosenSortTypes[0]].Init(this, 1, sortingValues1);
            _additionalSortingComponent.Init(this, 2, sortingValues2);
        }
    }

    public void ClearValueVisualisers()
    {
        if (sortingValues1.Length > 0)
        {
            foreach (ValueVisualBehavior vvb in sortingValues1)
            {
                Destroy(vvb.gameObject);
            }
            sortingValues1 = new ValueVisualBehavior[0];
        }
        if (sortingValues2.Length > 0)
        {
            foreach (ValueVisualBehavior vvb in sortingValues2)
            {
                Destroy(vvb.gameObject);
            }
            sortingValues2 = new ValueVisualBehavior[0];
        }
    }


    public void SortFinished(int sortIndex)
    {
        if (sortIndex == 1) { isSort1OnGoing = false; UpdateVisualValuesPositions(1); }
        if (sortIndex == 2) { isSort2OnGoing = false; UpdateVisualValuesPositions(2); }

        if (isComparingSorts && !isSort1OnGoing && !isSort2OnGoing)
        {
            _uiManager.SortingFinished();
            Destroy(_additionalSortingComponent);
        }

        if(!isComparingSorts && !isSort1OnGoing)
        {
            _uiManager.SortingFinished();
        }
    }
}
