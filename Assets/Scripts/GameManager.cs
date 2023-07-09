using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int _difficulty = 1;
    //public int _coins;
    //public int _bombs;
    //public int _liquid;
    Scene mainScene;
    string sceneName;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Load();
        DontDestroyOnLoad(instance);
    }
    void Start()
    {
        mainScene = SceneManager.GetActiveScene();
        sceneName = mainScene.name;
        
        switch(_difficulty)
        {
            case (0):
                EventManager.Trigger(EventManager.EventType.Easy);
                break;
            case (1):
                EventManager.Trigger(EventManager.EventType.Normal);
                break;
            case (2):
                EventManager.Trigger(EventManager.EventType.Hard);
                break;
        }
        if (sceneName == "Level")
        {

        }
    }
    //public void AddCoins(int coinsAmount)
    //{
    //    _coins = _coins + coinsAmount;
        
    //}
    //public void Save()
    //{
    //    PlayerPrefs.SetInt("PlayerCoins", _coins);
    //    PlayerPrefs.Save();
    //}
    private void Load()
    {
        PlayerPrefs.GetInt("PlayerCoins", 0);
    }    
}
