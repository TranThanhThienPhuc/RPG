using UnityEngine;


public class Unit : MonoBehaviour
{
    [Header("Unit Information")]
    public string unitName;

    [Header("Unit Health Stats")]
    public int maxHP;
    public int currentHP;

    [Header("Unit Damage Stats")]
    public int rockDamage;
    public int healPercentage;

    public int enemyDamage;

    [Header("Animator")]
    public Animator anim;

    public void Start()
    {
        currentHP = maxHP;
    }
    public bool TakeDamage(int dmg)
    {
        anim.SetBool("Hurt", true);
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            anim.SetBool("Death", true);
            return true;
        }
        else
        {
            return false;

        }


    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}