using UnityEngine;

namespace Ruinum.InstantBridge
{
    public class RewardAdButton : MonoBehaviour
    {
        protected Skin _currentSkin;

        public void Show(Skin skin)
        {
            _currentSkin = skin;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _currentSkin = null;
            gameObject.SetActive(false);    
        }
    }
}
