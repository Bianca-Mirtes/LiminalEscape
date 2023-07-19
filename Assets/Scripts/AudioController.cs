using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioClip Menu;
    public AudioClip levelOne;
    public AudioClip selectionSound, moveMenuSound;
    public AudioClip tension;

    [Header("Menu")]
    public AudioSource selection;
    public AudioSource move;
    public AudioSource levelAmbiance;
    public AudioSource menuAmbiance;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("MenuInicial"))
        {
            AudioClip teste = Menu;
            menuAmbiance.clip = teste;
            menuAmbiance.volume = 0.635f;
            menuAmbiance.Play();
            //GameObject.FindGameObjectForType<MenuController>().GetComponent<AudioSource>().PlayOneShot(menu);
        }
        if (SceneManager.GetActiveScene().name.Equals("level1"))
        {
            AudioClip teste = levelOne;
            levelAmbiance.clip = teste;
            levelAmbiance.volume = 0.635f;
            levelAmbiance.Play();
            //GameObject.FindGameObjectForType<MenuController>().GetComponent<AudioSource>().PlayOneShot(levelOne);
        }
    }

    public void MoveButton()
    {
        AudioClip teste = moveMenuSound;
        move.clip = teste;
        move.Play();
    }

    public void Click()
    {
        AudioClip teste = selectionSound;
        selection.clip = teste;
        selection.Play();
    }

    public void TensionSoundStart()
    {
        AudioClip teste = tension;
        levelAmbiance.clip = teste;
        levelAmbiance.volume = 0.7f;
        levelAmbiance.Play();
    }

    public float GetVolume()
    {
        return AudioListener.volume;
    }

    public void ControlaVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void TensionSoundFinish()
    {
        Invoke("Start", 1f);
    }
}
