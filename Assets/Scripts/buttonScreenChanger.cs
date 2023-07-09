using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonScreenChanger : MonoBehaviour
{
    public GameObject LoadingBackground;
    public Image LoadingProgress;
    int random;
    public void ChangeToScene(string scene)
    {
        StartCoroutine(loading(scene));
    }

    public IEnumerator loading(string Scene)
    {
        LoadingBackground.SetActive(true);
        AsyncOperation MyLoading = SceneManager.LoadSceneAsync(Scene,LoadSceneMode.Single);

        while(MyLoading.isDone == false)
        {
            LoadingProgress.fillAmount = MyLoading.progress;
            yield return null;
        }
    }

    public void GoBack()
    { SceneManager.LoadScene("menu2"); }

    public void MinigameRandom()
    {
        storeManager.instance._intentos++;
        random = Random.Range(0, 4);
        if(storeManager.instance._intentos <= 2)
        {
            switch (random)
            {
                case 0:
                    ChangeToScene("Minigame1");
                    break;
                case 1:
                    ChangeToScene("Minigame2");
                    break;
                case 2:
                    ChangeToScene("Minigame3");
                    break;
                case 3:
                    ChangeToScene("Minigame4");
                    break;

            }
        }
        else
        {
            return;
        }
    }

    public void Exit()
    {
        storeManager.instance.SaveCoins();
        Application.Quit(); 
    }
}
