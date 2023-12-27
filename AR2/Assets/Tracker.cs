using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    private Vector3 previousPosition;
    public int ct = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the previousPosition variable with the current position at the start
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(ct==0)
        {
            previousPosition = transform.position;
        }
        ct++;
        if (ct > 2000)
        {
            Vector3 currentPosition = transform.position;

            if (currentPosition.y > previousPosition.y)
                Debug.Log("Moving Up");
            if (currentPosition.y < previousPosition.y)
                Debug.Log("Moving Down");

            previousPosition = currentPosition;

            ct = 0; 
        }
    }

}
