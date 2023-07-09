using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storeManager : MonoBehaviour
{
    public static storeManager instance;
    public int _coins;
    public int _intentos;
    [SerializeField] Dictionary<itemID, int> itemDictionary = new Dictionary<itemID, int>();
    [SerializeField] List<itemID> _items = new List<itemID>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _coins = PlayerPrefs.GetInt("PlayerCoins");
        DontDestroyOnLoad(instance);
        addQuantity(_items[0]);
        addQuantity(_items[1]);
    }
    private void Start()
    {
        _intentos = 0;
    }
    public void addQuantity(itemID item)
    {
        if (!itemDictionary.ContainsKey(item))
            itemDictionary.Add(item, 1);
        else
            itemDictionary[item]++;
    }
    public void restQuantity(itemID item)
    {
        if (itemDictionary.ContainsKey(item))
            itemDictionary[item]--;
    }
    public int GetValue(itemID item)
    {
        if (itemDictionary.ContainsKey(item))
            return itemDictionary[item];

        return default;
    }
    public int SetValue(itemID item, int value)
    {
        if (itemDictionary.ContainsKey(item))
        {
            return itemDictionary[item] = value;
        }

        return default;
    }
    public void SaveCoins()
    {
        PlayerPrefs.SetInt("PlayerCoins", _coins);
    }
    
}
