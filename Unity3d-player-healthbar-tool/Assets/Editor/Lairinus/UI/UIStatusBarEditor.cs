using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Lairinus.UI.Status;
using UnityEngine.UI;

[CustomEditor(typeof(UIStatusBar))]
public class UIStatusBarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        _util = new GUIUtility();
        ShowSharedConfiguration();

        switch (_object.statusBarType)
        {
            case UIStatusBar.StatusBarType.Quantity:
                ShowQuantityUI();
                break;

            case UIStatusBar.StatusBarType.SeparateSprites:
                ShowSeparateSpriteUI();
                break;

            case UIStatusBar.StatusBarType.SimpleFill:
                ShowSimpleFillUI();
                break;
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Space(_util.sectionSpace);
        ShowTextSettings();
    }

    private Image.OriginHorizontal _horizontalOriginValue = new Image.OriginHorizontal();
    private Image.Origin180 _origin180Value = new Image.Origin180();
    private Image.OriginVertical _originVerticalValue = new Image.OriginVertical();
    private Image.Origin90 _origin90Value = new Image.Origin90();
    private Image.Origin360 _origin360Value = new Image.Origin360();

    private void ShowSharedConfiguration()
    {
        GUILayout.Space(_util.sectionSpace);
        EditorGUILayout.LabelField("General Fields", EditorStyles.boldLabel);

        // Value Image
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        _object.valueImage = (Image)EditorGUILayout.ObjectField("Value Image", _object.valueImage, typeof(Image), true);
        GUILayout.EndHorizontal();

        // Value Text
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        _object.valueText = (Text)EditorGUILayout.ObjectField("Value Text", _object.valueText, typeof(Text), true);
        GUILayout.EndHorizontal();

        // Spacer
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Space(_util.sectionSpace);

        // Status Bar Type
        EditorGUILayout.LabelField("Status Bar Type", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        _object.statusBarType = (UIStatusBar.StatusBarType)GUILayout.SelectionGrid((int)_object.statusBarType, _util.GetStatusBarTypeContent(), 3);
        GUILayout.EndHorizontal();
    }

    private void ShowSimpleFillUI()
    {
        // Fill Method
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        _object.fillMethod = (Image.FillMethod)EditorGUILayout.EnumPopup(_util.fillMethod, _object.fillMethod);
        GUILayout.EndHorizontal();

        ShowFillOrigins();

        // Show Lingering Value
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        _object.showLingeringValue = EditorGUILayout.Toggle(_util.showLingeringValue, _object.showLingeringValue);
        GUILayout.EndHorizontal();

        // Lingering Value Image
        if (_object.showLingeringValue)
        {
            // Lingering Value Image
            GUILayout.BeginHorizontal();
            GUILayout.Space(_util.propertySpace);
            _object.valueLingeringImage = (Image)EditorGUILayout.ObjectField(_util.lingeringValueImage, _object.valueLingeringImage, typeof(Image), true);
            GUILayout.EndHorizontal();

            // Lingering Value Speed
            GUILayout.BeginHorizontal();
            GUILayout.Space(_util.propertySpace);
            _object.lingeringValueSpeed = EditorGUILayout.Slider(_util.lingeringValueSpeed, _object.lingeringValueSpeed, 0, 1);
            GUILayout.EndHorizontal();
        }
    }

    private void ShowFillOrigins()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        switch (_object.fillMethod)
        {
            case Image.FillMethod.Horizontal:
                _horizontalOriginValue = (Image.OriginHorizontal)EditorGUILayout.EnumPopup(_util.fillOrigin, _horizontalOriginValue);
                _object.fillOrigin = (int)_horizontalOriginValue;
                break;

            case Image.FillMethod.Radial180:
                _origin180Value = (Image.Origin180)EditorGUILayout.EnumPopup(_util.fillOrigin, _origin180Value);
                _object.fillOrigin = (int)_origin180Value;
                break;

            case Image.FillMethod.Radial360:
                _origin360Value = (Image.Origin360)EditorGUILayout.EnumPopup(_util.fillOrigin, _origin360Value);
                _object.fillOrigin = (int)_origin360Value;
                break;

            case Image.FillMethod.Radial90:
                _origin90Value = (Image.Origin90)EditorGUILayout.EnumPopup(_util.fillOrigin, _origin90Value);
                _object.fillOrigin = (int)_origin90Value;
                break;

            case Image.FillMethod.Vertical:
                _originVerticalValue = (Image.OriginVertical)EditorGUILayout.EnumPopup(_util.fillOrigin, _originVerticalValue);
                _object.fillOrigin = (int)_originVerticalValue;
                break;
        }
        GUILayout.EndHorizontal();
    }

    private void ShowQuantityUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        _object.quantityIcon = (Sprite)EditorGUILayout.ObjectField("Display Icon", _object.quantityIcon, typeof(Sprite), true);
        GUILayout.EndHorizontal();
    }

    private void ShowSeparateSpriteUI()
    {
        // Separate Sprite
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        SerializedProperty prop = serializedObject.FindProperty("separateSprites");
        EditorGUILayout.PropertyField(prop, true);
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
        GUILayout.EndHorizontal();
    }

    private void ShowTextSettings()
    {
        EditorGUILayout.LabelField("Text Settings", EditorStyles.boldLabel);

        // Status Text Display Type
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        _object.statusTextDisplayType = (UIStatusBar.TextDisplayType)EditorGUILayout.EnumPopup(_util.statusTextDisplayType, _object.statusTextDisplayType);
        GUILayout.EndHorizontal();

        // Use Prefix Text
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        _object.usePrefixText = EditorGUILayout.Toggle("Use Prefix Text", _object.usePrefixText);
        GUILayout.EndHorizontal();

        // Use Postfix Text
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        _object.usePostfixText = EditorGUILayout.Toggle("Use Postfix Text", _object.usePostfixText);
        GUILayout.EndHorizontal();

        // Prefix Text
        if (_object.usePrefixText)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(_util.propertySpace);
            _object.prefixText = EditorGUILayout.TextField("Prefix Text", _object.prefixText);
            GUILayout.EndHorizontal();
        }

        // Postfix Text
        if (_object.usePostfixText)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(_util.propertySpace);
            _object.postfixText = EditorGUILayout.TextField("Postfix Text", _object.postfixText);
            GUILayout.EndHorizontal();
        }
    }

    private UIStatusBar _object = null;
    private GUIUtility _util = new GUIUtility();

    private void OnEnable()
    {
        _object = (UIStatusBar)target;
    }

    private class GUIUtility
    {
        public int sectionSpace { get { return 20; } }
        public int propertySpace { get { return 30; } }

        public GUIContent[] GetStatusBarTypeContent()
        {
            List<GUIContent> final = new List<GUIContent>();
            string[] names = Enum.GetNames(typeof(UIStatusBar.StatusBarType));

            for (var a = 0; a < names.Length; a++)
            {
                GUIContent newContent = new GUIContent(names[a], GetStatusBarTypeTooltip((UIStatusBar.StatusBarType)a));
                final.Add(newContent);
            }

            return final.ToArray();
        }

        private string GetStatusBarTypeTooltip(UIStatusBar.StatusBarType type)
        {
            switch (type)
            {
                case UIStatusBar.StatusBarType.Quantity:
                    return "Quantity Type:\n\nYou should use the \"Quantity\" type if you are keeping track of something. For example, player lives, player coins, etc.";

                case UIStatusBar.StatusBarType.SeparateSprites:
                    return "Separate Sprites Type:\n\nYou should use the \"Separate Sprites\" type if you want your Status Bar to be made up of many different Sprite elements.";

                case UIStatusBar.StatusBarType.SimpleFill:
                    return "Simple Fill:\n\nYou should use the \"Simple Fill\" type if you need a basic Health or Resource bar.";

                default:
                    return "";
            }
        }

        private GUIContent _fillOrigin = new GUIContent("Fill Origin", "Overwrites the Fill Origin value for the Value and Lingering Value Image Objects");
        public GUIContent fillOrigin { get { return _fillOrigin; } }

        private GUIContent _fillMethod = new GUIContent("Fill Method", "Every image that is being used with the Fill type will have its' type set to fill. This field determines how it will be filled, and will overwrite the Image's value");
        public GUIContent fillMethod { get { return _fillMethod; } }

        private GUIContent _lingeringValueImage = new GUIContent("Lingering Value Image", "The Lingering Value Image is the bar that sits directly underneath the Status Bar. When the Status Bar's value is set drastically different, you will see the Lingering Value Image as an after-image for a couple of seconds");
        public GUIContent lingeringValueImage { get { return _lingeringValueImage; } }

        private GUIContent _showLingeringValue = new GUIContent("Show Lingering Value", "If shown, you will need to attach a Lingering Value Image to this component. The Lingering Value Image acts as an after-image after a radical change in the Status Bar's value.");
        public GUIContent showLingeringValue { get { return _showLingeringValue; } }

        private GUIContent _statusTextDisplayType = new GUIContent("Text Display Type", "Uses the provided \"Value Text\" to display a specific type of Text. Some of the Text Display Types use the \"Appended Text\" value somewhere in the string");
        public GUIContent statusTextDisplayType { get { return _statusTextDisplayType; } }

        private GUIContent _lingeringValueSpeed = new GUIContent("Lingering Value Speed", "This determines how slowly the Lingering Value depletes after a radical shift in values. Anything less than 1 is slower than normal speed.");
        public GUIContent lingeringValueSpeed { get { return _lingeringValueSpeed; } }
    }
}