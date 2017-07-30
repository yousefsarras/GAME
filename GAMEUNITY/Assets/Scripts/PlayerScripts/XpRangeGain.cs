using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpRangeGain : MonoBehaviour {

    private Enemy enemy;
    public bool xpReaper;

    public void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        //xpReaper = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            xpReaper = true;
            PlayerManager player = collision.GetComponent<PlayerManager>();
            player.addXp(enemy.enemyStats.xpWorth);
        }
    }
}
