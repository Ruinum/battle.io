using TMPro;
using UnityEngine;

namespace Ruinum.Utils
{
    public static class ImpactUtils
    {
        private static int _popupUnloadDistance = GameConstants.POPUP_UNLOAD_DISTANCE;

        public static bool TryCreatePopUp(string text, Vector3 position, Color color, out TMP_Text tmp)
        {
            tmp = null;
            
            if (!Game.Context.PopUpPool.TryGetPoolObject(out PopUp popUpObject)) return false;
            if (Game.Context.Player == null || Game.Context.Player == default) return false;
            if (Vector2.Distance(popUpObject.transform.position, Game.Context.Player.Transform.position) >= _popupUnloadDistance) return false;
            
            popUpObject.ShowPopUp(text, position, color);
            tmp = popUpObject.GetTMPText();
            
            return true;
        }

        public static bool TryCreatePopUp(string text, Vector3 position, Color color, float size, out TMP_Text tmp)
        {
            if (!TryCreatePopUp(text, position, color, out tmp)) return false;

            tmp.fontSize = size;

            return true;
        }
    }
}