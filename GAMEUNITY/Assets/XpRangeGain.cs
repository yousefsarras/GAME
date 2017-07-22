using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpRangeGain : MonoBehaviour {

    private Enemy enemy;

    public void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            player.addXp(enemy.enemyStats.xpWorth);
            GameMaster.KillEnemy(enemy);
        }
    }
}
