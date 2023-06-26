using System;

[Serializable]
public sealed class AnimationTimelineKey
{
    public string Name;
    public AnimationTimelineKeyType KeyType;
    public float KeyTime;
    
    public int ID => _id;
    private int _id;

    public void SetID(int id) => _id = id;
}
