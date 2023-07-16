using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public AudioClip opening, closing;
    private bool open;
    private Animator door;

    void Start()
    {
        open = false;
        door = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
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

        if (Input.GetKeyDown(KeyCode.O) && open)
        {
            GetComponent<AudioSource>().PlayOneShot(opening);
        }
        if (Input.GetKeyDown(KeyCode.O) && !open)
        {
            GetComponent<AudioSource>().PlayOneShot(closing);
        }
    }
}
