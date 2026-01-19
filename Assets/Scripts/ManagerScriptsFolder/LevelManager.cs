using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static int level = 1;
    public static int expRequired = 100;
    public static int currentExp = 0;


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
            level++;
            currentExp -= expRequired;
            expRequired *= 2;
        }
    }

}
