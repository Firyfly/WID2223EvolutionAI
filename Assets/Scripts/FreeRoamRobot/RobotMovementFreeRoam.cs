using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementFreeRoam : MonoBehaviour
{

    private SphereCollider sphereCollider;

    public float robotRange = 20;
    public float robotSpeed = 5;

    public float savedDistanceRessource;
    public Vector3 positionClosestRessource;

    public float savedDistanceRobot;
    public Vector3 positionClosestRobot;

    private Vector3 randomDirection;
    private float randomTimer = 0.0f;

    public FreeRoamGeneralController freeRoamGeneralController;
    private RobotGeneticsBaseFreeRoam robotGeneticsBaseFreeRoam;

    private BoxCollider dummyBoxCollider;


    private GameObject testStillExistObjectRessource;
    private GameObject testStillExistObjectRobot;


    // Start is called before the first frame update
    void Start()
    {
        freeRoamGeneralController = GameObject.Find("FreeRoamGeneralController").GetComponent<FreeRoamGeneralController>();
        robotGeneticsBaseFreeRoam = this.gameObject.GetComponent<RobotGeneticsBaseFreeRoam>();

        sphereCollider = this.gameObject.GetComponent<SphereCollider>();
        sphereCollider.radius = robotRange;

        savedDistanceRessource = robotRange;
        savedDistanceRobot = robotRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (!testStillExistObjectRessource)
        {
            positionClosestRessource = Vector3.zero;
        }
        if (!testStillExistObjectRobot)
        {
            positionClosestRobot = Vector3.zero;
        }

        savedDistanceRessource = robotRange;
        //positionClosestRobot = Vector3.zero;
        savedDistanceRobot = robotRange;

        if (robotGeneticsBaseFreeRoam.readyToMove == true && freeRoamGeneralController.currentGameState == FreeRoamGeneralController.GameState.Running)
        {
            switch (robotGeneticsBaseFreeRoam.collectGoalMax - robotGeneticsBaseFreeRoam.collectGoalCount)
            {
                case 3:
                case 2:
                case 1:
                    if (savedDistanceRessource >= savedDistanceRobot)
                    {
                        MoveToRobot();
                    
                    }
                    else
                    {
                        MoveToRessource();
                       
                    }
                    break;

                case 0:
                    MoveToRobot();
                    break;

                default:
                    MoveToRessource();
                    break;
            }
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ressource")
        {
            
            if (savedDistanceRessource >= Vector3.Distance(other.transform.position, this.gameObject.transform.position))
            {
                
                savedDistanceRessource = Vector3.Distance(other.transform.position, this.gameObject.transform.position);
                positionClosestRessource = other.transform.position;

                testStillExistObjectRessource = other.gameObject;
                
            }

        }

        if(other.tag == "Robot")
        {

            if (savedDistanceRobot >= Vector3.Distance(other.transform.position, this.gameObject.transform.position))
            {
                
                savedDistanceRobot = Vector3.Distance(other.transform.position, this.gameObject.transform.position);
                positionClosestRobot = other.transform.position;

                testStillExistObjectRobot = other.gameObject;



            }


        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other is BoxCollider && (robotGeneticsBaseFreeRoam.collectGoalMax - robotGeneticsBaseFreeRoam.collectGoalCount) < 3 && other.tag == "Robot")
        {
            robotGeneticsBaseFreeRoam.EvolutionGenerateGenetics(other.gameObject);
        }
    }



    private void MoveToRessource()
    {

        if (positionClosestRessource != Vector3.zero)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, positionClosestRessource, Time.deltaTime * robotSpeed);
        }
        else
        {
            if (randomTimer <= 0.0f)
            {
                randomDirection = new Vector3(Random.Range(-1.0f,1.0f), 0, Random.Range(-1.0f, 1.0f));
                randomTimer = 5.0f;
            }
            else
            {
                this.gameObject.transform.Translate(randomDirection * (Time.deltaTime * robotSpeed));
                randomTimer -= 0.003f;
            }
        }
    }

    private void MoveToRobot()
    {
        Debug.Log(positionClosestRobot);
        if (positionClosestRobot != Vector3.zero)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, positionClosestRobot, Time.deltaTime * robotSpeed);
            //positionClosestRobot = Vector3.zero;
        }
        else
        {
            if (randomTimer <= 0.0f)
            {
                randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));   
                randomTimer = 5.0f;
            }
            else
            {  
                this.gameObject.transform.Translate(randomDirection * (Time.deltaTime * robotSpeed));
                randomTimer -= 0.003f;
            }


        }

    }

}
