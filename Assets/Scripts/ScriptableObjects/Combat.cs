using UnityEngine;

[CreateAssetMenu(fileName = "Combat", menuName = "Scriptable Objects/Combat")]
public class Combat : ScriptableObject
{
    public Target[] target;

    [System.Serializable]
    public struct Target
    {
        public Enemy enemy;
    }
}
