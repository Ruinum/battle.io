using UnityEngine;

public interface IClassAbility
{
    void Initialize(GameObject player, ClassConfig config);
    void UseAbility();
}