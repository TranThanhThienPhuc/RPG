using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Quests/Dialogue")]
public class Dialogue : ScriptableObject
{
    public Dialogues[] conversations;

    [System.Serializable]
    public struct Dialogues
    {
        public string title;
        [TextArea] public string[] lines;
    }
    
}