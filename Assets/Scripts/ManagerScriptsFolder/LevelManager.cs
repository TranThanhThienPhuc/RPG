using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int level;
    public int expRequired;
    public int currentExp;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddExp(int expPoints)
    {
        currentExp += expPoints;
        if(currentExp >= expRequired)
        {
            currentExp -= expRequired;
            expRequired *= 2;
        }
    }

}
