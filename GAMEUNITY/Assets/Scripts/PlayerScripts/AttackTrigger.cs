using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("We hit " + collision.name + " and did " + damage + " damage!");
        }

        if (collision.CompareTag("Turret"))
        {
            TurretManager turret = collision.GetComponent<TurretManager>();
            turret.DamageTurret(damage);
        }
    }
}
