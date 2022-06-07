using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 100.0f;
        float length = 1.0f;
        float pingpong = Mathf.PingPong(Time.time - speed, length);
        Vector3 prevPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(prevPos.x, pingpong, prevPos.z);
    }
}
