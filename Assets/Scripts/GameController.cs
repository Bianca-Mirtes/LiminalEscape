using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEditor;
using System.Collections.Specialized;

public class GameController : MonoBehaviour
{
    [Header("Doors")]
    public AudioClip opening, closing, locked;
    private bool open;
    public GameObject door1;
    //public GameObject door2;
    public GameObject saida;
    private float distanceForInteract = 2.5f, distancePlayerDoor1, distancePlayerDoor2;

    [Header("Keys")]
    public AudioClip collectKey;
    private float distancePlayerToKey1, distancePlayerToKey2;
    private bool isKey1;
    private bool isKey2;

    [Header("Tools")]
    public AudioClip brokenTool;
    private int LifeTool = 3;

    [Header("Player")]
    public GameObject player;
    public GameObject key1;
    public GameObject key2;
    private GameObject tool;

    private HUDController HUD;
    private string typeTool;
    private Vector3 positionTool = new Vector3(-30.953f, -179.683f, 94.377f);


    void Start()
    {
        open = false;
        isKey1 = false;
        isKey2 = false;
        HUD = FindObjectOfType<HUDController>();
    }

    private void Doors()
    {
        distancePlayerDoor1 = Vector3.Distance(player.transform.position, door1.transform.position);
        distancePlayerDoor2 = Vector3.Distance(player.transform.position, saida.transform.position);
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (distancePlayerDoor1 <= distanceForInteract)
            {
                if (isKey1)
                {
                    door1.GetComponent<Animator>().SetBool("isLocked", false);
                    open = !open;
                    if (open)
                    {
                        door1.GetComponent<Animator>().SetBool("isOpen", true);
                        door1.GetComponent<AudioSource>().PlayOneShot(opening);
                    }
                    else
                    {
                        door1.GetComponent<Animator>().SetBool("isOpen", false);
                        door1.GetComponent<AudioSource>().PlayOneShot(closing);
                    }
                }
                else
                {
                    door1.GetComponent<Animator>().SetBool("isLocked", true);
                    door1.GetComponent<AudioSource>().PlayOneShot(locked);
                }
            }
            if (distancePlayerDoor2 <= distanceForInteract)
            {
                if (isKey2)
                {
                    
                    saida.GetComponent<Animator>().SetBool("isLocked", false);
                    open = !open;
                    if (open)
                    {
                        saida.GetComponent<Animator>().SetBool("isOpen", true);
                        saida.GetComponent<AudioSource>().PlayOneShot(opening);

                    }
                    else
                    {
                        saida.GetComponent<Animator>().SetBool("isOpen", false);
                        saida.GetComponent<AudioSource>().PlayOneShot(closing);
                    }
                    SceneManager.LoadScene(2); // vitoria
                }
                else
                {
                    SceneManager.LoadScene(1); // derrota
                    saida.GetComponent<Animator>().SetBool("isLocked", true);
                    saida.GetComponent<AudioSource>().PlayOneShot(locked);
                }

            }
            Debug.Log("apertou");
        }
    }

    public void SetTypeTool(string name)
    {
        this.typeTool = name;
    }

    void Update()
    {
        Doors();
        Keys();
        Tools();
    }

    private void Tools()
    {
        if (Input.GetKeyDown(KeyCode.E) && tool == null)
        {
            if(LifeTool == 0)
            {
                // som da ferramenta quebrando (isso � pra mim) kkkk
                Destroy(tool);
                tool = null;
                LifeTool = 3;

            }
            typeTool = FindObjectOfType<SpawnerToolsController>().VerifTools();

            this.tool = GameObject.Find("Tool").transform.GetChild(0).gameObject;
            tool.transform.rotation = Quaternion.Euler(positionTool);

            if (tool != null)
            {
                if (typeTool.Equals("Machado"))
                {
                    // Bota o icone na HUD
                }
                if (typeTool.Equals("PeDeCabra"))
                {
                    // Bota o icone na HUD
                }
                if (typeTool.Equals("Martelo"))
                {
                    // Bota o icone na HUD
                }
                GameObject.FindObjectOfType<SpawnerToolsController>().SetcanCollect(false);
            }
        }
    }

    public void SetLifeTool()
    {
        LifeTool--;
    }


    public void Keys()
    {
        distancePlayerToKey1 = key1 != null ? Vector3.Distance(player.transform.position, key1.transform.position) : 10f;
        distancePlayerToKey2 = key2 != null ? Vector3.Distance(player.transform.position, key2.transform.position) : 10f;
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (distancePlayerToKey1 <= distanceForInteract)
            {
                key1.GetComponent<AudioSource>().PlayOneShot(collectKey);
                isKey1 = true;
                HUD.setChave1();
                Invoke("DestroyKey", 1f);
            }

            if (distancePlayerToKey2 <= distanceForInteract)
            {
                key2.GetComponent<AudioSource>().PlayOneShot(collectKey);
                isKey2 = true;
                HUD.setChave2();
                // bota o icone na HUD (Vai que � tua tafarel!!)
                Invoke("DestroyKey", 1f);
            }
            Debug.Log("apertou key");
        }
    }

    void DestroyKey()
    {
        if (isKey2)
        {
            Destroy(key2);
        }
        if (isKey1)
        {
            Destroy(key1);
        }
    }
}
