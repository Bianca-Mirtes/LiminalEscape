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
    private float distancePlayerToTool;
    private bool canCollect;


    // Start is called before the first frame update
    void Start()
    {
        canCollect = true;
        for (int ii=0; ii < qntTools; ii++)
        {
            int posRandom = Random.Range(0, spawnPoints.Count);
            Instantiate(tools[Random.Range(0, tools.Length)], spawnPoints[posRandom].transform.position, Quaternion.identity, toolsSlot.transform);
            spawnPoints.Remove(spawnPoints[posRandom]);
        }
    }

    public GameObject VerifTools()
    {
        for (int ii = 0; ii < toolsSlot.transform.childCount; ii++)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            distancePlayerToTool = Vector3.Distance(player.transform.position, toolsSlot.transform.GetChild(ii).position);
            if(distancePlayerToTool <= 2.5f && canCollect)
            {
                canCollect = false;
                return toolsSlot.transform.GetChild(ii).gameObject;
            }
        }
        return null;
    }

    public void SetcanCollect(bool value)
    {
        canCollect = value;
    }
}
