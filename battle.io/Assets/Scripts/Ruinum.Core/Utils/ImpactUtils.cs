using TMPro;
using UnityEngine;

namespace Ruinum.Utils
{
    public static class ImpactUtils
    {
        public static TMP_Text CreatePopUp(string text, Vector3 position, Color color)
        {          
            var popUpObject = Game.Context.PopUpPool.GetPoolObject();
            popUpObject.ShowPopUp(text, position, color);

            return popUpObject.GetTMPText();
        }

        public static TMP_Text CreatePopUp(string text, Vector3 position, Color color, float size)
        {
            TMP_Text textMeshPro = CreatePopUp(text, position, color);
            textMeshPro.fontSize = size;

            return textMeshPro;
        }
    }
}