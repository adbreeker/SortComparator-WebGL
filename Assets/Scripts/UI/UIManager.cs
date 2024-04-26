using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [SerializeField] SortManager _sortManager;

    [Header("UI options:")]
    [SerializeField] Slider _playSpeedSlider;
    [SerializeField] TMP_Dropdown _sort1dropdown;
    [SerializeField] TMP_Dropdown _sort2dropdown;
    [SerializeField] Toggle _compareSortsToggle;

    [Header("UI elements")]
    [SerializeField] GameObject _startButton;
    [SerializeField] GameObject _splitLine;
    [SerializeField] GameObject _startButtonSplitline;
    [SerializeField] GameObject _sortingOptionsGroup;

    void Update()
    {
        
    }

    public void SetPlaySpeed()
    {
        if (_playSpeedSlider.value == 0) { Time.timeScale = 1.0f; }
        if (_playSpeedSlider.value > 0) { Time.timeScale = 1.0f + _playSpeedSlider.value * 0.05f; }
        if (_playSpeedSlider.value < 0) { Time.timeScale = 1.0f + (_playSpeedSlider.value * 0.001f); }
    }

    public void SetSortsDropdownsOptions(List<string> sortNames)
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        foreach (string name in sortNames) 
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = name;
            options.Add(option);
        }

        _sort1dropdown.options = options;
        _sort2dropdown.options = options;
    }

    public void ChangeSort1DropdownOption()
    {
        _sortManager.choosenSortTypes[0] = _sort1dropdown.value;
        _sortManager.ClearValueVisualisers();
    }

    public void ChangeSort2DropdownOption()
    {
        _sortManager.choosenSortTypes[1] = _sort2dropdown.value;
        _sortManager.ClearValueVisualisers();
    }

    public void ComparingTwoSortsUIChange()
    {
        if(_compareSortsToggle.isOn) 
        {
            _sort2dropdown.gameObject.SetActive(true);
            RectTransform rt = _compareSortsToggle.gameObject.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -110);

            _splitLine.gameObject.SetActive(true);
            _startButton.gameObject.SetActive(false);

            _sortManager.isComparingSorts = true;
        }
        else
        {
            _sort2dropdown.gameObject.SetActive(false);
            RectTransform rt = _compareSortsToggle.gameObject.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -70);

            _splitLine.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(true);

            _sortManager.isComparingSorts = false;
        }

        _sortManager.ClearValueVisualisers();
    }

    public void StartSortingButton()
    {
        if (_compareSortsToggle.isOn) { _startButtonSplitline.SetActive(false); }
        else { _startButton.SetActive(false); }
        _sortManager.StartSorting();

        _sortingOptionsGroup.SetActive(false);
    }

    public void SortingFinished()
    {
        _sortingOptionsGroup.SetActive(true);

        if (_compareSortsToggle.isOn) { _startButtonSplitline.SetActive(true); }
        else { _startButton.SetActive(true); }
    }
}
