using System;

[Serializable]
public sealed class AnimationTimelineKey
{
    public string Name;
    public AnimationTimelineKeyType Type;
    public float Time;

    public Action OnKeyInvoke;
}
