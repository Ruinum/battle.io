using System;

[Serializable]
public struct AnimationTimelineKey
{
    public string Name;
    public AnimationTimelineKeyType Type;
    public float Time;

    public Action OnKeyInvoke;
}
