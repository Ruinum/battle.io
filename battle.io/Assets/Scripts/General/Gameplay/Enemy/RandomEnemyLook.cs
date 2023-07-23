using UnityEngine;

public class RandomEnemyLook : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private SpriteRenderer[] _arms;

    [SerializeField] private Appearance[] _appearances;

    private void Start()
    {
        int index = Random.Range(0, _appearances.Length);
        
        _body.sprite = _appearances[index].Body;
        _arms[0].sprite = _appearances[index].Arm;
        _arms[1].sprite = _appearances[index].Arm;
    }
}
