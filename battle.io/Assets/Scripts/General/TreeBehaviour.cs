using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    private List<TreeItemView> _treeItemViews;

    private void Start()
    {
        _treeItemViews = new List<TreeItemView>();
        _treeItemViews.AddRange(FindObjectsOfType<TreeItemView>());

        RefreshTree();
    }

    public void RefreshTree()
    {
        for (int i = 0; i < _treeItemViews.Count; i++)
        {
            TreeItemView treeItem = _treeItemViews[i];
            treeItem.Initialize();
        }
    }
}