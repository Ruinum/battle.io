using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponChooseUI : MonoBehaviour
{
    private LevelStructure Level;
    private ILevelProgression _levelProgression;

    [SerializeField] private GameObject[] Images = new GameObject[4];
    [SerializeField] private GameObject _background;
    
    public void Initialize(IPlayer player)
    {
        _levelProgression = player.LevelProgression;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject @object = transform.GetChild(i).gameObject;
            Images[i] = @object;
            @object.SetActive(false);
        }
    }

    public void GetChoose(LevelStructure level)
    {
        if (level.NextLevel.Length <= 0) Close();

        _background.SetActive(true);

        int ct = level.NextLevel.Length;
        for (int i = 0; i < ct; i++)
        {
            Images[i].SetActive(true);
            Images[i].transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.3f);
            if (level.NextLevel.Length <= 0) continue;
            if (level.NextLevel[i].Icon)
            {
                Images[i].GetComponent<Image>().sprite = level.NextLevel[i].Icon;
                Images[i].transform.localScale = level.NextLevel[i].IconSize;
            }
        }

        Level = level;
    }

    public void Choose(int i)
    {
        _levelProgression.TakeLevel(Level.NextLevel[i], i);
        Close();
    }

    public void Close()
    {
        _background.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            Images[i].SetActive(false);
        }
    }
}
