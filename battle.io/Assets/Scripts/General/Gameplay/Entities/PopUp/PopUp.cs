using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class PopUp : PoolObject
{    
    [SerializeField] private TMP_Text _textMeshPro;
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void ShowPopUp(string text, Vector3 position, Color color)
    {
        _canvas.sortingOrder = 5;
        var resultPosition = position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1);
        Active(resultPosition, Quaternion.identity);

        _textMeshPro.text = text;
        _textMeshPro.color = color;

        transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.3f);
        _textMeshPro.DOFade(0, 1.25f).OnComplete(() => ReturnToPool());
    }

    public TMP_Text GetTMPText() => _textMeshPro;
}
