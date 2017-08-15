using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float fallBoundary = -20;
    public bool noRegenMana = false;
    public bool dead = false;
	[HideInInspector]
    public PlayerMovement moveController;
    public CameraFollow cam;
	public GameMaster gm;

    public WeaponObject[] itemBag;
	public int firstOpenBagSpace = 0;

    [System.Serializable]    //Serializable class
    public class PlayerStats
    {   //Health
        public int maxHealth = 500;
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

        public int attackDamage;

        public float movementSpd;

        public int xp = 0;
        public int gold = 0;

        public void Init()
        {
            currentHealth = maxHealth;
            currentMana = maxMana;
            level = 1;
            attackDamage = 40;
            gold = 0;
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
        moveController = GetComponent<PlayerMovement>();
		itemBag = new WeaponObject[4];
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
			gm.KillPlayer(this);
			cam.enabled = false;
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
        //Debug.Log("Xp: " + playerStats.xp);
        playerStats.xp += xp;
        //Static for now
        if(playerStats.xp == 20)
        {
            levelUp(playerStats.level++);
        }
    }

    public void levelUp(int currentLevel)
    {
        Debug.Log("level: " + playerStats.level);
        statusIndicator.SetLevel(playerStats.level);
        Debug.Log("level: " + playerStats.level);
        playerStats.maxHealth = (playerStats.maxHealth) + 10;
        playerStats.attackDamage  = playerStats.attackDamage + 3;
        moveController.moveSpeed = playerStats.movementSpd + 10;
        playerStats.xp = 0;

    }

    public void addGold(int goldAmount)
    {
        playerStats.gold += goldAmount;
    }

	public void findOpenSpace(){
		for (int i = 0; i < itemBag.Length; i++) {
			if (itemBag [i] == null) {
				firstOpenBagSpace = i;
				break;
			}
		}
	}

}
