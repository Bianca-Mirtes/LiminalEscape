using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMesh; 

    private float velocityWalking = 3f, velocityPersecution = 5f;
    private float distanceFollow = 20f, distancePerception = 30f, distanceAttack = 2f;

    private float timeForAttack = 1.5f;
    private float distanceForPlayer, distanceForAIPoint;

    private bool seeingPlayer;
    public Transform[] destinyRandow;
    private int AIPointCurrent;

    private bool followSomething, attackSomething, teste;
    private float countPersecution=0, countAttack=0;
    private bool playerFrontForEnemy, playerBack;

    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        navMesh = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        AIPointCurrent = Random.Range(0, destinyRandow.Length);
    }

    // Update is called once per frame
    void Update()
    {
        distanceForPlayer = Vector3.Distance(player.transform.position, transform.position);
        distanceForAIPoint = Vector3.Distance(destinyRandow[AIPointCurrent].transform.position, transform.position);

        RaycastHit hit;
        Vector3 from = transform.position;
        Vector3 to = player.position;
        Vector3 direction = to - from;
        if(Physics.Raycast(transform.position, direction, out hit, 1000) && distanceForPlayer < distancePerception) // para ver se o player esta no raio de percepçao do inimigo
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                seeingPlayer = true;
            }
            else
            {
                seeingPlayer = false;
            }
        }
        if(distanceForPlayer > distancePerception)
        {
            Walking();
        }
        if(distanceForPlayer <= distancePerception && distanceForPlayer > distanceFollow) // para verificar se o inimigo esta vendo o player
        {
            if (seeingPlayer)
            {
                See();
            }
            else{
                Walking();
            }
        }
        if(distanceForPlayer <= distanceFollow && distanceForPlayer > distanceAttack) // para verificar se o inimigo pode seguir o player
        {
            if (seeingPlayer)
            {
                Follow();
                followSomething = true;
            }
            else
            {
                Walking();
            }
        }

        if(distanceForPlayer <= distanceAttack) // para verificar se o inimigo pode atacar o player
        {
            Attack();
        }

        if(distanceForAIPoint <= 2f) // para mudar o destino aleatorio do inimigo
        {
            AIPointCurrent = Random.Range(0, destinyRandow.Length);
            Walking();
        }

        if (teste/*followSomething*/)
        {
            ani.SetBool("isRunning", true);
            countPersecution += Time.deltaTime;
        }
        if(countPersecution >= 5f && seeingPlayer == false)
        {
            teste = false;
            followSomething = false;
            countPersecution = 0f;
        }

        if (attackSomething)
        {
            ani.SetBool("isAtacking", true);
            countAttack += Time.deltaTime;
        }
        if(countAttack >= timeForAttack && distanceForPlayer <= distanceAttack)
        {
            attackSomething = true;
            countAttack = 0f;
            player.GetComponent<Animator>().SetBool("isDead", true);
            SceneManager.LoadScene(1); // derrota
        }
        else if (countAttack >= timeForAttack && distanceForPlayer > distanceAttack) {
            ani.SetBool("isAtacking", false);
            attackSomething = false;
            countAttack = 0f;        
        }

    }

    void Walking()
    {
        if (!followSomething)
        {
            ani.SetBool("isRunning", false);
            navMesh.acceleration = 5f;
            navMesh.speed = velocityWalking;
            navMesh.destination = destinyRandow[AIPointCurrent].transform.position;
        }
        else
        {
            teste = true;
        }
    }

    void See()
    {
        ani.SetBool("isIdle", true);
        navMesh.speed = 0f;
        transform.LookAt(player);

    }

    void Follow()
    {
        ani.SetBool("isRunning", true);
        navMesh.acceleration = 8f;
        navMesh.speed = velocityPersecution;
        navMesh.destination = player.position;
    }

    void Attack()
    {
        attackSomething = true;
    }
}
