using TMPro;
using DG.Tweening;
using UnityEngine;

namespace Ruinum.Utils
{
    public static class ImpactUtils
    {
        public static TMP_Text CreatePopUp(string text, Vector3 position, Color color)
        {
            AssetsContext assetsContext = Game.Context.AssetsContext;
            if (assetsContext == null) { Debug.LogWarning($"Can't create PopUp because there is no AssetsContext or it is null in {typeof(GameConfig)}"); return null; }

            var popUpObject = Object.Instantiate(assetsContext.GetObjectOfType(typeof(GameObject), "PopUp")) as GameObject;
            popUpObject.GetComponent<Canvas>().sortingOrder = 5;
            popUpObject.transform.position = position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1);

            var textMeshPro = popUpObject.GetComponentInChildren<TMP_Text>();
            textMeshPro.text = text;
            textMeshPro.color = color;

            popUpObject.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.3f);
            textMeshPro.DOFade(0, 1.25f).OnComplete(() => Object.Destroy(popUpObject));

            return textMeshPro;
        }

        public static TMP_Text CreatePopUp(string text, Vector3 position, Color color, float size)
        {
            TMP_Text textMeshPro = CreatePopUp(text, position, color);
            textMeshPro.fontSize = size;

            return textMeshPro;
        }
    }
}