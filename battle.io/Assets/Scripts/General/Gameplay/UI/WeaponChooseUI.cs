using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChooseUI : MonoBehaviour
{
    public static WeaponChooseUI Singleton;

    private GameObject @player;
    private WeaponInventory _context;
    private LevelStructure Level;

    public GameObject[] Images = new GameObject[4]; 
    private void Start()
    {
        Singleton = this;

        player = FindObjectOfType<Player>().gameObject;
        _context = player.GetComponent<WeaponInventory>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject @object = transform.GetChild(i).gameObject;
            Images[i] = @object;
            @object.SetActive(false);
        }
    }

    public void GetChoose(LevelStructure level)
    {
        int ct = level.nextLevel.Length;
        for (int i = 0; i < ct; i++)
        {
            Images[i].SetActive(true);
            Images[i].GetComponent<Image>().sprite = level.nextLevel[i].Right.Prefab.GetComponent<SpriteRenderer>().sprite;
        }
        Level = level;
    }

    public void Choose(int i)
    {
        player.GetComponent<Player>().TakeLevel(Level.nextLevel[i], i);
        Close();
    }

    public void Close()
    {
        for (int i = 0; i < 4; i++)
        {
            Images[i].SetActive(false);
        }
    }
}
