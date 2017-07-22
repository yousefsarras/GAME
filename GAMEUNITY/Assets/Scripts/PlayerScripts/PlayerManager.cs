using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float fallBoundary = -20;
    public bool noRegenMana = false;
    public bool dead = false;

    public CameraFollow cam;
    [System.Serializable]    //Serializable class
    public class PlayerStats
    {   //Health
        public int maxHealth = 100;
        private int _currentHealth;
        public int currentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
        }
        //Mana
        public int maxMana = 100;
        public int manaRegen = 5;
        private int _currentMana;
        public int currentMana
        {
            get { return _currentMana; }
            set { _currentMana = Mathf.Clamp(value, 0, maxHealth); }
        }

        //Level and XP
        public int maxLevel = 20;
        private int _level;
        public int level
        {
            get { return _level; }
            set { _level = Mathf.Clamp(value, 1, 20); }
        }

        public double damage;

        public int xp = 0;

        public void Init()
        {
            currentHealth = maxHealth;
            currentMana = maxMana;
            level = 1;
            damage = 40;
        }
    }

    public PlayerStats playerStats = new PlayerStats();

    [SerializeField]
    private PlayerStatusIndicator statusIndicator;
    [SerializeField]
    private UIStatusIndicator UIstatusIndicator;

    private void Start()
    {
        playerStats.Init();
        if(statusIndicator == null || UIstatusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on Player");
        }else
        {
            statusIndicator.SetHealth(playerStats.currentHealth, playerStats.maxHealth);
            statusIndicator.SetMana(playerStats.currentMana, playerStats.maxMana);
            statusIndicator.SetLevel(playerStats.level);

            //UI
            UIstatusIndicator.SetHealth(playerStats.currentHealth, playerStats.maxHealth);
            UIstatusIndicator.SetMana(playerStats.currentMana, playerStats.maxMana);
        }

    }
    public void DamagePlayer(int damage)
    {
        playerStats.currentHealth -= damage;
        if(playerStats.currentHealth <= 0)
        {
            dead = true;
            cam.enabled = false;
            GameMaster.KillPlayer(this);
        }

        statusIndicator.SetHealth(playerStats.currentHealth, playerStats.maxHealth);
        UIstatusIndicator.SetHealth(playerStats.currentHealth, playerStats.maxHealth);
    }

    public void LoseMana(int manaCost)
    {
        playerStats.currentMana -= manaCost;
    }

    private void Update()
    {
        statusIndicator.SetMana(playerStats.currentMana, playerStats.maxMana);
        UIstatusIndicator.SetMana(playerStats.currentMana, playerStats.maxMana);
        if (this.transform.position.y <= fallBoundary)
        {
            DamagePlayer(999999);
        }

        if(playerStats.currentMana < 100 && noRegenMana == false)
        {
            noRegenMana = true;
            StartCoroutine("RegenMana");
        }
    }

    IEnumerator RegenMana()
    {
        playerStats.currentMana += playerStats.manaRegen;
        yield return new WaitForSeconds(1);
        noRegenMana = false;
    }

    public int getMana()
    {
        return playerStats.currentMana;
    }

    public void addXp(int xp)
    {
        playerStats.xp += xp;
        //Static for now
        if(playerStats.xp == 20)
        {
            levelUp(playerStats.level++);
        }
    }

    public void levelUp(int currentLevel)
    {
        playerStats.level++;
        statusIndicator.SetLevel(playerStats.level);
        //add health ie: 10 * currentLevel * 0.7
        playerStats.damage  = playerStats.damage + currentLevel * 0.25; 
        //add movement speed ie: 
        playerStats.xp = 0;

    }

}
