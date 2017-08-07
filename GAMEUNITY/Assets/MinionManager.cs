using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : Enemy {
    public new Collider2D collider;

    public new class EnemyStats
    {
        public double damage;
    }

    public new void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    public void Attack(double damage)
    {

    }




}
