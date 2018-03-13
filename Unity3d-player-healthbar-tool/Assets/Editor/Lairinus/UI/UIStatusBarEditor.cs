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
        _object.statusBarType = (UIStatusBar.StatusBarType)GUILayout.SelectionGrid((int)_object.statusBarType, _util.GetStatusBarTypeContent(), 3);

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
    }

    private void ShowSharedConfiguration()
    {
        _object.valueImage = (Image)EditorGUILayout.ObjectField("Value Image", _object.valueImage, typeof(Image), true);
        _object.valueText = (Text)EditorGUILayout.ObjectField("Value Text", _object.valueText, typeof(Text), true);
        _object.separationText = EditorGUILayout.TextField("Appended Text", _object.separationText);
    }

    private void ShowSimpleFillUI()
    {
    }

    private void ShowQuantityUI()
    {
        _object.quantityIcon = (Sprite)EditorGUILayout.ObjectField("Display Icon", _object.quantityIcon, typeof(Sprite), true);
    }

    private void ShowSeparateSpriteUI()
    {
    }

    private UIStatusBar _object = null;
    private GUIUtility _util = new GUIUtility();

    private void OnEnable()
    {
        _object = (UIStatusBar)target;
    }

    private class GUIUtility
    {
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
                    return "Quantity Type:\n\nYou should use the \"Quantity\" type if you are keeping track of something. For example, player lives, player coints, etc.";

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