using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{

    private BoxCollider dummyBoxCollider;
    private FreeRoamRessourceSpawner freeRoamRessourceSpawner;

    // Start is called before the first frame update
    void Start()
    {
        dummyBoxCollider = this.gameObject.GetComponent<BoxCollider>();
        freeRoamRessourceSpawner = GameObject.Find("FreeRoamRessourceSpawner").GetComponent<FreeRoamRessourceSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetType() == dummyBoxCollider.GetType())
        {
            other.GetComponent<RobotGeneticsBaseFreeRoam>().collectGoalCount += 1;
            freeRoamRessourceSpawner.ressourceCount -= 1;
            Destroy(this.gameObject);
        }
    }

}
