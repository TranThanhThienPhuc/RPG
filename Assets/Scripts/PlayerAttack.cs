using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Vector2 size;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private BattleSystem battle;
    [SerializeField] private GameObject BattleStart;
    [SerializeField] private SpriteRenderer playerRen;
    [SerializeField] private List<SpriteRenderer> enemyRen;
    [SerializeField] private GameObject theEnemy;
    [SerializeField] LayerMask mask;
    [SerializeField] private int distance;

    private void Start()
    {
        GetAllEnemyRen();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
        if (battle.state == State.WIN)
        {
            Invoke("SetPlayerRenOn", 2f);
        }
    }

    void Attack()
    {
        var origin = transform.position;
        var hit = Physics2D.BoxCast(origin, size, Mathf.Atan2(movement.direction.y, movement.direction.x) * Mathf.Rad2Deg, movement.direction, distance, mask);
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Debug.Log(hit.collider.name);
            print(hit);
            print(hit.transform.name);

            BattleStart.SetActive(true);
            battle.state = State.START;
            theEnemy = hit.collider.gameObject;
            StartCoroutine(battle.BattleSetUp(hit.collider.gameObject.GetComponent<Enemy>().prefabObj));
            //StartCoroutine(battle.BattleSetUp());
            playerRen.enabled = false;
            movement.enabled = false;
            foreach (SpriteRenderer Ren in enemyRen)
                Ren.enabled = false;
        }
    }

    private void GetAllEnemyRen()
    {
        enemyRen = new();
        GameObject[] spriteObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in spriteObjects)
            enemyRen.Add(enemy.GetComponent<SpriteRenderer>());
    }
    private void SetPlayerRenOn()
    {
        playerRen.enabled = true;
        movement.enabled = true;
        Destroy(theEnemy);

        GetAllEnemyRen();
        foreach (SpriteRenderer Ren in enemyRen)
            Ren.enabled = true;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireCube(transform.position * movement.direction, size);
        Gizmos.DrawRay(transform.position, movement.direction);
    }
}