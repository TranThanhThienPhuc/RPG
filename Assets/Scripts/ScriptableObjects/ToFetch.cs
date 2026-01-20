using UnityEngine;

[CreateAssetMenu(fileName = "ToFetch", menuName = "Quests/ToFetch")]
public class ToFetch : ScriptableObject
{
    public Target[] FetchList;

    [System.Serializable]
    public struct Target
    {
        public Items type;
        public int amount;
    }
}
