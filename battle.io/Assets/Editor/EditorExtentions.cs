using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class EditorExtentions
{
    public static Label CreateLabel(string text = "")
    {
        return new Label(text);
    }

    public static Button CreateButton(string text = "", Action clickEvent = null)
    {
        Button button = new Button(clickEvent);
        button.text = text;
        return button;
    }

    public static Image CreateImage(Sprite sprite = null, ScaleMode scaleMode = ScaleMode.ScaleToFit)
    {
        Image image = new Image();
        image.sprite = sprite;
        image.scaleMode = scaleMode;
        return image;
    }

    public static DropdownField CreateDropdownField(string label = "", List<string> choices = null)
    {
        return new DropdownField(label, choices, 0);        
    }

    public static Toggle CreateToggle(string label = "")
    {
        return new Toggle(label);
    }

    public static TextField CreateTextField(string label = "", int maxLength = 100, bool multiline = false, bool isPasswordField = false, char maskChar = '.')
    {
        return new TextField(label, maxLength, multiline, isPasswordField, maskChar);
    }
}