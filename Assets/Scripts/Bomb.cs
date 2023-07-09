using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour
{
    [SerializeField] float _counter;
    [SerializeField] int _damange = 30;
    [SerializeField] GameObject _explotion;
    [SerializeField] private List<Enemy> _rangeEnemies = new List<Enemy>();

    public float _ratio = 2;
    public BombType _bombType;
    private Renderer _rd;
    Vector3 calculo;

    private void Start()
    {
        _rd = gameObject.GetComponentInChildren<Renderer>();
        gameObject.SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();
        _counter = 0;
        calculo = new Vector3(Mathf.Sin(0.01f) * 0.1f, Mathf.Sin(0.01f) * 0.1f, Mathf.Sin(0.01f) * 0.1f); 
        SetColor(Random.Range(0,3));

        isExploded = false;

    }

    void Update()
    {
        _counter += Time.deltaTime;
        transform.localScale += calculo;
        
    }

    bool isExploded;
    //Ccrear una lista con todos los enemigos en un area
    private void OnTriggerStay(Collider other)
    {
        if (isExploded) return;

        var hit = other.gameObject.GetComponent<Enemy>();
       
        if (hit != null)
        {
            _rangeEnemies.Add(hit);

            if (_counter >= 2.5f)
            {
                /*
                //refactor this entirely if statement using linq
                if (hit._EnemyType.Equals(_bombType))
                {
                    hit.gameObject.GetComponent<IDamangeable>().CauseDamange(_damange/2);
                    //Debug.Log(other);
                    Instantiate(_explotion, transform.position, Quaternion.Euler(0, 0, Camera.main.transform.position.z));
                    gameObject.SetActive(false);   
                    Debug.Log("daño");
                }
                else
                {
                    hit.gameObject.GetComponent<IDamangeable>().CauseDamange(_damange);
                    //Debug.Log(other);
                    Instantiate(_explotion, transform.position, Quaternion.Euler(0, 0, Camera.main.transform.position.z));
                    gameObject.SetActive(false);
                    Debug.Log("mitad daño");
                    
                }
                */
                isExploded = true;
                Explode();
            }
        }

    }
    //con agregate crear una tuple en la que separe los 3 tipos de enmigos y dsp con un foreach voy a hacerles yamete kudasai

    public float radius = 3.5f;
    public IEnumerable<Enemy> onArea = new List<Enemy>();
    void Explode()
    {
        onArea = EnemyGrid.Instance.Query(transform.position + new Vector3(-radius, 0, -radius),
                transform.position + new Vector3(radius, 0, radius),
                x => {
                    var position2d = x - transform.position;
                    position2d.y = 0;
                    return position2d.sqrMagnitude < radius * radius;
                });

        if (onArea.Count() <= 0) gameObject.SetActive(false);

        foreach (var entity in onArea)
        {
            if (entity.gameObject.activeSelf)
            { 
               entity.CauseDamange(_damange);
               gameObject.SetActive(false);
            }
        }
    }

    public void SetColor(int a)
    {
        switch (a)
        {
            case 0:
                _rd.material.color= Color.blue;
                _bombType = BombType.blue;
                break;
            case 1:
                _rd.material.color = Color.green;
                _bombType = BombType.green;
                break;
            case  2:
                _rd.material.color = Color.red;
                _bombType = BombType.red;
                break;
            
        }
    }

    public enum BombType
    {
        blue = 1
        ,red = 0
        ,green = 2
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position,_ratio);
    }
}
