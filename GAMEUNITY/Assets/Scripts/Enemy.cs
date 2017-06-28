using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

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
        private int _currentMana;
        public int currentMana
        {
            get { return _currentMana; }
            set { _currentMana = Mathf.Clamp(value, 0, maxHealth); }
        }
    }
    
}
