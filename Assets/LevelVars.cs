using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelVars : MonoBehaviour
{
    public EnemyManager enemyManager;
    public Player player;

    private void Start()
    {
        if (!enemyManager)
        {
            enemyManager = EnemyManager.instance;
        }
    }

    //using aggregate create function that returns a tuple of the player atackCommand and the enemy Hitcount in AllSpawnedEnemies 

   //IA2-P1
    public Tuple<float, float> AccuarcyHit()
    {
        var enemys = enemyManager.GetAllSpawnedEnemys();
        var playerHit = player.atackCommand;

        var result = enemys.Aggregate(Tuple.Create(0f, 0f), (x,y) =>
        {
            var hit = x.Item1;
            var acc = x.Item2;

            hit += y.Hitcount;
            acc = hit / playerHit; 

            return Tuple.Create(hit, acc);
        });

        return result;
    }

}
