using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 0.0f;
    public float timer = 0.0f;

    [Header("Stage Setting")]
    public bool answer;                 // Change this variable in the inspector for every question
    public bool stage1;
    public bool stage2;
    public bool stage3;
    public bool stage4;

    public static int scoreCounter = 0; 
    public bool correctChecker;

    bool isAnswered = false;

    [Header("Reference")]
    public GameObject car;
    public GameObject pivotPoint;
    public PostProcessVolume postProcessingVolume;
    public ChromaticAberration chromaticAberration;

    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.Find("Car");
        pivotPoint = GameObject.Find("Car/PivotPoint");

        postProcessingVolume = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();
        postProcessingVolume.profile.TryGetSettings(out chromaticAberration);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(!isAnswered)
        {
            // Checing if the user input is correct
            if(Input.GetKeyDown(KeyCode.O))
            {
                chromaticAberration.intensity.value = 0;
                correctChecker = true;
                isAnswered = true;
            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                chromaticAberration.intensity.value = 0;
                correctChecker = false;
                isAnswered = true;
            }
        }

        // Debug
        // Debug.Log(speed);
        // Debug.Log(timer);
        // Debug.Log(chromaticAberration.intensity.value);
    }

    void FixedUpdate()
    {
        if(!isAnswered)
        {
            PreAnswerAnimation();
        }
        else
        {
            if(correctChecker == answer)    // Play the correct animation
            {
                CorrectAnimation();       
                scoreCounter++;
            }
            else                            // Play the wrong animation
            {
                WrongAnimation();           
            }
        }
    }

    void PreAnswerAnimation()
    {
        car.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        if(timer < 5)
        {
            if(speed < 3.0f)
            {
                speed *= 1.02f;
            }
            else
            {
                speed = 3.0f;
            }
        }
        else
        {
            speed *= 0.975f;
            if(chromaticAberration.intensity.value <= 2)
            {
                chromaticAberration.intensity.value += 0.01f * 1.005f;
            }
        }        
    }

    void CorrectAnimation()
    {
        if(stage1)
        {
            speed = 3.0f;
            car.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
        if(stage2)
        {
            
        }
        if(stage3)
        {
            
        }
        if(stage4)
        {
            
        }
        Debug.Log("CorrectAnswer");
    }

    void WrongAnimation()
    {
        if(stage1)
        {

        }
        if(stage2)
        {
            
        }
        if(stage3)
        {
            
        }
        if(stage4)
        {
            
        }
        Debug.Log("WrongAnswer");
    }
}
