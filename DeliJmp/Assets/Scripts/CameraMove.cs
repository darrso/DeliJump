using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform DeliTransform;
    const int resolutionX = 9;
    const int resolutionY = 16;
    void Start()
    {
        float screenRatio = Screen.width * 1f / Screen.height;
        float bestRatio = resolutionX * 1f / resolutionY;
        if (screenRatio <= bestRatio)
        {
            GetComponent<Camera>().rect = new Rect(0, (1f - screenRatio / bestRatio) / 2f, 1, screenRatio / bestRatio);
        }
        else if (screenRatio > bestRatio)
        {
            GetComponent<Camera>().rect = new Rect((1f - bestRatio / screenRatio) / 2f, 0, bestRatio / screenRatio, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(DeliTransform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, DeliTransform.position.y, transform.position.z);
        }

    }
}
