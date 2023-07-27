using Ruinum.Core;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponChooseUI : BaseSingleton<WeaponChooseUI>
{
    private Player _player;
    private LevelStructure Level;

    [SerializeField] public GameObject[] Images = new GameObject[4]; 
    protected override void Awake()
    {
        base.Awake();
        _player = FindObjectOfType<Player>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject @object = transform.GetChild(i).gameObject;
            Images[i] = @object;
            @object.SetActive(false);
        }
    }

    public void GetChoose(LevelStructure level)
    {
        int ct = level.NextLevel.Length;
        for (int i = 0; i < ct; i++)
        {
            Images[i].SetActive(true);
            Images[i].transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.3f);
            Images[i].GetComponent<Image>().sprite = level.NextLevel[i].MainWeapon.Prefab.GetComponent<SpriteRenderer>().sprite;
        }
        Level = level;
    }

    public void Choose(int i)
    {
        _player.LevelProgression.TakeLevel(Level.NextLevel[i], i);
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
