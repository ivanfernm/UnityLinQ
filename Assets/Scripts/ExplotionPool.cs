using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionPool : MonoBehaviour
{
    public static ExplotionPool instance;
    public Smoke smoke;
    public ObjectPool<Smoke> pool;
    [SerializeField] int _coinSpawn = 5;

    [SerializeField] float timer;

    void Start()
    {
        instance = this;
        pool = new ObjectPool<Smoke>(SmokeReturn, Smoke.TurnOn, Smoke.TurnOff, _coinSpawn);
    }
    public void Set(Vector3 pos)
    {

        var e = pool.GetObject(); //Le decimos al pool que me devuelva el objeto
        e.transform.position = pos;   //A eso que me dió Seteo parametros

    }
    public Smoke SmokeReturn() // Lo que le tengo que pasar para que instancie T
    {
        return Instantiate(smoke);
    }
}
