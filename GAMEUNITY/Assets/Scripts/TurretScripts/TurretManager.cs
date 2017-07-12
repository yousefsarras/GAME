using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour {

	[System.Serializable]
    public class TurretStats
    {
        public int maxHealth = 100;
        public float startPercentHealth = 1f;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth) ;}
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public TurretStats stats = new TurretStats();
    [Header("Optional")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start()
    {
        stats.Init();
        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    public void DamageTurret(int damage)
    {
        stats.curHealth -= damage;
        if(stats.curHealth <= 0)
        {
            GameMaster.KillTurret(this);
        }
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }
}
