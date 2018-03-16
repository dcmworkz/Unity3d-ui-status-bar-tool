# Unity3d-player-healthbar-tool
Introduction
Progress bars in Unity3D are fantastic at showing health, experience, loading progress, and many other features. Although they're very useful and almost everyone has at least one, you still need to create some sort of custom code or use a tool. The UIStatusBar tool allows users to show a simple progress bar with sprites, image fill, or even as a quantity tracker. See below for the demo and more details!

Meet the Unity3D UI Status Bar Tool
Out of the box, the UI Progress Bar tool contains a lot of great features:

3 Progress Bar Types
Simple Fill
    Shows basic Images with a fill in order to represent the current/max value.
    If enabled, "Lingering" values show an after-image in case the value of the progress bar changes dramatically.
Quantity
    Can be used to track a basic value or count
Sprite
    Can cycle through sprites to display a minimum/maximum/current value.
Many Text Settings
    The text can be fully customized in order to provide exactly what you want.
    Supports a "prefix" and "post-fix" string that can be added before and after the values, respectively
    Supports showing the UIStatusBar value in five different formats
        None - No text is displayed
        Quantity - Text is treated as a quantity, and only the current value is displayed
        Current Value - Only the Current Value is displayed
        Current Value Percentage - The Current Value is displayed as a percentage
        Current Value of Max = The formatting for this type is "CurrentValue + "x" + MaxValue", where X is any string that you want to     specify
        
Ease of Use
The UI Status Bar is designed to be incredibly easy to use. There are only three steps involved:
    1. Add the UIStatusBar component to an object
    2. Fill in the Value Image and Value Text properties, along with all other fields that you want
    3. Call the UpdateStatusBar() method in-game in Update(), a Coroutine() or even at your own pace to update the Status Bar.


========================================

CHANGELOG

========================================

Version 1.0.0 - 03/15/18 - Initial Release
Version 1.0.1 - 03/16/18 - Bug Fixes
    1. Sometimes when pressing "Play" in the Unity Editor, properties in the inspector would disappear. This is now resolved.
    2. Sometimes when modifying an instance of a prefab, values would revert to the original prefab object. This is now resolved.
