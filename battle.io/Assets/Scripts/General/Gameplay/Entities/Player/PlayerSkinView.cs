public class PlayerSkinView : SkinView
{
    private void Start()
    {
        SetSkin(Game.Context.PlayerSkin);
    }

    public void SetSkin(Skin skin)
    {
        _body.sprite = skin.Body;
        _arms[0].sprite = skin.Arm;
        _arms[1].sprite = skin.Arm;
    }
}