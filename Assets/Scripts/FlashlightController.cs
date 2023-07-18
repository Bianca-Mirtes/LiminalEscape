using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public AudioClip ligado, Off;
    private bool On;
    private Light Luz;
    private HUDController HUD;

    void Start()
    {
        On = true;
        Luz = GetComponent<Light>();
        HUD = FindObjectOfType<HUDController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            On = !On;
            HUD.lanternaLigaDesliga();
        }
        if (On == true)
        {
            Luz.intensity = 30;
        }
        else
        {
            Luz.intensity = 0;
        }

        if (Input.GetKeyDown(KeyCode.F) && On == false)
        {
            GetComponent<AudioSource>().PlayOneShot(ligado);
        }
        if (Input.GetKeyDown(KeyCode.F) && On == true)
        {
            GetComponent<AudioSource>().PlayOneShot(Off);
        }
    }
}
