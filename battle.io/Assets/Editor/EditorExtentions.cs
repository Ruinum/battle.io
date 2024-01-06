using System;
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
}