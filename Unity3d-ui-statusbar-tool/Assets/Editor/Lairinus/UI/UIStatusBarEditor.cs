using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Lairinus.UI;
using UnityEngine.UI;

[CustomEditor(typeof(UIStatusBar))]
[CanEditMultipleObjects]
public class UIStatusBarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
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
        serializedObject.ApplyModifiedProperties();
    }

    private Image.OriginHorizontal _horizontalOriginValue = new Image.OriginHorizontal();

    private UIStatusBar _object = null;

    private Image.Origin180 _origin180Value = new Image.Origin180();

    private Image.Origin360 _origin360Value = new Image.Origin360();

    private Image.Origin90 _origin90Value = new Image.Origin90();

    private Image.OriginVertical _originVerticalValue = new Image.OriginVertical();

    private SerializedProperty _property_CurrentValueImage = null;
    private SerializedProperty _property_EnableDebugging = null;
    private SerializedProperty _property_FillMethod = null;
    private SerializedProperty _property_LingeringValueImage = null;
    private SerializedProperty _property_LingeringValueSpeed = null;
    private SerializedProperty _property_MiddleText = null;
    private SerializedProperty _property_PostfixText = null;
    private SerializedProperty _property_PrefixText = null;
    private SerializedProperty _property_QuantityIcon = null;
    private SerializedProperty _property_SeparateSprites = null;
    private SerializedProperty _property_ShowLingeringValue = null;
    private SerializedProperty _property_StatusBarType = null;
    private SerializedProperty _property_StatusTextDisplayType = null;
    private SerializedProperty _property_UsePostfixText = null;
    private SerializedProperty _property_UsePrefixText = null;
    private SerializedProperty _property_ValueText = null;
    private GUIUtility _util = new GUIUtility();

    private void OnEnable()
    {
        _util = new GUIUtility();
        _object = (UIStatusBar)target;
        _property_SeparateSprites = serializedObject.FindProperty("_separateSprites");
        _property_StatusBarType = serializedObject.FindProperty("_statusBarType");
        _property_QuantityIcon = serializedObject.FindProperty("_quantityIcon");
        _property_EnableDebugging = serializedObject.FindProperty("_enableDebugging");
        _property_CurrentValueImage = serializedObject.FindProperty("_currentValueImage");
        _property_ValueText = serializedObject.FindProperty("_valueText");
        _property_UsePrefixText = serializedObject.FindProperty("_usePrefixText");
        _property_UsePostfixText = serializedObject.FindProperty("_usePostfixText");
        _property_PrefixText = serializedObject.FindProperty("_prefixText");
        _property_PostfixText = serializedObject.FindProperty("_postfixText");
        _property_FillMethod = serializedObject.FindProperty("_fillMethod");
        _property_LingeringValueImage = serializedObject.FindProperty("_lingeringValueImage");
        _property_LingeringValueSpeed = serializedObject.FindProperty("_lingeringValueSpeed");
        _property_ShowLingeringValue = serializedObject.FindProperty("_showLingeringValue");
        _property_MiddleText = serializedObject.FindProperty("_middleText");
        _property_StatusTextDisplayType = serializedObject.FindProperty("_statusTextDisplayType");
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
        EditorGUILayout.PropertyField(_property_QuantityIcon, new GUIContent("Quantity Icon"));
        GUILayout.EndHorizontal();
    }

    private void ShowSeparateSpriteUI()
    {
        // Separate Sprite
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        EditorGUILayout.PropertyField(_property_SeparateSprites, true);
        GUILayout.EndHorizontal();
    }

    private void ShowSharedConfiguration()
    {
        EditorGUILayout.PropertyField(_property_EnableDebugging, new GUIContent("Enable Debugging"));
        GUILayout.Space(_util.sectionSpace);
        EditorGUILayout.LabelField("General Fields", EditorStyles.boldLabel);

        // Value Image
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        EditorGUILayout.PropertyField(_property_CurrentValueImage, new GUIContent("Value Image"));
        GUILayout.EndHorizontal();

        // Value Text
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        EditorGUILayout.PropertyField(_property_ValueText, new GUIContent("Value Text"));
        GUILayout.EndHorizontal();

        // Spacer
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Space(_util.sectionSpace);

        // Status Bar Type
        EditorGUILayout.LabelField("Status Bar Type", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        EditorGUILayout.PropertyField(_property_StatusBarType, new GUIContent("Status Bar Type"));
        GUILayout.EndHorizontal();
    }

    private void ShowSimpleFillUI()
    {
        // Fill Method
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        EditorGUILayout.PropertyField(_property_FillMethod, new GUIContent("Fill Method"));
        GUILayout.EndHorizontal();

        ShowFillOrigins();

        // Show Lingering Value
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        EditorGUILayout.PropertyField(_property_ShowLingeringValue);
        GUILayout.EndHorizontal();

        // Lingering Value Image
        if (_object.showLingeringValue)
        {
            // Lingering Value Image
            GUILayout.BeginHorizontal();
            GUILayout.Space(_util.propertySpace);
            EditorGUILayout.PropertyField(_property_LingeringValueImage, new GUIContent("Lingering Value Image"));
            GUILayout.EndHorizontal();

            // Lingering Value Speed
            GUILayout.BeginHorizontal();
            GUILayout.Space(_util.propertySpace);
            EditorGUILayout.PropertyField(_property_LingeringValueSpeed, new GUIContent("Lingering Value Speed"));
            GUILayout.EndHorizontal();
        }
    }

    private void ShowTextSettings()
    {
        EditorGUILayout.LabelField("Text Settings", EditorStyles.boldLabel);

        // Status Text Display Type
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        EditorGUILayout.PropertyField(_property_StatusTextDisplayType, new GUIContent("Text Display Type"));
        GUILayout.EndHorizontal();

        // Use Prefix Text
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        EditorGUILayout.PropertyField(_property_UsePrefixText, new GUIContent("Use Prefix Text"));
        GUILayout.EndHorizontal();

        // Use Postfix Text
        GUILayout.BeginHorizontal();
        GUILayout.Space(_util.propertySpace);
        EditorGUILayout.PropertyField(_property_UsePostfixText, new GUIContent("Use Postfix Text"));
        GUILayout.EndHorizontal();

        // Prefix Text
        if (_object.usePrefixText)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(_util.propertySpace);
            EditorGUILayout.PropertyField(_property_PrefixText, new GUIContent("Prefix Text", "Text that is shown before the value"));
            GUILayout.EndHorizontal();
        }

        // Postfix Text
        if (_object.usePostfixText)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(_util.propertySpace);
            EditorGUILayout.PropertyField(_property_PostfixText, new GUIContent("Postfix Text", "Text that is shown after the value"));
            GUILayout.EndHorizontal();
        }

        // Middle Text
        if (_object.statusTextDisplayType == UIStatusBar.TextDisplayType.CurrentValueOfMax)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(_util.propertySpace);
            EditorGUILayout.PropertyField(_property_MiddleText, new GUIContent("Middle Text"));
            GUILayout.EndHorizontal();
        }
    }

    private class GUIUtility
    {
        public GUIContent fillMethod { get { return _fillMethod; } }
        public GUIContent fillOrigin { get { return _fillOrigin; } }
        public GUIContent lingeringValueImage { get { return _lingeringValueImage; } }
        public GUIContent lingeringValueSpeed { get { return _lingeringValueSpeed; } }
        public int propertySpace { get { return 30; } }
        public int sectionSpace { get { return 20; } }
        public GUIContent showLingeringValue { get { return _showLingeringValue; } }
        public GUIContent statusTextDisplayType { get { return _statusTextDisplayType; } }

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

        private GUIContent _fillMethod = new GUIContent("Fill Method", "Every image that is being used with the Fill type will have its' type set to fill. This field determines how it will be filled, and will overwrite the Image's value");
        private GUIContent _fillOrigin = new GUIContent("Fill Origin", "Overwrites the Fill Origin value for the Value and Lingering Value Image Objects");
        private GUIContent _lingeringValueImage = new GUIContent("Lingering Value Image", "The Lingering Value Image is the bar that sits directly underneath the Status Bar. When the Status Bar's value is set drastically different, you will see the Lingering Value Image as an after-image for a couple of seconds");
        private GUIContent _lingeringValueSpeed = new GUIContent("Lingering Value Speed", "This determines how slowly the Lingering Value depletes after a radical shift in values. Anything less than 1 is slower than normal speed.");
        private GUIContent _showLingeringValue = new GUIContent("Show Lingering Value", "If shown, you will need to attach a Lingering Value Image to this component. The Lingering Value Image acts as an after-image after a radical change in the Status Bar's value.");
        private GUIContent _statusTextDisplayType = new GUIContent("Text Display Type", "Uses the provided \"Value Text\" to display a specific type of Text. Some of the Text Display Types use the \"Appended Text\" value somewhere in the string");

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
    }
}