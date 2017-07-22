using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.DamageEnemy(damage);
        }

        if (collision.CompareTag("Turret"))
        {
            TurretManager turret = collision.GetComponentInParent<TurretManager>();//find child component
            turret.DamageTurret(damage);
        }
    }
}
