using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPool : MonoBehaviour
{
    public static CoinsPool instance;
    public Coin coin;
    public ObjectPool<Coin> pool;
    [SerializeField] int _coinSpawn = 5;

    [SerializeField] float timer;

    void Start()
    {
        instance = this;
        pool = new ObjectPool<Coin>(CoinReturn, Coin.TurnOn, Coin.TurnOff, _coinSpawn);
    }
    public void Set(Vector3 pos)
    {
       
        var e = pool.GetObject(); //Le decimos al pool que me devuelva el objeto
        e.transform.position = pos;   //A eso que me dió Seteo parametros
       
    }
    public Coin CoinReturn() // Lo que le tengo que pasar para que instancie T
    {
        return Instantiate(coin);
    }
}
