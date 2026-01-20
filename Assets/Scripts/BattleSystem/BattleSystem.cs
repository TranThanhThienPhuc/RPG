using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.FilterWindow;

public enum State { START, LOST, WIN, PLAYER, ENEMY }


public class BattleSystem : MonoBehaviour
{
    [Header("Unit Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    [Header("Unit Spawnpoints")]
    public Transform playerStation;
    public Transform enemyStation;

    [Header("Unit Scripts")]
    Unit playerUnit;
    Unit enemyUnit;

    [Header("UI System")]
    public TMP_Text dialogueText;
    public string basicAttackChant;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    [Header("Ingame states")]
    public State state;
    [SerializeField] private bool inBattle;
    [SerializeField] private bool attackable;
    public bool battleEnd;

    [Header("Game Object stuffs")]
    [SerializeField] private GameObject playerIG;
    [SerializeField] private GameObject Battle;
    [SerializeField] private GameObject[] options;

    public IEnumerator BattleSetUp(GameObject enemyObj) //Setting up the battle UI
    {
        //Instantiate player and add unit codes
        GameObject player = Instantiate(playerPrefab, playerStation);
        playerUnit = player.GetComponent<Unit>();

        //Instantiate enemy and add unit codes
        GameObject enemy = Instantiate(enemyObj, enemyStation);
        enemyUnit = enemy.GetComponent<Unit>();

        dialogueText.text = "Battle starting...";

        //Setting UI system
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        //Game starts by player turn
        state = State.PLAYER;
        PlayerTurn();
        yield break;
    }
    IEnumerator BasicAttack()
    {
        if (attackable == true)
        {
            enemyUnit.TakeDamage(playerUnit.rockDamage);
            enemyHUD.SetHP(enemyUnit.currentHP);

            dialogueText.text = basicAttackChant;
            attackable = false;


            yield return new WaitForSeconds(2f);

            if (enemyUnit.currentHP <= 0)
            {
                state = State.WIN;
                dialogueText.text = "You defeated the enemy!";
                yield return new WaitForSeconds(2f);
                EndBattle();
            }
            else
            {
                state = State.ENEMY;
                enemyUnit.anim.SetBool("Hurt", false);
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        enemyUnit.anim.SetBool("Attack", true);
        int attackChance = Random.Range(1, 10); //Randomize if enemy attack successfully
        print(attackChance);
        if (attackChance <= 8)
        {
            yield return new WaitForSeconds(0.8f);
            bool isDead = playerUnit.TakeDamage(enemyUnit.enemyDamage);
            dialogueText.text = enemyUnit.unitName + " attacks";
            playerHUD.SetHP(playerUnit.currentHP);
            enemyUnit.anim.SetBool("Attack", false);

            if (isDead) //Check if player is dead
            {
                state = State.LOST;
                dialogueText.text = "You were defeated.";
                yield return new WaitForSeconds(2f);
                EndBattle();
            }
            else
            {
                yield return new WaitForSeconds(0.8f);
                state = State.PLAYER;
                playerUnit.anim.SetBool("Hurt", false);
                PlayerTurn();
            }
        }

        else if (attackChance > 8) //If attack fails
        {
            yield return new WaitForSeconds(0.8f);
            dialogueText.text = enemyUnit.unitName + " attacks missed";
            enemyUnit.anim.SetBool("Attack", false);
            state = State.PLAYER;
            PlayerTurn();
        }

        else
        {
            Debug.LogError("Something is wrong");
        }
    }

    IEnumerator PlayerHeal()
    {
        if (attackable)
        {
            playerUnit.Heal(playerUnit.healPercentage);

            dialogueText.text = "Healed " + playerUnit.healPercentage + " hp";
            yield return new WaitForSeconds(2f);

            playerHUD.SetHP(playerUnit.currentHP);
            state = State.ENEMY;
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnBasicAttackButton()
    {
        if (state != State.PLAYER)
            return;

        StartCoroutine(BasicAttack());
    }

    public void OnHealButton()
    {
        if (state != State.PLAYER)
            return;

        StartCoroutine(PlayerHeal());
    }

    public void PlayerTurn()
    { 
        if(state != State.PLAYER)
        {
            return;
        }
        foreach (GameObject go in options)
            go.SetActive(true);
        dialogueText.text = "Choose an action:";
        attackable = true;
    }


    public void EndBattle()
    {
        if (state == State.WIN)
        {
            Destroy(playerUnit.gameObject);
            Destroy(enemyUnit.gameObject);
            Battle.SetActive(false);
            LevelManager.Instance.AddExp(1000);
        }
        else if (state == State.LOST)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
}