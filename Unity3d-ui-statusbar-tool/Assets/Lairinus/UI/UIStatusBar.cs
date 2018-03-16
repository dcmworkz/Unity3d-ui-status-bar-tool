using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lairinus.UI
{
    public class UIStatusBar : MonoBehaviour
    {
        public enum StatusBarType
        {
            SimpleFill,
            Quantity,
            SeparateSprites
        }

        public enum TextDisplayType
        {
            None,
            Quantity,
            CurrentValue,
            CurrentValuePercentage,
            CurrentValueOfMax,
        }

        public Image currentValueImage { get { return _currentValueImage; } set { _currentValueImage = value; } }

        public bool enableDebugging { get { return _enableDebugging; } set { _enableDebugging = value; } }

        public Image.FillMethod fillMethod { get { return _fillMethod; } set { _fillMethod = value; } }

        public int fillOrigin { get { return _fillOrigin; } set { _fillOrigin = value; } }

        public float lastingValueUpdateRate { get { return _lingeringValueSpeed; } }

        public float lingeringValueSpeed { get { return _lingeringValueSpeed; } set { _lingeringValueSpeed = value; } }

        public string middleText { get { return _middleText; } set { _middleText = value; } }

        public string postfixText { get { return _postfixText; } set { _postfixText = value; } }

        public string prefixText { get { return _prefixText; } set { _prefixText = value; } }

        public Sprite quantityIcon { get { return _quantityIcon; } set { _quantityIcon = value; } }

        public List<Sprite> separateSprites { get { return _separateSprites; } }

        public bool showLingeringValue { get { return _showLingeringValue; } set { _showLingeringValue = value; } }

        public StatusBarType statusBarType { get { return _statusBarType; } set { _statusBarType = value; } }

        public TextDisplayType statusTextDisplayType { get { return _statusTextDisplayType; } set { _statusTextDisplayType = value; } }

        public bool usePostfixText { get { return _usePostfixText; } set { _usePostfixText = value; } }

        public bool usePrefixText { get { return _usePrefixText; } set { _usePrefixText = value; } }

        public Image valueLingeringImage { get { return _lingeringValueImage; } set { _lingeringValueImage = value; } }

        public Text valueText { get { return _valueText; } set { _valueText = value; } }

        public void UpdateStatusBar(float currentValue, float maxValue)
        {
            /*
             * External use
             * -------------------------
             * Provides updates to the Status Bar based on the current and maximum values.
             */

            float currentPercentage = (currentValue / maxValue);
            UpdateTextValue(currentValue, maxValue, currentPercentage);
            switch (_statusBarType)
            {
                case StatusBarType.SimpleFill:
                    {
                        SimpleFill_FillStatusBar(currentPercentage);
                        SimpleFill_UpdateLingeringFillValue(currentPercentage);
                    }
                    break;

                case StatusBarType.Quantity:
                    {
                        Quantity_UpdateValue(currentValue, maxValue, currentPercentage);
                    }
                    break;

                case StatusBarType.SeparateSprites:
                    {
                        SeparateSprites_UpdateValue(currentValue, maxValue, currentPercentage);
                    }
                    break;
            }
        }

        [SerializeField] private Image _currentValueImage = null;

        [SerializeField] private bool _enableDebugging = false;

        [SerializeField] private int _fillOrigin = 0;

        private float _lastLingeringPercentage = 0;

        [SerializeField] private Image _lingeringValueImage = null;

        [SerializeField] private float _lingeringValueSpeed = 0;

        [SerializeField] private string _middleText = "";

        [SerializeField] private string _postfixText = "";

        [SerializeField] private string _prefixText = "";

        [SerializeField] private Sprite _quantityIcon = null;

        [SerializeField] private List<Sprite> _separateSprites = new List<Sprite>();

        [SerializeField] private bool _showLingeringValue = false;

        [SerializeField] private Image.FillMethod _fillMethod = Image.FillMethod.Horizontal;

        [SerializeField] private StatusBarType _statusBarType = StatusBarType.SimpleFill;

        [SerializeField] private TextDisplayType _statusTextDisplayType = TextDisplayType.CurrentValue;

        [SerializeField] private bool _usePostfixText = false;

        [SerializeField] private bool _usePrefixText = false;

        [SerializeField] private Text _valueText = null;

        private void Awake()
        {
            Debug.Log(Debugging.DebuggingEnabled.Replace(Debugging.ReplaceString, gameObject.name));
        }

        private void Quantity_UpdateValue(float currentValue, float maxValue, float currentPercentage)
        {
            if (_valueText == null)
            {
                if (_enableDebugging)
                {
                    Debug.LogWarning(Debugging.TextValueIsNull.Replace(Debugging.ReplaceString, gameObject.name));
                    return;
                }
            }

            if (_currentValueImage == null)
            {
                if (_enableDebugging)
                {
                    Debug.LogWarning(Debugging.CurrentValueImageIsNull.Replace(Debugging.ReplaceString, gameObject.name));
                    return;
                }
            }

            _currentValueImage.sprite = _quantityIcon;
        }

        private void SeparateSprites_UpdateValue(float currentValue, float maxValue, float currentPercentage)
        {
            if (separateSprites == null || separateSprites.Count == 0)
                return;

            int currentRoughIndex = Mathf.CeilToInt((separateSprites.Count - 1) * currentPercentage);
            if (currentRoughIndex >= separateSprites.Count)
                currentRoughIndex = separateSprites.Count - 1;
            else if (currentRoughIndex <= 0)
                currentRoughIndex = 0;

            Sprite desiredSprite = separateSprites[currentRoughIndex];
            if (desiredSprite == null)
            {
                if (_enableDebugging)
                {
                    Debug.LogWarning(Debugging.SpriteIsNull.Replace(Debugging.ReplaceString, gameObject.name));
                    return;
                }
            }

            if (_currentValueImage == null)
            {
                if (_enableDebugging)
                {
                    Debug.LogWarning(Debugging.CurrentValueImageIsNull.Replace(Debugging.ReplaceString, gameObject.name));
                    return;
                }
            }

            _currentValueImage.sprite = desiredSprite;
        }

        private void SimpleFill_FillStatusBar(float currentPercentage)
        {
            /*
             * Internal use
             * -------------------------
             * Updates the Image fill values for this Status Bar
             */

            if (_currentValueImage == null && _enableDebugging)
            {
                Debug.LogWarning(Debugging.CurrentValueImageIsNull.Replace(Debugging.ReplaceString, gameObject.name));
                return;
            }

            if (_currentValueImage.type != Image.Type.Filled)
                _currentValueImage.type = Image.Type.Filled;

            if (_currentValueImage.fillMethod != _fillMethod)
                _currentValueImage.fillMethod = _fillMethod;

            if (_currentValueImage.fillOrigin != _fillOrigin)
                _currentValueImage.fillOrigin = _fillOrigin;

            _currentValueImage.fillAmount = currentPercentage;
        }

        private void SimpleFill_UpdateLingeringFillValue(float currentPercentage)
        {
            /*
             * Internal use
             * -------------------------
             * Directly updates the value of the lingering value bar
             */
            if (_showLingeringValue)
            {
                if (_lingeringValueImage == null && _enableDebugging)
                {
                    Debug.LogWarning(Debugging.LingeringValueImageIsNull.Replace(Debugging.ReplaceString, gameObject.name));
                    return;
                }

                if (_lingeringValueImage.type != Image.Type.Filled)
                    _lingeringValueImage.type = Image.Type.Filled;

                if (_lingeringValueImage.fillMethod != _fillMethod)
                    _lingeringValueImage.fillMethod = _fillMethod;

                if (_lingeringValueImage.fillOrigin != _fillOrigin)
                    _lingeringValueImage.fillOrigin = _fillOrigin;

                if (currentPercentage >= 0)
                {
                    float finalSpeed = Time.deltaTime;
                    if (_lingeringValueSpeed != 0)
                        finalSpeed *= _lingeringValueSpeed;

                    if (currentPercentage >= _lastLingeringPercentage)
                        _lastLingeringPercentage = currentPercentage;
                    else if (currentPercentage < _lastLingeringPercentage)
                        _lastLingeringPercentage -= finalSpeed;
                }

                _lingeringValueImage.fillAmount = _lastLingeringPercentage;
            }
            else
            {
                if (_lingeringValueImage != null)
                    _lingeringValueImage.fillAmount = 0;
            }
        }

        private void UpdateTextValue(float currentValue, float maxValue, float currentPercentage)
        {
            /*
             * Internal use
             * -------------------------
             * Updates the Text object attached to this class
             */

            if (_valueText == null)
            {
                if (_statusTextDisplayType != TextDisplayType.None && _enableDebugging)
                    Debug.LogWarning(Debugging.TextValueIsNull.Replace(Debugging.ReplaceString, gameObject.name));

                return;
            }

            string finalText = "";
            if (usePrefixText)
                finalText += _prefixText;

            switch (_statusTextDisplayType)
            {
                case TextDisplayType.None:
                    finalText += "";
                    break;

                case TextDisplayType.CurrentValue:
                    finalText += ((int)currentValue).ToString();
                    break;

                case TextDisplayType.CurrentValueOfMax:
                    finalText += ((int)currentValue).ToString() + _middleText + ((int)maxValue).ToString();
                    break;

                case TextDisplayType.CurrentValuePercentage:
                    {
                        currentPercentage *= 100;
                        finalText += ((int)currentPercentage).ToString();
                        break;
                    }

                case TextDisplayType.Quantity:
                    {
                        finalText += ((int)currentValue).ToString();
                    }
                    break;
            }

            if (usePostfixText)
                finalText += _postfixText;

            _valueText.text = finalText;
        }

        private class Debugging
        {
            public const string CurrentValueImageIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Current Value Image is NULL!\n The Current Value Image is null, but you are using the \"Simple Fill\" Status Bar Type.";
            public const string DebuggingEnabled = "Lairinus.UI.Status.UIStatusBar DEBUGGING ENABLED on GameObject \"" + ReplaceString + "\"\nPlease disable this if you are running into performance issues";
            public const string LingeringValueImageIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Lingering Value Image is NULL\n The Lingering Value Image is null, but the MainStatusBar is set to display a lingering value. Either assign an Image object or do not flag this to show a lingering value!";
            public const string ReplaceString = "%%custom%%";
            public const string SpriteIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Sprite is NULL!\n inside of the \"separateSprites\" list. Assign a Sprite Object to show a sprite for this percentage!";
            public const string TextValueIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Value Text is NULL!\n The \"Value Text\" Object is NULL. Assign a Text object, or set the \"Status Text Display Type\" to \"None\"";
        }
    }
}