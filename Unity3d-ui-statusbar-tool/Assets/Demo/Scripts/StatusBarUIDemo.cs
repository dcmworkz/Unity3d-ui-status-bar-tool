using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lairinus.UI;
using UnityEngine.UI;

public class StatusBarUIDemo : MonoBehaviour
{
    /*
     * This script is for DEMO PURPOSES ONLY to show very basic use cases.
     */

    private int _currentCountingValue = 0;
    private int _currentValue = 1000;
    private int _maxCountingValue = 0;
    private int _maxValue = 1000;
    [SerializeField] private List<UIStatusBar> _allStatusBars = new List<UIStatusBar>();
    [SerializeField] private Button _barType_QuantityButton = null;
    [SerializeField] private Button _barType_SimpleFillButton = null;
    [SerializeField] private Button _barType_SpriteButton = null;
    [SerializeField] private InputField _input_MiddleText = null;
    [SerializeField] private InputField _input_PostfixText = null;
    [SerializeField] private InputField _input_PrefixText = null;
    [SerializeField] private GameObject _quantityStatusBar = null;
    [SerializeField] private Button _setValue100PercentButton = null;
    [SerializeField] private Button _setValue10PercentButton = null;
    [SerializeField] private GameObject _simpleFillStatusBar = null;
    [SerializeField] private GameObject _spriteStatusBar = null;
    [SerializeField] private Button _textSetting_ValueButton = null;
    [SerializeField] private Button _textSetting_ValueOfMaxButton = null;
    [SerializeField] private Button _textSetting_ValuePercentageButton = null;

    private void Awake()
    {
        _setValue10PercentButton.onClick.AddListener(() => HandleOnClick_SetValue10Percent());
        _setValue100PercentButton.onClick.AddListener(() => HandleOnClick_SetValue100Percent());
        _barType_SimpleFillButton.onClick.AddListener(() => HandleOnClick_SimpleFill());
        _barType_SpriteButton.onClick.AddListener(() => HandleOnClick_Sprite());
        _barType_QuantityButton.onClick.AddListener(() => HandleOnClick_Quantity());
        _textSetting_ValueButton.onClick.AddListener(() => HandleOnClick_SetValueText());
        _textSetting_ValuePercentageButton.onClick.AddListener(() => HandleOnClick_SetValuePercentageText());
        _textSetting_ValueOfMaxButton.onClick.AddListener(() => HandleOnClick_SetValueOfMaxText());
        _input_PrefixText.onValueChanged.AddListener(delegate { HandleOnChanged_PrefixText(); });
        _input_PostfixText.onValueChanged.AddListener(delegate { HandleOnChanged_PostfixText(); });
        _input_MiddleText.onValueChanged.AddListener(delegate { HandleOnChanged_MiddleText(); });
        _input_PrefixText.text = "";
        _input_MiddleText.text = " of ";
        _input_PostfixText.text = "";
        ResetStatusBarTexts();
    }

    private void HandleOnChanged_MiddleText()
    {
        _allStatusBars.ForEach(x => x.middleText = _input_MiddleText.text);
    }

    private void HandleOnChanged_PostfixText()
    {
        _allStatusBars.ForEach(x => x.postfixText = _input_PostfixText.text);
    }

    private void HandleOnChanged_PrefixText()
    {
        _allStatusBars.ForEach(x => x.prefixText = _input_PrefixText.text);
    }

    private void HandleOnClick_Quantity()
    {
        _simpleFillStatusBar.SetActive(false);
        _spriteStatusBar.SetActive(false);
        _quantityStatusBar.SetActive(true);
        _input_PrefixText.text = "x ";
        _input_PostfixText.text = " Lives left";
    }

    private void HandleOnClick_SetValue100Percent()
    {
        _currentValue = _maxValue;
    }

    private void HandleOnClick_SetValue10Percent()
    {
        _currentValue = (int)(_maxValue * 0.1F);
    }

    private void HandleOnClick_SetValueOfMaxText()
    {
        _allStatusBars.ForEach(x => x.statusTextDisplayType = UIStatusBar.TextDisplayType.CurrentValueOfMax);
        ResetStatusBarTexts();
        _input_MiddleText.text = " of ";
        _input_PostfixText.text = "";
        _input_PrefixText.text = "";
    }

    private void HandleOnClick_SetValuePercentageText()
    {
        _allStatusBars.ForEach(x => x.statusTextDisplayType = UIStatusBar.TextDisplayType.CurrentValuePercentage);
        ResetStatusBarTexts();
        _input_MiddleText.text = "";
        _input_PostfixText.text = "%";
        _input_PrefixText.text = "";
    }

    private void HandleOnClick_SetValueText()
    {
        _allStatusBars.ForEach(x => x.statusTextDisplayType = UIStatusBar.TextDisplayType.CurrentValue);
        ResetStatusBarTexts();
        _input_MiddleText.text = "";
        _input_PostfixText.text = "";
        _input_PrefixText.text = "";
    }

    private void HandleOnClick_SimpleFill()
    {
        _simpleFillStatusBar.SetActive(true);
        _spriteStatusBar.SetActive(false);
        _quantityStatusBar.SetActive(false);
        _input_PrefixText.text = "";
        _input_PostfixText.text = "";
        _input_MiddleText.text = " of ";
    }

    private void HandleOnClick_Sprite()
    {
        _simpleFillStatusBar.SetActive(false);
        _spriteStatusBar.SetActive(true);
        _quantityStatusBar.SetActive(false);
        _input_PrefixText.text = "";
        _input_PostfixText.text = "";
        _input_MiddleText.text = " of ";
    }

    private void ResetStatusBarTexts()
    {
        _allStatusBars.ForEach(x => x.prefixText = _input_PrefixText.text);
        _allStatusBars.ForEach(x => x.postfixText = _input_PostfixText.text);
        _allStatusBars.ForEach(x => x.middleText = " of ");
        _allStatusBars.ForEach(x => x.usePrefixText = true);
        _allStatusBars.ForEach(x => x.usePostfixText = true);
    }

    private void Update()
    {
        if (_currentValue > 0)
            _currentValue--;

        _allStatusBars.ForEach(x => x.UpdateStatusBar(_currentValue, _maxValue));
    }
}