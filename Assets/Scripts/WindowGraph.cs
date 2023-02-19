using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class WindowGraph : MonoBehaviour
{
    [SerializeField]private Sprite circleSprite;
    private RectTransform graphContainer;

    public List<int> valueList;

    private int oldValueListCount = 0;

    [System.Obsolete]
    private void Awake()
    {
        graphContainer = transform.FindChild("GraphContainer").GetComponent<RectTransform>();

        

        CreateCircle(new Vector2(200,200));
        valueList = new List<int>(); //{ 5, 94, 56, 45, 30, 22, 12, 54, 23, 23, 56, 23, 23, 76, 54, 12, 69 };
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(oldValueListCount == valueList.Count)
        {

        }
        else
        {
            ClearGraph();
            ShowGraph(valueList);
            oldValueListCount = valueList.Count;
        }


    }

    private void ClearGraph()
    {

        Debug.Log(valueList.Count);


        if(graphContainer.Find("circle") != null)
        {
            for(int i = 0; i < valueList.Count; i++)
            {
                Destroy(graphContainer.Find("circle").gameObject);
                Debug.Log(graphContainer.Find("circle"));
            }

        }
        if (graphContainer.Find("DotConnection") != null)
        {
            for (int i = 0; i < valueList.Count-1; i++)
            {
                Destroy(graphContainer.Find("DotConnection").gameObject);
                Debug.Log("DotConnection");
            }

        }

        
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(5, 5);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);


        return gameObject;
    }

    private void ShowGraph(List<int> valueList)
    {

        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize;
        if (valueList.Count == 0)
        {
            xSize = 5.0f;
        }
        else
        {
            xSize = 100 / valueList.Count;
        }


        GameObject lastCircleGameObject = null;
        for(int i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            
            if(lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }


    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 1.5f);
        rectTransform.anchoredPosition = dotPositionA + dir*distance*0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));

    }



}
