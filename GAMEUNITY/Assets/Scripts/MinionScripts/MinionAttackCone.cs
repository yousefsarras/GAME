using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttackCone : MonoBehaviour {

    MinionAI minion;
    //Handle Ranged Attack
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletTimer;
    public float shootInterval;
    public Transform shootPoint;

    void Start () {
        minion = GetComponentInParent<MinionAI>();
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Turret"))
        {
            minion.currentTarget = collision.gameObject;
            Attack();
        }
    }

    public void Attack()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= shootInterval)
        {
            Vector2 direction = minion.currentTarget.transform.position - transform.position;
            direction.Normalize();

            GameObject bulletClone;
            bulletClone = Instantiate(bullet, shootPoint.position, shootPoint.rotation) as GameObject;
            bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            bulletTimer = 0;
        }
    }


}
