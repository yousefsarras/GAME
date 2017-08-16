using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public PlayerManager player;
    public PlayerAbilities abilities;

    public void Start()
    {
        abilities = GameObject.Find("Player").GetComponent<PlayerAbilities>();
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy.enemyStats.currentHealth - player.playerStats.attackDamage <= 0)
            {
                player.addGold(enemy.enemyStats.goldWorth);
                //Will run first to fix range issue
                player.addXp(enemy.enemyStats.xpWorth);
                GameMaster.KillEnemy(enemy);
            }
            enemy.DamageEnemy(player.playerStats.attackDamage);
            abilities.goExist = false;
            Destroy(gameObject);
        }

        if (collision.CompareTag("Obstacle")|| collision.CompareTag("Through"))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 1f);
    }
}
