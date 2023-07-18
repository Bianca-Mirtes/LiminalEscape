using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerToolsController : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public GameObject[] tools;
    public GameObject toolsSlot;

    private int qntTools = 4;


    // Start is called before the first frame update
    void Start()
    {
        for(int ii=0; ii < qntTools; ii++)
        {
            int posRandom = Random.Range(0, spawnPoints.Count);
            Instantiate(tools[Random.Range(0, tools.Length)], spawnPoints[posRandom].transform.position, Quaternion.identity, toolsSlot.transform);
            spawnPoints.Remove(spawnPoints[posRandom]);
        }
    }
}
