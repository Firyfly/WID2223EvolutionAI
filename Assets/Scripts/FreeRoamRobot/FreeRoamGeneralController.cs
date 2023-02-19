using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoamGeneralController : MonoBehaviour
{
    public TMPro.TMP_InputField numberRobotsInputField;
    public TMPro.TMP_InputField initialSpeedMinInputField;
    public TMPro.TMP_InputField initialSpeedMaxInputField;
    public TMPro.TMP_InputField initialRadiusMinInputField;
    public TMPro.TMP_InputField initialRadiusMaxInputField;
    public TMPro.TMP_InputField growthRateSpeedInputField;
    public TMPro.TMP_InputField growthRateRadiusInputField;

    public GameObject RobotObject;

    public float initialSpeedMin = 4.5f;
    public float initialSpeedMax = 5.5f;

    public float initalRadiusMin = 18.0f;
    public float initalRadiusMax = 22.0f;

    public int collectionGoalMax = 10;

    public int numberRobots;

    public float growthRateSpeed;
    public float growthRateRadius;

    public enum GameState
    {
        Empty,
        Initialization,
        Running,
        Ending,
    }

    public GameState currentGameState;
    public GameState oldState;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState != oldState)
        {

            switch (currentGameState)
            {
                case GameState.Initialization:

                    numberRobots = 10;
                    initialSpeedMin = 4.5f;
                    initialSpeedMax = 5.5f;
                    initalRadiusMin = 18.0f;
                    initalRadiusMax = 22.0f;
                    growthRateSpeed = 5.0f;
                    growthRateRadius = 1.0f;

                    if (numberRobotsInputField.text != string.Empty)
                    {
                        numberRobots = int.Parse(numberRobotsInputField.text);
                    }
                    if (initialSpeedMinInputField.text != string.Empty)
                    {
                        initialSpeedMin = float.Parse(initialSpeedMinInputField.text);
                    }
                    if (initialSpeedMaxInputField.text != string.Empty)
                    {
                        initialSpeedMax = float.Parse(initialSpeedMaxInputField.text);
                    }
                    if (initialRadiusMinInputField.text != string.Empty)
                    {
                        initalRadiusMin = float.Parse(initialRadiusMinInputField.text);
                    }
                    if (initialRadiusMaxInputField.text != string.Empty)
                    {
                        initalRadiusMax = float.Parse(initialRadiusMaxInputField.text);
                    }
                    if (growthRateSpeedInputField.text != string.Empty)
                    {
                        growthRateSpeed = float.Parse(growthRateSpeedInputField.text);
                    }
                    if (growthRateRadiusInputField.text != string.Empty)
                    {
                        growthRateRadius = float.Parse(growthRateRadiusInputField.text);
                    }

                    for (int i = 0; i < numberRobots; i++)
                    {
                        Instantiate(RobotObject, new Vector3(0, 0.5f, 0), new Quaternion(0, 0, 0, 0));
                        Debug.Log(i);
                    }

                    break;

                case GameState.Running:

                    break;

                case GameState.Ending:

                    break;
            }

            oldState = currentGameState;
        }
    }

    public void StateSetInitilisation()
    {
        currentGameState = GameState.Initialization;
    }

    public void StateSetRunning()
    {
        currentGameState = GameState.Running;
    }

    public void StateSetEnding()
    {
        currentGameState = GameState.Ending;
    }

}
