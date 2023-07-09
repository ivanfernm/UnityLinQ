using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonWIN : MonoBehaviour
{
    [SerializeField] GameObject _win;
    [SerializeField] string _scene;
    [SerializeField] GameObject _audio;
    [SerializeField] AudioClip _clip;
   public void OnPress()
   {
        _win.SetActive(true);
        storeManager.instance._coins += 50;
        _audio.GetComponent<AudioSource>().clip = _clip;
        _audio.GetComponent<AudioSource>().Play();
        StartCoroutine(changeScene());
   }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(_scene);
    }
}
