using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lairinus.UI.Status
{
    public class MainStatusBar : MonoBehaviour
    {
        private void Awake()
        {
        }

        public enum StatusBarType
        {
            SimpleFill
        }

        public enum SimpleFillStatusDisplayType
        {
            None,
            CurrentValue,
            CurrentValuePercentage,
            CurrentValueOfMax,
        }

        [SerializeField] private Image.FillMethod _fillMethod = Image.FillMethod.Horizontal;
        public Image.FillMethod fillMethod { get { return _fillMethod; } set { _fillMethod = value; } }

        [SerializeField] private bool _enableDebugging = false;
        public bool enableDebugging { get { return _enableDebugging; } set { _enableDebugging = value; } }

        [SerializeField] private Image _currentValueImage = null;
        public Image currentValueImage { get { return _currentValueImage; } set { _currentValueImage = value; } }

        [SerializeField] private Image _lingeringValueImage = null;
        public Image lingeringValueImage { get { return _lingeringValueImage; } set { _lingeringValueImage = value; } }

        [SerializeField] private StatusBarType _statusBarType = StatusBarType.SimpleFill;
        public StatusBarType statusBarType { get { return _statusBarType; } set { _statusBarType = value; } }

        [SerializeField] private SimpleFillStatusDisplayType _statusTextDisplayType = SimpleFillStatusDisplayType.CurrentValue;
        public SimpleFillStatusDisplayType statusTextDisplayType { get { return _statusTextDisplayType; } }

        [SerializeField] private bool _showLingeringValue = false;
        public bool showLingeringValue { get { return _showLingeringValue; } set { _showLingeringValue = value; } }

        [SerializeField] private Text _valueText = null;
        public Text valueText { get { return _valueText; } set { _valueText = value; } }

        [SerializeField] private float _lingeringValueUpdateRate = 0;
        public float lingeringValueFrameWeight { get { return _lingeringValueUpdateRate; } }

        private float _lastLingeringPercentage = 0;

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
            _currentValueImage.fillMethod = _fillMethod;
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

                _lingeringValueImage.type = Image.Type.Filled;
                _lingeringValueImage.fillMethod = _fillMethod;
                _lingeringValueImage.fillAmount = _lastLingeringPercentage;

                if (currentPercentage > 0)
                {
                    if (currentPercentage > _lastLingeringPercentage)
                        _lastLingeringPercentage += Time.deltaTime + _lingeringValueUpdateRate;
                    else if (currentPercentage < _lastLingeringPercentage)
                        _lastLingeringPercentage -= Time.deltaTime - _lingeringValueUpdateRate;
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
                if (_statusTextDisplayType != SimpleFillStatusDisplayType.None && _enableDebugging)
                    Debug.LogWarning(Debugging.TextValueIsNull.Replace(Debugging.ReplaceString, gameObject.name));

                return;
            }

            switch (_statusTextDisplayType)
            {
                case SimpleFillStatusDisplayType.None:
                    _valueText.text = "";
                    break;

                case SimpleFillStatusDisplayType.CurrentValue:
                    _valueText.text = ((int)currentValue).ToString();
                    break;

                case SimpleFillStatusDisplayType.CurrentValueOfMax:
                    _valueText.text = ((int)currentValue).ToString() + " / " + ((int)maxValue).ToString();
                    break;

                case SimpleFillStatusDisplayType.CurrentValuePercentage:
                    {
                        currentPercentage *= 100;
                        _valueText.text = ((int)currentPercentage).ToString() + "%";
                        break;
                    }
            }
        }

        private class Debugging
        {
            public const string ReplaceString = "%%custom%%";
            public const string LingeringValueImageIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Lingering Value Image is NULL\n The Lingering Value Image is null, but the MainStatusBar is set to display a lingering value. Either assign an Image object or do not flag this to show a lingering value!";
            public const string CurrentValueImageIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Current Value Image is NULL!\n The Current Value Image is null, but you are using the \"Simple Fill\" Status Bar Type.";
            public const string TextValueIsNull = "Lairinus.UI.Status.MainStatusBar on GameObject \"" + ReplaceString + "\"- Value Text is NULL!\n The \"Value Text\" Object is NULL. Assign a Text object, or set the \"Status Text Display Type\" to \"None\"";
        }
    }
}