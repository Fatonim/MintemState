using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe : MonoBehaviour
{
    private float timeToAttack;

    public Transform attackPos;
    public LayerMask enemy;
    public int damage;
    public float attackRadius;
    public float timeAttack;
    public Animator animSword;

    [HideInInspector] public CameraScript camera;

    private void Update()
    {
        if (timeToAttack > 0)
            timeToAttack -= Time.deltaTime;
    }

    public void StartAttack()
    {
        if (timeToAttack <= 0)
        {
            animSword.SetTrigger("attack");
            timeToAttack = timeAttack;
        }
    }

    public void EndAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            camera.StartShake();
            enemies[i].GetComponent<TakeDamage>().TakeDamages(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
}
