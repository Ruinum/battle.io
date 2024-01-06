using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class RuinumBuildWindow : EditorWindow
{
    private VisualElement _rightPanel;

    [MenuItem("Ruinum/Ruinum Build Window")]
    public static void ShowWindow()
    {
        RuinumBuildWindow wnd = GetWindow<RuinumBuildWindow>();
        wnd.titleContent = new GUIContent("Ruinum Build Window");
    }

    public void CreateGUI()
    {
        var buildTypes = new List<BuildType>();
        buildTypes.AddRange(Enum.GetValues(typeof(BuildType)).Cast<BuildType>().ToArray());
        buildTypes.RemoveAt(0);
        buildTypes.RemoveAt(buildTypes.Count - 1);

        var splitView = new TwoPaneSplitView(0, 150, TwoPaneSplitViewOrientation.Horizontal);

        rootVisualElement.Add(splitView);

        // A TwoPaneSplitView always needs exactly two child elements
        var listView = new ListView();
        splitView.Add(listView);
        _rightPanel = new VisualElement();
        splitView.Add(_rightPanel);

        // Initialize the list view with all sprites' names
        listView.makeItem = () => EditorExtentions.CreateLabel();
        listView.bindItem = (item, index) => { (item as Label).text = buildTypes[index].ToString(); };
        listView.itemsSource = buildTypes;
        listView.onSelectionChange += OnListSelectionChange;
    }

    private void OnListSelectionChange(IEnumerable<object> selectedItems)
    {
        _rightPanel.Clear();

        var buildType = (BuildType)selectedItems.First();

        switch (buildType)
        {
            case BuildType.Desktop: OnDesktopSelected(_rightPanel); break;
            case BuildType.Console: OnConsoleSelected(_rightPanel); break;
            case BuildType.WEBGL: OnWebglSelected(_rightPanel); break;
        }

    }

    private void OnDesktopSelected(VisualElement visualElement)
    {
        visualElement.Add(new Label("123"));
        visualElement.Add(EditorExtentions.CreateImage());
        visualElement.Add(new Label("22"));
        visualElement.Add(EditorExtentions.CreateButton("Reset Saves"));
        visualElement.Add(EditorExtentions.CreateButton("Build"));
    }

    private void OnWebglSelected(VisualElement visualElement)
    {

    }

    private void OnConsoleSelected(VisualElement visualElement)
    {

    }
}
