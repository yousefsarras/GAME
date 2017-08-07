using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float lifeTime = 1.5f;
    public int damage = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerManager>().DamagePlayer(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Turret"))
        {
            collision.GetComponentInParent<TurretManager>().DamageTurret(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject, lifeTime);
    }
}
