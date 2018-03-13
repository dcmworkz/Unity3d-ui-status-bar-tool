using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lairinus.UI.Status
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

        public bool enableDebugging { get { return _enableDebugging; } set { _enableDebugging = value; } }
        public Image.FillMethod fillMethod { get { return _simpleFill_FillMethod; } set { _simpleFill_FillMethod = value; } }
        public int fillOrigin { get { return _fillOrigin; } set { _fillOrigin = value; } }
        public float lastingValueUpdateRate { get { return simpleFill_LastingValueUpdateRate; } }

        public List<Sprite> separateSprites = new List<Sprite>();
        public Sprite quantityIcon { get { return _quantityIcon; } set { _quantityIcon = value; } }
        public string separationText { get { return _separationText; } set { _separationText = value; } }
        public bool showLingeringValue { get { return _showLastingValue; } set { _showLastingValue = value; } }
        public StatusBarType statusBarType { get { return _statusBarType; } set { _statusBarType = value; } }
        public TextDisplayType statusTextDisplayType { get { return _statusTextDisplayType; } }
        public Image valueImage { get { return _currentValueImage; } set { _currentValueImage = value; } }
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
        [SerializeField] private Sprite _quantityIcon = null;
        [SerializeField] private string _separationText = "x";
        [SerializeField] private bool _showLastingValue = false;
        [SerializeField] private Image.FillMethod _simpleFill_FillMethod = Image.FillMethod.Horizontal;
        [SerializeField] private StatusBarType _statusBarType = StatusBarType.SimpleFill;
        [SerializeField] private TextDisplayType _statusTextDisplayType = TextDisplayType.CurrentValue;
        [SerializeField] private Text _valueText = null;
        [SerializeField] private float simpleFill_LastingValueUpdateRate = 0;

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
            if (separateSprites == null)
                return;

            int currentRoughIndex = Mathf.CeilToInt(separateSprites.Count * currentPercentage);
            if (currentRoughIndex == 0)
                currentRoughIndex++;

            Sprite desiredSprite = separateSprites[currentRoughIndex - 1];
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

            if (_currentValueImage.fillMethod != _simpleFill_FillMethod)
                _currentValueImage.fillMethod = _simpleFill_FillMethod;

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
            if (_showLastingValue)
            {
                if (_lingeringValueImage == null && _enableDebugging)
                {
                    Debug.LogWarning(Debugging.LingeringValueImageIsNull.Replace(Debugging.ReplaceString, gameObject.name));
                    return;
                }

                if (_lingeringValueImage.type != Image.Type.Filled)
                    _lingeringValueImage.type = Image.Type.Filled;

                if (_lingeringValueImage.fillMethod != _simpleFill_FillMethod)
                    _lingeringValueImage.fillMethod = _simpleFill_FillMethod;

                if (_lingeringValueImage.fillOrigin != _fillOrigin)
                    _lingeringValueImage.fillOrigin = _fillOrigin;

                if (currentPercentage > 0)
                {
                    if (currentPercentage > _lastLingeringPercentage)
                        _lastLingeringPercentage += Time.deltaTime + simpleFill_LastingValueUpdateRate;
                    else if (currentPercentage < _lastLingeringPercentage)
                        _lastLingeringPercentage -= Time.deltaTime - simpleFill_LastingValueUpdateRate;
                }
                else _lastLingeringPercentage = 0;

                _lingeringValueImage.fillAmount = _lastLingeringPercentage;
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

            switch (_statusTextDisplayType)
            {
                case TextDisplayType.None:
                    _valueText.text = "";
                    break;

                case TextDisplayType.CurrentValue:
                    _valueText.text = ((int)currentValue).ToString();
                    break;

                case TextDisplayType.CurrentValueOfMax:
                    _valueText.text = ((int)currentValue).ToString() + _separationText + ((int)maxValue).ToString();
                    break;

                case TextDisplayType.CurrentValuePercentage:
                    {
                        currentPercentage *= 100;
                        _valueText.text = ((int)currentPercentage).ToString() + _separationText;
                        break;
                    }

                case TextDisplayType.Quantity:
                    {
                        _valueText.text = _separationText + ((int)currentValue).ToString();
                    }
                    break;
            }
        }

        private class Debugging
        {
            public const string CurrentValueImageIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Current Value Image is NULL!\n The Current Value Image is null, but you are using the \"Simple Fill\" Status Bar Type.";
            public const string LingeringValueImageIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Lingering Value Image is NULL\n The Lingering Value Image is null, but the MainStatusBar is set to display a lingering value. Either assign an Image object or do not flag this to show a lingering value!";
            public const string ReplaceString = "%%custom%%";
            public const string SpriteIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Sprite is NULL!\n inside of the \"separateSprites\" list. Assign a Sprite Object to show a sprite for this percentage!";
            public const string TextValueIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Value Text is NULL!\n The \"Value Text\" Object is NULL. Assign a Text object, or set the \"Status Text Display Type\" to \"None\"";
        }
    }
}