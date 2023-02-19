using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearKameraMovement : MonoBehaviour
{

    private Camera camera;

    private GameObject robotGeneralControllerLinearObject;
    private RobotGeneralControllerLinear robotGeneralControllerLinear;

    // Start is called before the first frame update
    void Start()
    {
        camera = this.gameObject.GetComponent<Camera>();

        robotGeneralControllerLinearObject = GameObject.Find("RobotGeneralControllerLinear");
        robotGeneralControllerLinear = robotGeneralControllerLinearObject.GetComponent<RobotGeneralControllerLinear>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MoveCameraLeft()
    {

        robotGeneralControllerLinear.generationCount -= 1;
        camera.gameObject.transform.position = this.transform.position = new Vector3(this.transform.position.x - robotGeneralControllerLinear.numberRobots * 2 - robotGeneralControllerLinear.numberRobots, this.transform.position.y, this.transform.position.z); 
    }

    public void MoveCameraRight()
    {
        robotGeneralControllerLinear.generationCount += 1;
        camera.gameObject.transform.position = this.transform.position = new Vector3(this.transform.position.x + robotGeneralControllerLinear.numberRobots * 2 + robotGeneralControllerLinear.numberRobots, this.transform.position.y, this.transform.position.z);
    }

}
