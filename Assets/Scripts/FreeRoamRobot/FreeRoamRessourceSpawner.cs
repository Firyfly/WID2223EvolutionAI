using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoamRessourceSpawner : MonoBehaviour
{

    public GameObject ressourceObject;
    public int ressourceCount = 0;
    public int ressourceCountMax = 10;
    private float ressourceSpawnTimer = 3.0f;

    FreeRoamGeneralController freeRoamGeneralController;

    // Start is called before the first frame update
    void Start()
    {
        freeRoamGeneralController = GameObject.Find("FreeRoamGeneralController").GetComponent<FreeRoamGeneralController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(freeRoamGeneralController.currentGameState == FreeRoamGeneralController.GameState.Running)
        {
            spawnRessource();
        }

    }


    public void spawnRessource()
    {
        if(ressourceSpawnTimer <= 0.0f && ressourceCount < ressourceCountMax)
        {
            Instantiate(ressourceObject, new Vector3(Random.Range(-50.0f, 50.0f), 0.5f, Random.Range(-50.0f, 50.0f)), new Quaternion(0,0,0,0));
            ressourceSpawnTimer = 3.0f;
            ressourceCount += 1;
        }
        else
        {
            ressourceSpawnTimer -= Time.deltaTime;
        }
    }

}
