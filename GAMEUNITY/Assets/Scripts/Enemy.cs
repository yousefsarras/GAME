using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Collider2D xpRange;
    public XpRangeGain xpGain;
    [System.Serializable]    //Serializable class
    public class EnemyStats
    {
        public int maxHealth = 100;
        private int _currentHealth;
        public int currentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int maxMana = 100;
        public int manaRegen = 5;

        public int xpWorth = 10;
        public void Init()
        {
            currentHealth = maxHealth;
        }
    }

    public EnemyStats enemyStats = new EnemyStats();

    public void Start()
    {
        enemyStats.Init();
        xpRange.enabled = false;
        xpGain = gameObject.GetComponentInChildren<XpRangeGain>();
    }

    public void DamageEnemy(int damage)
    {
        enemyStats.currentHealth -= damage;
        if(enemyStats.currentHealth <= 0)
        {
            xpRange.enabled = true;
        }
    }



}
