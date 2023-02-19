using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementControllerLinear : MonoBehaviour
{

    private RobotGeneticsBaseLinear robotGeneticsBaseLinear;
    private RobotGeneralControllerLinear robotGeneralControllerLinear;
    private GameObject robotGeneralControllerLinearObject;
    private bool forward = true;

    private float startPositionZ;

    private float dropTimer = 5.0f;
    private float hasDroppedTimer = -1.0f;
    private bool hasDropped = false;

    // Start is called before the first frame update
    void Start()
    {
        robotGeneralControllerLinearObject = GameObject.Find("RobotGeneralControllerLinear");
        robotGeneticsBaseLinear = this.gameObject.GetComponent<RobotGeneticsBaseLinear>();
        robotGeneralControllerLinear = robotGeneralControllerLinearObject.GetComponent<RobotGeneralControllerLinear>();

        startPositionZ = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

        if (dropTimer >= 0 && hasDroppedTimer <= 0)
        {
            dropTimer -= Time.deltaTime;


            switch (robotGeneralControllerLinear.stateOfSimulation)
            {
                case RobotGeneralControllerLinear.State.Running:
                    if (!robotGeneticsBaseLinear.alreadyDead)
                    {
                        if (forward)
                        {
                            this.transform.Translate(new Vector3(0, 0, 1 * robotGeneticsBaseLinear.ROBOT_SPEED * Time.deltaTime));
                            if (this.transform.position.z >= startPositionZ + robotGeneticsBaseLinear.DISTANCE) { forward = false; }
                        }
                        else
                        {
                            this.transform.Translate(new Vector3(0, 0, -1 * robotGeneticsBaseLinear.ROBOT_SPEED * Time.deltaTime));
                            if (this.transform.position.z <= startPositionZ)
                            {
                                forward = true;
                                robotGeneticsBaseLinear.DELIVERED += 1 * robotGeneticsBaseLinear.ROBOT_PACKAGES;
                            }
                        }
                    }
                    break;

                case RobotGeneralControllerLinear.State.Ending:

                    break;
            }

        }
        else
        {

            if(hasDropped == false)
            {
                int randomVal = Mathf.RoundToInt(Random.Range(0, 100));

                float dropChance = robotGeneticsBaseLinear.dropChance;

                for(int i = 0; i < robotGeneticsBaseLinear.ROBOT_PACKAGES; i++)
                {
                    int sizeOfPackage = Mathf.RoundToInt(Random.Range(1, 5));   //3 weight categories for a package
                    dropChance += sizeOfPackage; 
                }

                Debug.Log("-----");
                Debug.Log(randomVal);
                Debug.Log(dropChance);
                if (randomVal <= dropChance)
                {
                    Debug.Log("Dropped");
                    hasDropped = true;
                    hasDroppedTimer = 3;
                }
                else
                {
                    dropTimer = 5;
                }

            }
            else
            {

                if (hasDroppedTimer >= 0)
                {
                    hasDroppedTimer -= Time.deltaTime;
                }
                else
                {
                    hasDropped = false;
                    dropTimer = 5;
                }


            }




        }

    }
}
    