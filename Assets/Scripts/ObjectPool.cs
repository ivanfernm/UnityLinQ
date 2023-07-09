    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> //Pide un objeto cualquiera
{
    public delegate T FactoryMethod();//Guarda el método que instancia

    List<T> _currentStock = new List<T>(); //La lista de "T"
    FactoryMethod _factoryMethod; //Inicializamos FactoryMethod
    Action<T> _turnOnCallback; //Devuelve el void de T para prenderlo
    Action<T> _turnOffCallback;//              "       para apagarlo
    public int _initialStock = 5;
    //               |       Objeto        |   Metodo para encender  |    Método para apagar    |   Cantidad inicial
    public ObjectPool(FactoryMethod factory, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialStock = 5)
    {
        _factoryMethod = factory;
        _turnOnCallback = turnOnCallback;         //Inicializamos las variables
        _turnOffCallback = turnOffCallback;
        _initialStock = initialStock;

        for (int i = 0; i < initialStock; i++) //Recorre la lista
        {
            var o = _factoryMethod(); //Guarda Factory en una variable (Factory es el objeto que le pasamos)
            _turnOffCallback(o); //Lo apaga
            _currentStock.Add(o); //Lo añade a la lista
        }
    }
    /// <summary>
    /// Se utiliza para devolver el objeto que creamos.
    /// </summary>
    /// <returns></returns>
    public T GetObject()        
    {
        var result = default(T);

        if (_currentStock.Count > _initialStock) //Pregunta si en mi lista tengo 1 T (0 es el primero de la Lista)
        {
            result = _currentStock[0]; //Le da el primero
            _currentStock.RemoveAt(0); //Lo borra de la lista pq ya se lo dá
        }
        else
        {
            result = _factoryMethod(); // Si no tengo lo crea
        }
            
        _turnOnCallback(result); //Lo activa

        return result; //Lo devuelve
    }
    
    /// <summary>
    /// Devuelve el objeto a la lista del Object Pool
    /// </summary>
    /// <param name="o"></param>
    public void ReturnObject(T o) //Cuando devuelva el objeto
    {
        _turnOffCallback(o); //Lo apaga
        _currentStock.Add(o); //Lo vuelve a añadir
    }
}
