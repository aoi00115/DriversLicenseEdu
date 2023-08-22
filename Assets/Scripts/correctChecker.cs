using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class correctChecker : MonoBehaviour
{
    public static int ScoreCounter = 0;
    public bool CorrectChecker = false;
    public aniManager AniManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            AniManager.Ani_A();
            if (CorrectChecker == true)
            {
                ScoreCounter++;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            AniManager.Ani_B();
            if (CorrectChecker == false)
            {
                ScoreCounter++;
            }
        }

    }
    
}
