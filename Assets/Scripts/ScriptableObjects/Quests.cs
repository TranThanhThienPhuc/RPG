using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quests/Quests")]
public class Quests : ScriptableObject
{
    public string questName;
    public Stages[] elements;
    public enum QuestStage {Dialogue, Fetch, Combat}

    [System.Serializable]
    public struct Stages
    {
        public QuestStage stage;
        public ScriptableObject data;
    }
}
