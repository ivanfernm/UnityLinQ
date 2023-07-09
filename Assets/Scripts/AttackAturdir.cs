using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAturdir : EnemyAttack
{
    public override void Actioner(IDamangeable collider, int attack)
    {
        base.Actioner(collider, attack);
        Debug.Log("aaa");
        collider.ExtraAction();
    }
}
