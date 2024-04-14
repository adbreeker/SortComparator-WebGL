using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortsList : MonoBehaviour
{
    public List<SortingAlgorithm> sortingAlgorithms = new List<SortingAlgorithm>();

    public List<string> GetSortsNames()
    {
        List<string> names = new List<string>();
        foreach (SortingAlgorithm algorithm in sortingAlgorithms)
        {
            string name = algorithm.GetType().Name;

            string nameModified = name[0].ToString();
            for(int i = 1; i<name.Length; i++)
            {
                if (char.IsUpper(name[i]))
                {
                    nameModified += " ";
                }
                nameModified += name[i];
            }

            names.Add(nameModified);
        }
        return names;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        FindObjectOfType<UIManager>().SetSortsDropdownsOptions(GetSortsNames());
        //foreach(string name in GetSortsNames()) { Debug.Log(name); }
    }
#endif
}
