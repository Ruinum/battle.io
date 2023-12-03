public class PlayerSkinView : SkinView
{
    private void Start()
    {
        var playerSkin = Game.Context.PlayerSkin;
        _body.sprite = playerSkin.Body;
        _arms[0].sprite = playerSkin.Arm;
        _arms[1].sprite = playerSkin.Arm;
    }
}