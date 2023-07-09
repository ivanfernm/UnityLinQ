using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class Ads : MonoBehaviour
{
    
    void Start()
    {
        Advertisement.Initialize("4473711", true);
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady("Rewarded_Android")) 
        {
            ShowOptions options = new ShowOptions { resultCallback = AdFinished };
            Advertisement.Show("Rewarded_Android", options);
        }
    }
    private void AdFinished(ShowResult result)
    {
        if(result == ShowResult.Finished)
        {
            storeManager.instance._coins += 50;
            SceneManager.LoadScene("menu2");
        }
        else if(result == ShowResult.Skipped)
        {
            return;
        }
        else
        {
            return;
        }
    }
}
