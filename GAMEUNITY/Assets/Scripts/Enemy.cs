using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Collider2D xpRangeCollider;
    public XpRangeGain xpRangeGain;

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
        public int goldWorth = 10;
        public void Init()
        {
            currentHealth = maxHealth;
        }
    }

    public EnemyStats enemyStats = new EnemyStats();


    public void Start()
    {
        xpRangeCollider.enabled = false;
        enemyStats.Init();
        xpRangeGain = gameObject.GetComponentInChildren<XpRangeGain>();
    }

    public void DamageEnemy(int damage)
    {
        enemyStats.currentHealth -= damage;
        //Enemy is dead after this statement
        if(enemyStats.currentHealth <= 0)
        {
            xpRangeCollider.enabled = true;
        }
    }

    public void Update()
    {
        if (xpRangeGain.xpReaper)
        {
            GameMaster.KillEnemy(this);
        }
    }
}
