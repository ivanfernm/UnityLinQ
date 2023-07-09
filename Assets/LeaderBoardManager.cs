using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LeaderBoardManager : MonoBehaviour
{
   public List<Text> _texts = new List<Text>();
   
   public EnemyManager _enemyManager;
   
   private void Start()
   {
      _enemyManager = EnemyManager.instance;
      _enemyManager._LeaderBoardManager = this;
   }
   
   //IA2
   public void UpdateLeaderBoard()
   {
      var list = _enemyManager.GetThreeMoreHitCount(_enemyManager.GetAllSpawnedEnemys().ToList()).Select(x => x.ToString()).ToList();
      for (int i = 0; i < list.Count; i++)
      {
         _texts[i].text = list[i];
      }
   }
   
   


}
