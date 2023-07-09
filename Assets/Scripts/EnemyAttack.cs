using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int damange = 10;
    [SerializeField] bool isEnemy = false;
    public virtual void OnTriggerEnter(Collider other)
    {
        
        if(isEnemy == false)
        {
            var hit = other.gameObject.GetComponent<Enemy>();
            if (hit != null)
            {
                Actioner(hit, damange);
                Debug.Log("PlayerHit");
            }
        }
        else if (isEnemy == true)
        {
            var hit = other.gameObject.GetComponent<Player>();
            var hit2 = other.gameObject.GetComponent<ObjectDamangeable>();
            if (hit != null)
            {
                Actioner(hit, damange);
                Debug.Log("EnemyHit");
            }
            else if (hit2 != null)
            {
                Actioner(hit2, damange);
            }
        }

    }
    public virtual void Actioner(IDamangeable collider, int attack) //Basic Attack
    {
        collider.CauseDamange(attack);
    }
}
