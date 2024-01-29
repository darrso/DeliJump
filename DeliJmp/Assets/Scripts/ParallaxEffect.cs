using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startpos;
    public GameObject camera;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;

    }

    // Update is called once per frame
    void Update()
    {
        float tmp = (camera.transform.position.y * (1 - parallaxEffect));
        float dist = (camera.transform.position.y * parallaxEffect);
        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

        if(tmp > startpos + length)
        {
            startpos += length;
        }
        else if(tmp < startpos - length) {
            startpos -= length;
        }
    }
}
