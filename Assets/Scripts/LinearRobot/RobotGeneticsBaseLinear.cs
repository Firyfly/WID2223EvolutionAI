using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGeneticsBaseLinear : MonoBehaviour
{

    private GameObject robotGeneralControllerLinearObject;
    private RobotGeneralControllerLinear robotGeneralControllerLinear;

    public int ROBOT_PACKAGES;
    public float ORIGINAL_SPEED;
    public float ROBOT_SPEED;
    public float DISTANCE;

    public int DELIVERED;

    public int id;

    public bool oldGeneration = false;
    public bool alreadyDead = false;

    public float dropChance;

    public RobotGeneralControllerLinear.State oldState = RobotGeneralControllerLinear.State.Null;
    


    // Start is called before the first frame update
    void Start()
    {
        robotGeneralControllerLinearObject = GameObject.Find("RobotGeneralControllerLinear");
        robotGeneralControllerLinear = robotGeneralControllerLinearObject.GetComponent<RobotGeneralControllerLinear>();

        id = robotGeneralControllerLinear.numberIDs;
        robotGeneralControllerLinear.numberIDs += 1;


    }

    // Update is called once per frame
    void Update()
    {
        if (oldState != robotGeneralControllerLinear.stateOfSimulation)
        {
            switch (robotGeneralControllerLinear.stateOfSimulation)
            {
                case RobotGeneralControllerLinear.State.Initialization:
                    InitializationGenes();
                    break;

                case RobotGeneralControllerLinear.State.Running:
                    Running();
                    break;

                case RobotGeneralControllerLinear.State.EndingRunSent:
                    EndingRunSent();
                    break;
                case RobotGeneralControllerLinear.State.EndingRunCalculate:
                    EndingRunCalculate();
                    break;

                case RobotGeneralControllerLinear.State.Ending:
                    Ending();
                    break;

                case RobotGeneralControllerLinear.State.Generate:
                    GenerationGenes();
                    break;


            }

            oldState = robotGeneralControllerLinear.stateOfSimulation;
        }




    }


    public void InitializationGenes()   //For first robots initialization
    {

        ROBOT_PACKAGES = (int)Random.Range(robotGeneralControllerLinear.initialMinPackages, robotGeneralControllerLinear.initialMaxPackages);
        ROBOT_SPEED = Random.Range(robotGeneralControllerLinear.initialMinSpeed, robotGeneralControllerLinear.initialMaxSpeed);
        ORIGINAL_SPEED = ROBOT_SPEED;
        float multiplySpeed = 0;
        for (int i = 1; i <= ROBOT_PACKAGES; i++) {
            if(i == 1) { continue; }
            multiplySpeed += Mathf.Pow(0.5f,i);
        }
    
        ROBOT_SPEED = (ROBOT_SPEED / ROBOT_PACKAGES) + multiplySpeed;

        DISTANCE = robotGeneralControllerLinear.distance;

        dropChance = ROBOT_PACKAGES * 2;

        Color speedColor = new Color(0.5f,0.5f,0.5f);
        this.GetComponent<Renderer>().material.SetColor("_Color", speedColor);
       
    }
    public void GenerationGenes()   //For generation of future generations genes
    {
        if (!oldGeneration)
        {
            ROBOT_PACKAGES = (int)Random.Range(robotGeneralControllerLinear.newMinPackages - robotGeneralControllerLinear.growthRatePackages, robotGeneralControllerLinear.newMaxPackages + robotGeneralControllerLinear.growthRatePackages);
            ROBOT_SPEED = Random.Range(robotGeneralControllerLinear.newMinSpeed-robotGeneralControllerLinear.growthRateSpeed, robotGeneralControllerLinear.newMaxSpeed+ robotGeneralControllerLinear.growthRateSpeed);
            ORIGINAL_SPEED = ROBOT_SPEED;

            if(ROBOT_PACKAGES <= 0)
            {
                ROBOT_PACKAGES = 1;
            }

            float multiplySpeed = 0;
            for (int i = 1; i <= ROBOT_PACKAGES; i++)
            {
                if (i == 1) { continue; }
                multiplySpeed += Mathf.Pow(0.5f, i);
            }
           
            ROBOT_SPEED = (ROBOT_SPEED / ROBOT_PACKAGES) + multiplySpeed;

            DISTANCE = robotGeneralControllerLinear.distance;

            float speedDifference = (ORIGINAL_SPEED - robotGeneralControllerLinear.initialMaxSpeed);
            speedDifference = speedDifference / 100;
            //if(Mathf.Abs(speedDifference) > 0 && Mathf.Abs(speedDifference) < 10)
            //{
            //    speedDifference = speedDifference / 10;
            //}
            //else if (Mathf.Abs(speedDifference) > 10 && Mathf.Abs(speedDifference) < 100)
            //{
            //    speedDifference = speedDifference / 100;
            //}

            dropChance = ROBOT_PACKAGES * 2;


            Color speedColor = new Color(0.5f, 0.5f, 0.5f + speedDifference);
            this.GetComponent<Renderer>().material.SetColor("_Color", speedColor);

            if(ROBOT_SPEED <= 0)
            {
                ROBOT_SPEED = 0.5f;
            }
           
        }
        else
        {
            this.transform.position = new Vector3(0 + ((robotGeneralControllerLinear.spawnPosition) * 2), 0.5f, -10);
            robotGeneralControllerLinear.spawnPosition += 1;
        }

    }

    public void Running()
    {

    }

    public void EndingRunSent()
    {
        robotGeneralControllerLinear.fitnessList.Add(DELIVERED);
        robotGeneralControllerLinear.fitnessList.Add(id);
    }

    public void EndingRunCalculate()
    {
        bool isInGeneration = false;

        //int halfNumberRobots;
        //if (robotGeneralControllerLinear.numberRobots % 2 == 0) { halfNumberRobots = robotGeneralControllerLinear.numberRobots / 2; }
        //else { halfNumberRobots = robotGeneralControllerLinear.numberRobots / 2; halfNumberRobots += 1; }
        if (!alreadyDead)
        {
            for (int i = 1; i <= robotGeneralControllerLinear.ValueList.Count; i++)
            {
                if (robotGeneralControllerLinear.ValueList[i] == id)
                {
                    isInGeneration = true;
                }
                i++;
            }

            if (isInGeneration)
            {

                //Speed
                if (robotGeneralControllerLinear.newMinSpeed == 0.0f && robotGeneralControllerLinear.newMaxSpeed == 0.0f)
                {
                    robotGeneralControllerLinear.newMinSpeed = ORIGINAL_SPEED;
                    robotGeneralControllerLinear.newMaxSpeed = ORIGINAL_SPEED;
                }
                else
                {
                    if (robotGeneralControllerLinear.newMinSpeed > ORIGINAL_SPEED)
                    {
                        robotGeneralControllerLinear.newMinSpeed = ORIGINAL_SPEED;
                    }
                    if (robotGeneralControllerLinear.newMaxSpeed < ORIGINAL_SPEED)
                    {
                        robotGeneralControllerLinear.newMaxSpeed = ORIGINAL_SPEED;
                    }
                }

                //Packages
                if (robotGeneralControllerLinear.newMinPackages == 0.0f && robotGeneralControllerLinear.newMaxPackages == 0.0f)
                {
                    robotGeneralControllerLinear.newMinPackages = ROBOT_PACKAGES;
                    robotGeneralControllerLinear.newMaxPackages = ROBOT_PACKAGES;
                }
                else
                {
                    if (robotGeneralControllerLinear.newMinPackages > ROBOT_PACKAGES)
                    {
                        robotGeneralControllerLinear.newMinPackages = ROBOT_PACKAGES;
                    }
                    if (robotGeneralControllerLinear.newMaxPackages < ROBOT_PACKAGES)
                    {
                        robotGeneralControllerLinear.newMaxPackages = ROBOT_PACKAGES;
                    }
                }

                oldGeneration = true;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
                DELIVERED = 0;

            }
            else
            {
                alreadyDead = true;
                this.transform.position = new Vector3(this.transform.position.x - robotGeneralControllerLinear.numberRobots*2- robotGeneralControllerLinear.numberRobots, this.transform.position.y, -10);
            }
        }
        else if (alreadyDead)
        {
            this.transform.position = new Vector3(this.transform.position.x - robotGeneralControllerLinear.numberRobots*2- robotGeneralControllerLinear.numberRobots, this.transform.position.y, this.transform.position.z);
        }


    }


    public void Ending()
    {

    }

}
