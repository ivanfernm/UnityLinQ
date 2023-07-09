using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public LeaderBoardManager _LeaderBoardManager;

    public Enemy _Leader;
    public List<EnemySpawner> _PortalLists = new List<EnemySpawner>();

//TP IA2
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        StartCoroutine(getFirstLeader());
        StartCoroutine(StartLeader());
        StartCoroutine(SetRank());
        
    }
    
    public IEnumerable<Enemy> GetAllSpawnedEnemys()
    {
        return _PortalLists.SelectMany(x => x._spawnedInPoral)
            .Where(x => x.gameObject.activeSelf == true).ToList();;
    }

    public Enemy GetLeader(List<Enemy> list)
    {
        var result = new Enemy();
        var List = list.OrderByDescending(x => x._life);
        return result = list.First();
    }

    public void SetLeader()
    {
        _Leader = GetLeader(GetAllSpawnedEnemys().ToList());
        _Leader.AscendToLeader();
    }

    //IA2
    public List<Enemy> GetThreeMoreHitCount(List<Enemy> list)
    {
        var result = new List<Enemy>();
        result = list.OrderByDescending(x => x.Hitcount).Take(3).ToList();
        return result;
    }

   
    IEnumerator StartLeader()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if (!_Leader.gameObject.activeSelf)
            {
                SetLeader();
            }

        }
    }

    IEnumerator getFirstLeader()
    {
        yield return new WaitForSeconds(2);
        SetLeader();
    }
    IEnumerator SetRank()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            var count = 0;
            foreach (var e in GetThreeMoreHitCount(GetAllSpawnedEnemys().ToList())) 
            {
                e.SetPositionCrown(count);
                count++;
                _LeaderBoardManager.UpdateLeaderBoard();
            }

        }

    }
}