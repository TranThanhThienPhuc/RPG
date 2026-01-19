using UnityEngine;

[CreateAssetMenu(fileName = "Rewards", menuName = "Quests/Rewards")]
public class Rewards : ScriptableObject
{
    public int exp;

    public void Reward()
    {
        LevelManager.Instance.AddExp(exp);
    }
}
