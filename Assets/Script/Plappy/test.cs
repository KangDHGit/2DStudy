using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World!");

        Debug.Log("Start");

        Debug.Log(Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");

        Debug.Log(Time.time);
    }
}
