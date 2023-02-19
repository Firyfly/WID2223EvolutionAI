using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RobotGeneralControllerLinear : MonoBehaviour
{
    public WindowGraph windowGraph;
    public WindowGraph windowGraph2;

    public TMPro.TMP_InputField numberRobotsField;
    public TMPro.TMP_InputField initialMinSpeedField;
    public TMPro.TMP_InputField initialMaxSpeedField;
    public TMPro.TMP_InputField initialMinPackagesField;
    public TMPro.TMP_InputField initialMaxPackagesField;
    public TMPro.TMP_InputField cycleTimeField;
    public TMPro.TMP_InputField growthRateSpeedField;
    public TMPro.TMP_InputField growthRatePackagesField;

    public TMPro.TMP_Text generationTextObject;
    public TMPro.TMP_Text cycleTimeLeftObject;

    public float cycleTime = float.NaN;
    public float innerCycleTime = 5.0f;
    public int innerCycleNumber = 0;

    public float growthRateSpeed;
    public float growthRatePackages;
    public int generationCount = 1;

    public float distance = 20;

    public int numberRobots = 100;
    public int numberArguments = 2; // id, delivered

    //public float savedArg1Min;
    //public float savedArg1Max;

    public List<int> fitnessList;
    public List<int> ValueList = new List<int>();

    public float initialMinSpeed;
    public float initialMaxSpeed;
    public float newMinSpeed = 0.0f;
    public float newMaxSpeed = 0.0f;

    public float initialMinPackages;
    public float initialMaxPackages;
    public float newMinPackages = 0.0f;
    public float newMaxPackages = 0.0f;

    public int numberIDs = 0;
    public int spawnPosition = 0;

    public bool isInitialized = false;
    private bool dontChangeState = false;
    private bool waitOnce = true;

    public GameObject RobotPrefabLinear;

    private State oldState = State.Null;

    public enum State
    {
        Null,
        Initialization,
        Running,
        EndingRunSent,
        EndingRunCalculate,
        Ending,
        Generate,
        
    }

    public State stateOfSimulation = State.Initialization;

    // Start is called before the first frame update
    void Start()
    {



        //windowGraph = GameObject.Find("WindowGraph").GetComponent<WindowGraph>();
        //windowGraph.GetComponentInParent<GameObject>();//.SetActive(false);
        //windowGraph2 = GameObject.Find("WindowGraph2").GetComponent<WindowGraph>();

    }

    // Update is called once per frame
    void Update()
    {

        generationTextObject.text = generationCount.ToString();

        if(!float.IsNaN(cycleTime))
        {

            cycleTimeLeftObject.text = cycleTime.ToString();

            if(cycleTime >= 0)
            {
                cycleTime -= Time.deltaTime;
            }
            else
            {

                if(innerCycleTime >= 0)
                {
                    innerCycleTime -= Time.deltaTime;
                }
                else{
                    innerCycleTime = 5.0f;
                    innerCycleNumber += 1;
                }

                switch (innerCycleNumber)
                {
                    case 1:
                        stateOfSimulation = State.EndingRunSent;
                        break;

                    case 2:
                        stateOfSimulation = State.EndingRunCalculate;
                        break;

                    case 3:
                        stateOfSimulation = State.Generate;
                        break;

                    case 4:
                        stateOfSimulation = State.Running;

                        if (cycleTimeField.text != string.Empty)
                        {
                            cycleTime = float.Parse(cycleTimeField.text);
                        }

                        else { cycleTime = 50; }
                        innerCycleNumber = 0;
                        break;
                }


            }

        }



        if (oldState != stateOfSimulation)
        {

            switch (stateOfSimulation)
            {
                case State.Initialization:

                    //TODO IF Empty String

                    numberRobots = 10;
                    initialMinSpeed = 1;
                    initialMaxSpeed = 5;
                    initialMinPackages = 1;
                    initialMaxPackages = 2;
                    growthRateSpeed = 5;
                    growthRatePackages = 1;
                    generationCount = 1;

                    if (numberRobotsField.text != string.Empty)
                    {
                        numberRobots = int.Parse(numberRobotsField.text);
                    }
                    if (initialMinSpeedField.text != string.Empty)
                    {
                        initialMinSpeed = float.Parse(initialMinSpeedField.text);
                    }
                    if (initialMaxSpeedField.text != string.Empty)
                    {
                        initialMaxSpeed = float.Parse(initialMaxSpeedField.text);
                    }
                    if (initialMinPackagesField.text != string.Empty)
                    {
                        initialMinPackages = float.Parse(initialMinPackagesField.text);
                    }
                    if (initialMaxPackagesField.text != string.Empty)
                    {
                        initialMaxPackages = float.Parse(initialMaxPackagesField.text);
                    }
                    if (growthRateSpeedField.text != string.Empty)
                    {
                        growthRateSpeed = float.Parse(growthRateSpeedField.text);
                    }
                    if (growthRatePackagesField.text != string.Empty)
                    {
                        growthRatePackages = float.Parse(growthRatePackagesField.text);
                    }



                    if (numberRobots % 2 != 0)
                    {
                        numberRobots += 1;
                    }

                    for (int i = 0; i < numberRobots; i++)
                    {
                        Instantiate(RobotPrefabLinear, new Vector3(0 + (i * 2), 0.5f, -10), new Quaternion(0, 0, 0, 0));
                    }

                    isInitialized = true;

                    

                    break;


                case State.EndingRunSent:
                    
                    if (fitnessList.Count >= numberRobots * numberArguments)
                    {
                        int halfNumberRobots;
                        if (numberRobots % 2 == 0) { halfNumberRobots = numberRobots / 2; }
                        else { halfNumberRobots = numberRobots / 2; halfNumberRobots += 1; }

                        //Because only half can survive
                        for (int i = 0; i <= halfNumberRobots; i++)
                        {
                            int indexHighest = 0;
                            int fitnesshighest = 0;
                            int deleteIndex = 0;

                            //Iterates through the whole fitness list and saves highes values
                            for (int k = 0; k < fitnessList.Count; k++)
                            {
              
                                if (fitnessList[k] >= fitnesshighest)
                                {
                                    fitnesshighest = fitnessList[k];
                                    indexHighest = fitnessList[k + 1];
                                    deleteIndex = k;

                                }

                                //Increases k by the amount of arguments, -1 because we already have ++ in the for loop
                                k += numberArguments-1;

                            }

                            fitnessList[deleteIndex] = 0;     //Setting the score to 0 so
                            ValueList.Add(fitnesshighest);
                            ValueList.Add(indexHighest);
                        }
                    }
                    else
                    {
                        dontChangeState = true;
                    }

                    newMinSpeed = 0.0f;
                    newMaxSpeed = 0.0f;
                    newMinPackages = 0.0f;
                    newMaxPackages = 0.0f;


                    break;

                case State.EndingRunCalculate:

                    //if (waitOnce)
                    //{
                    //    waitOnce = false;
                    //    dontChangeState = true;
                    //}
                    //else {
                    
                          
                    
                    
                    
                    //}

                  
                    break;


                case State.Generate:
                    spawnPosition = 0;
                    for (int i = 0; i < numberRobots - (ValueList.Count/numberArguments); i++)
                    {
                        Instantiate(RobotPrefabLinear, new Vector3(0 + ((spawnPosition) * 2), 0.5f, -10), new Quaternion(0, 0, 0, 0));
                        spawnPosition += 1;
                    }

                    ValueList.Clear();
                    fitnessList.Clear();
                    windowGraph.valueList.Add(Mathf.RoundToInt(newMaxSpeed));
                    windowGraph2.valueList.Add(Mathf.RoundToInt(newMaxSpeed));

                    generationCount += 1;

                    break;
            }

            if (dontChangeState == false)
            {
                oldState = stateOfSimulation;
            }
            else
            {
                dontChangeState = false;
            }
        }
    }


    public void StateSetInitialized()
    {
        stateOfSimulation = State.Initialization;
    }

    public void StateSetStart()
    {
        stateOfSimulation = State.Running;
        if (cycleTimeField.text != string.Empty)
        {
            cycleTime = float.Parse(cycleTimeField.text);
        }
        else { cycleTime = 50; }
    }

    public void StateSetEnd()
    {
        stateOfSimulation = State.Ending;
    }

}




