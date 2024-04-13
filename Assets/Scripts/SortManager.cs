using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    public int howManyValues = 500;

    public static ValueVisualBehavior[] sortingValues;
    public GameObject valueVisualPrefab;

    float _startX;

    void Start()
    {
        sortingValues = new ValueVisualBehavior[howManyValues];

        Time.timeScale = 1f;

        float totalWidth = howManyValues * 0.03f;

        // Calculate starting position for the first line
        _startX = -(totalWidth / 2f) + (0.03f / 2f);

        // Loop to spawn lines
        for (int i = 0; i < howManyValues; i++)
        {
            // Calculate position for the current line
            float xPos = _startX + (i * 0.03f);

            // Spawn line prefab at the calculated position
            sortingValues[i] = Instantiate(valueVisualPrefab, new Vector3(xPos, -5f, 0f), Quaternion.identity).GetComponent<ValueVisualBehavior>();
            sortingValues[i].visualiserValue = Random.Range(1, 100);
        }

        gameObject.AddComponent<SelectionSort>().Init(sortingValues);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < howManyValues; i++)
        {
            // Calculate position for the current line
            float xPos = _startX + (i * 0.03f);
            sortingValues[i].gameObject.transform.localPosition = new Vector3(
                xPos, sortingValues[i].gameObject.transform.localPosition.y, sortingValues[i].gameObject.transform.localPosition.z);
        }
    }
}
