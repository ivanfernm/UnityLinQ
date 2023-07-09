using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int difficult_;
    public Enemy[] enemy;
    public ObjectPool<Enemy> pool;
    private EnemyManager _enemyManager;

    public List<Enemy> _spawnedInPoral = new List<Enemy>();
    [SerializeField] int _enemySpawn = 5;
    
    [SerializeField]float timer;

    //[SerializeField] GameObject parentObject;
    [SerializeField] EnemyGrid grid;

    void Start()
    {
        _enemyManager = EnemyManager.instance;
        timer = Random.Range(0.5f, 3f);
        pool = new ObjectPool<Enemy>(EnemyReturn, Enemy.TurnOn, Enemy.TurnOff, _enemySpawn);
    }


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer<=0)
        {
            var e = pool.GetObject(); //Le decimos al pool que me devuelva el objeto
            e.transform.position = transform.position;    //A eso que me dió Seteo parametros
            e.transform.forward = transform.forward;
            _spawnedInPoral.Add(e);
            timer = 15f;            
        }
    }
    public Enemy EnemyReturn() // Lo que le tengo que pasar para que instancie T
    {
        var random = Random.Range(0, enemy.Length);
        enemy[random].Asign(this);

        var temp = Instantiate(enemy[random]);

        //if (parentObject != null) temp.transform.parent = parentObject.transform;
        //if (!parentObject.activeSelf) parentObject.SetActive(true);

        if (grid != null) 
        { 
           temp.transform.parent = grid.gameObject.transform;
           grid.PublicRefresh();
        }

        return temp;
    }
}
