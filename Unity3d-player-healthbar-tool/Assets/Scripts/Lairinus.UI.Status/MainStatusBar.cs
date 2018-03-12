using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lairinus.UI.Status
{
    public class MainStatusBar : MonoBehaviour
    {
        public enum SimpleFillTextStatusDisplayType
        {
            None,
            CurrentValue,
            CurrentValuePercentage,
            CurrentValueOfMax,
        }

        public enum StatusBarType
        {
            SimpleFill,
            Quantity
        }

        public enum QuantityTextStatusDisplayType
        {
            Current,
            CurrentOfMax
        }

        public bool enableDebugging { get { return _enableDebugging; } set { _enableDebugging = value; } }
        public int simpleFillOrigin { get; set; }
        public float simpleFill_lastingValueUpdateRate { get { return simpleFill_LastingValueUpdateRate; } }
        public bool showLastingValue { get { return _showLastingValue; } set { _showLastingValue = value; } }
        public Image.FillMethod simpleFill_FillMethod { get { return _simpleFill_FillMethod; } set { _simpleFill_FillMethod = value; } }
        public SimpleFillTextStatusDisplayType statusTextDisplayType { get { return _statusTextDisplayType; } }
        public QuantityTextStatusDisplayType quantityTextDisplayType { get { return _quantityTextDisplayType; } }
        public Image valueImage { get { return _currentValueImage; } set { _currentValueImage = value; } }
        public Image valueLingeringImage { get { return _lingeringValueImage; } set { _lingeringValueImage = value; } }
        public Text valueText { get { return _valueText; } set { _valueText = value; } }
        public Sprite quantityIcon { get { return _quantityIcon; } set { _quantityIcon = value; } }

        public void UpdateStatusBar(float currentValue, float maxValue)
        {
            /*
             * External use
             * -------------------------
             * Provides updates to the Status Bar based on the current and maximum values.
             */

            float currentPercentage = (currentValue / maxValue);

            switch (_statusBarType)
            {
                case StatusBarType.SimpleFill:
                    {
                        SimpleFill_FillStatusBar(currentPercentage);
                        SimpleFill_UpdateLingeringFillValue(currentPercentage);
                        SimpleFill_UpdateText(currentValue, maxValue, currentPercentage);
                    }
                    break;

                case StatusBarType.Quantity:
                    {
                        Quantity_UpdateValue(currentValue, maxValue, currentPercentage);
                    }
                    break;
            }
        }

        [SerializeField] private Image _currentValueImage = null;
        [SerializeField] private bool _enableDebugging = false;
        [SerializeField] private int _fillOrigin = 0;
        [SerializeField] private float simpleFill_LastingValueUpdateRate = 0;
        private float _lastLingeringPercentage = 0;
        [SerializeField] private Image _lingeringValueImage = null;
        [SerializeField] private bool _showLastingValue = false;
        [SerializeField] private Image.FillMethod _simpleFill_FillMethod = Image.FillMethod.Horizontal;
        [SerializeField] private StatusBarType _statusBarType = StatusBarType.SimpleFill;
        [SerializeField] private Sprite _quantityIcon = null;
        [SerializeField] private SimpleFillTextStatusDisplayType _statusTextDisplayType = SimpleFillTextStatusDisplayType.CurrentValue;
        [SerializeField] private QuantityTextStatusDisplayType _quantityTextDisplayType = QuantityTextStatusDisplayType.Current;
        [SerializeField] private string _quantitySeparationText = "x";

        [SerializeField] private Text _valueText = null;

        private void Awake()
        {
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

            switch (_quantityTextDisplayType)
            {
                case QuantityTextStatusDisplayType.Current:
                    _valueText.text = _quantitySeparationText + " " + currentValue.ToString();
                    break;

                case QuantityTextStatusDisplayType.CurrentOfMax:
                    _valueText.text = currentValue.ToString() + " " + _quantitySeparationText + " " + maxValue.ToString();
                    break;
            }
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

            _currentValueImage.type = Image.Type.Filled;
            _currentValueImage.fillMethod = _simpleFill_FillMethod;
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

                _lingeringValueImage.type = Image.Type.Filled;
                _lingeringValueImage.fillMethod = _simpleFill_FillMethod;
                _lingeringValueImage.fillAmount = _lastLingeringPercentage;

                if (currentPercentage > 0)
                {
                    if (currentPercentage > _lastLingeringPercentage)
                        _lastLingeringPercentage += Time.deltaTime + simpleFill_LastingValueUpdateRate;
                    else if (currentPercentage < _lastLingeringPercentage)
                        _lastLingeringPercentage -= Time.deltaTime - simpleFill_LastingValueUpdateRate;
                }
                else _lastLingeringPercentage = 0;
            }
        }

        private void SimpleFill_UpdateText(float currentValue, float maxValue, float currentPercentage)
        {
            /*
             * Internal use
             * -------------------------
             * Updates the Text object attached to this class
             */

            if (_valueText == null)
            {
                if (_statusTextDisplayType != SimpleFillTextStatusDisplayType.None && _enableDebugging)
                    Debug.LogWarning(Debugging.TextValueIsNull.Replace(Debugging.ReplaceString, gameObject.name));

                return;
            }

            switch (_statusTextDisplayType)
            {
                case SimpleFillTextStatusDisplayType.None:
                    _valueText.text = "";
                    break;

                case SimpleFillTextStatusDisplayType.CurrentValue:
                    _valueText.text = ((int)currentValue).ToString();
                    break;

                case SimpleFillTextStatusDisplayType.CurrentValueOfMax:
                    _valueText.text = ((int)currentValue).ToString() + " / " + ((int)maxValue).ToString();
                    break;

                case SimpleFillTextStatusDisplayType.CurrentValuePercentage:
                    {
                        currentPercentage *= 100;
                        _valueText.text = ((int)currentPercentage).ToString() + "%";
                        break;
                    }
            }
        }

        private class Debugging
        {
            public const string CurrentValueImageIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Current Value Image is NULL!\n The Current Value Image is null, but you are using the \"Simple Fill\" Status Bar Type.";
            public const string LingeringValueImageIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Lingering Value Image is NULL\n The Lingering Value Image is null, but the MainStatusBar is set to display a lingering value. Either assign an Image object or do not flag this to show a lingering value!";
            public const string ReplaceString = "%%custom%%";
            public const string TextValueIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Value Text is NULL!\n The \"Value Text\" Object is NULL. Assign a Text object, or set the \"Status Text Display Type\" to \"None\"";
        }
    }
}