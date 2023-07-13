using TMPro;
using DG.Tweening;
using UnityEngine;

public static class ImpactUtils
{
    public static GameObject CreatePopUp(string text, Vector3 position, Color color)
    {
        AssetsContext assetsContext = Resources.Load<AssetsContext>("Content/Data/AssetsContext");
        if (assetsContext == null) { Debug.LogWarning("Can't create PopUp because there is no AssetsContext in {Content/Data/}"); return null; }

        var popUpObject = Object.Instantiate(assetsContext.GetObjectOfType(typeof(GameObject), "PopUp")) as GameObject;
        popUpObject.GetComponent<Canvas>().sortingOrder = 5;
        popUpObject.transform.position = position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1);

        var textMeshPro = popUpObject.GetComponentInChildren<TMP_Text>();
        textMeshPro.text = text;
        textMeshPro.color = color;

        popUpObject.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.3f);
        textMeshPro.DOFade(0, 1.25f).OnComplete(() => Object.Destroy(popUpObject));

        return popUpObject;
    }
}