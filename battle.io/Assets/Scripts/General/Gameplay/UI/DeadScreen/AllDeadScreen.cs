using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AllDeadScreen : MonoBehaviour
{
    public float TimeFade;
    private Image[] images;
    private TextMeshProUGUI[] texts;

    private void Start()
    {
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        StartCoroutine(enumerator());
    }
    IEnumerator enumerator()
    {
        yield return new WaitForEndOfFrame();
        FindObjectOfType<Player>().GetComponent<Level>().OnDead += PlayDeadScreen;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayDeadScreen()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].DOFade(1, TimeFade);
        }
        for (int i = 0; i < images.Length; i++)
        {
            texts[i].DOFade(1, TimeFade);
        }
    }
}
