
using System;
using System.Threading;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Sttings")]
    public float speed = 0.0f;
    public float timer = 0.0f;

    [Header("Reference")]
    public GameObject pivotPoint;
    public GameObject car;

    // Start is called before the first frame update
    void Start()
    {
        pivotPoint = GameObject.Find("Car/PivotPoint");
        car = GameObject.Find("Car");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        CarAcceleration();
    }

    void CarAcceleration()
    {
        if(speed < 3.0f)
        {
            speed *= 1.0001f;
            car.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
        else
        {
            speed = 3.0f;
            car.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
        
    }
}
