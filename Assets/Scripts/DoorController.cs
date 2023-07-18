using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    public AudioClip opening, closing;
    private bool open;
    private Animator door;
    public GameObject player;
    FirstPersonController FPC;
    public GameObject porta1;
    public GameObject porta2;
    public GameObject saida;

    public InputAction map;

    void Start()
    {
        open = false;
        door = GetComponent<Animator>();
        FPC = player.GetComponent<FirstPersonController>();
        //_input
    }

    void Update()
    {
        if (FPC._input.openDoor)
        {
            Debug.Log("apertou");
            open = !open;
        }
        if (open)
        {
            door.SetBool("isOpen", true);
        }
        else
        {
            door.SetBool("isOpen", false);
        }

        if (FPC._input.openDoor && open)
        {
            GetComponent<AudioSource>().PlayOneShot(opening);
        }
        if (FPC._input.openDoor && !open)
        {
            GetComponent<AudioSource>().PlayOneShot(closing);
        }
    }
}
