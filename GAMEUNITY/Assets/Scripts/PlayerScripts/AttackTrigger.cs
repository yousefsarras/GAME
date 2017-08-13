using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public PlayerManager player;

    public void Start()
    {
        player = GetComponentInParent<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            //see if incoming dmg will kill enemy
            if(enemy.enemyStats.currentHealth - player.playerStats.attackDamage <= 0)
            {
                player.addGold(enemy.enemyStats.goldWorth);
                //Debug.Log("gold: " + player.playerStats.gold);
            }
            enemy.DamageEnemy(player.playerStats.attackDamage);
        }

        if (collision.CompareTag("Turret"))
        {
            TurretManager turret = collision.GetComponentInParent<TurretManager>();//find child component
            turret.DamageTurret(player.playerStats.attackDamage);
        }
    }
}
