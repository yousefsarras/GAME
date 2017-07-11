using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float fallBoundary = -20;
    public bool noRegenMana = false;

    [System.Serializable]    //Serializable class
    public class PlayerStats
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
        private int _currentMana;
        public int currentMana
        {
            get { return _currentMana; }
            set { _currentMana = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            currentHealth = maxHealth;
            currentMana = maxMana;
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

    public float getMana()
    {
        return playerStats.currentMana;
    }

}
