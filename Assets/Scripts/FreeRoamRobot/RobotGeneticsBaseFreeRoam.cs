using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGeneticsBaseFreeRoam : MonoBehaviour
{
    //public int optionsOfFacility;   //Numbers Of Facility that can be Chosen starting at 2, from 1 - 4
    //private int chosenFacility1;    //The chosen Facilities, Facilities are declared as the facility number
    //private int chosenFacility2;
    //private int chosenFacility3;
    //private int chosenFacility4;
    //public float walkingSpeed;  //Walking speed of robot
    //public float chanceOfPacketLoss;    //chance of loosing a or multiple packets
    //public int carryingCapacity;    //How many packages can be carried, increases chance of loss of packet as well as slowing down speed
    //public bool preferStreet;   //If the robot uses the street or not, street is faster than dirt

    public float robotSpeed;
    public float robotRange;
    public int collectGoalMax;
    public int collectGoalCount = 0;

    public bool readyToMove = false;

    public FreeRoamGeneralController freeRoamGeneralController;

    public GameObject prefabRobot;

    
    private FreeRoamGeneralController.GameState oldState;

    // Start is called before the first frame update
    void Start()
    {
        freeRoamGeneralController = GameObject.Find("FreeRoamGeneralController").GetComponent<FreeRoamGeneralController>();

    }

    // Update is called once per frame
    void Update()
    {

        if(freeRoamGeneralController.currentGameState != oldState)
        {

            switch (freeRoamGeneralController.currentGameState)
            {
                case FreeRoamGeneralController.GameState.Initialization:

                    robotSpeed = Random.Range(freeRoamGeneralController.initialSpeedMin, freeRoamGeneralController.initialSpeedMax);
                    robotRange = Random.Range(freeRoamGeneralController.initalRadiusMin, freeRoamGeneralController.initalRadiusMax);
                    collectGoalMax = freeRoamGeneralController.collectionGoalMax;
                    readyToMove = true;

                    break;


                case FreeRoamGeneralController.GameState.Running:



                    break;


                case FreeRoamGeneralController.GameState.Ending:



                    break;
            }



            oldState = freeRoamGeneralController.currentGameState;
        }




    }








    public void EvolutionGenerateGenetics(GameObject otherParentObject)
    {
        RobotGeneticsBaseFreeRoam otherParentRobotGeneticsBaseFreeRoam = otherParentObject.GetComponent<RobotGeneticsBaseFreeRoam>();
        otherParentRobotGeneticsBaseFreeRoam.collectGoalCount = 0;
        collectGoalCount = 0;

        GameObject instantiateRobot =  Instantiate(prefabRobot);
        RobotGeneticsBaseFreeRoam instantiateRobotGeneticsBaseFreeRoam = instantiateRobot.GetComponent<RobotGeneticsBaseFreeRoam>();

        if(this.robotRange>= otherParentRobotGeneticsBaseFreeRoam.robotRange)
        {
            instantiateRobotGeneticsBaseFreeRoam.robotRange = Random.Range(otherParentRobotGeneticsBaseFreeRoam.robotRange-1, this.robotRange + 2);
        }
        else
        {
            instantiateRobotGeneticsBaseFreeRoam.robotRange = Random.Range(this.robotRange - 1,otherParentRobotGeneticsBaseFreeRoam.robotRange + 2);
        }



        if (this.robotSpeed >= otherParentRobotGeneticsBaseFreeRoam.robotSpeed)
        {
            instantiateRobotGeneticsBaseFreeRoam.robotSpeed = Random.Range(otherParentRobotGeneticsBaseFreeRoam.robotSpeed - 1, this.robotSpeed + 2);
        }
        else
        {
            instantiateRobotGeneticsBaseFreeRoam.robotSpeed = Random.Range(this.robotSpeed - 1, otherParentRobotGeneticsBaseFreeRoam.robotSpeed + 2);
        }


        readyToMove = true;
    }





}
