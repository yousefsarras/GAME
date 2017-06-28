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
    }

    public PlayerStats playerStats = new PlayerStats();

    public void Start()
    {
        playerStats.currentHealth = playerStats.maxHealth;
        playerStats.currentMana = playerStats.maxMana;
    }
    public void DamagePlayer(int damage)
    {
        playerStats.currentHealth -= damage;
        if(playerStats.currentHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }

    public void LoseMana(int manaCost)
    {
        playerStats.currentMana -= manaCost;
        Debug.Log("Mana: " + playerStats.currentMana + "/" + playerStats.maxMana);
    }

    private void Update()
    {
        if(this.transform.position.y <= fallBoundary)
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
