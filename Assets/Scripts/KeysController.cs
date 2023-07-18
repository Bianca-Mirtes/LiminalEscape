using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysController : MonoBehaviour
{
    private bool canCollect;
    public Transform player;
    public AudioClip collectKey;
    private float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= 2f)
        {
            canCollect = true;
        }
        else if (distanceToPlayer > 2f)
        {
            canCollect = false;
        }
        if(Input.GetKey(KeyCode.K) && canCollect)
        {
            GetComponent<AudioSource>().PlayOneShot(collectKey);
            Invoke("DestroyKey", 1f);
        }

    }
    void DestroyKey()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, 2f);
    }
}

