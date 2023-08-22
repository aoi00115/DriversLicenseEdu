// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CorrectChecker : MonoBehaviour
// {
//     [Header("Stage Setting")]
//     public bool answer;

//     public static int scoreCounter = 0;
//     public bool correctChecker = false;
//     public aniManager AniManager;

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(Input.GetKeyDown(KeyCode.O))
//         {
//             aniManager.Ani_A();
//             if (correctChecker == true)
//             {
//                 scoreCounter++;
//             }
//         }
//         if (Input.GetKeyDown(KeyCode.X))
//         {
//             aniManager.Ani_B();
//             if (correctChecker == false)
//             {
//                 scoreCounter++;
//             }
//         }

//     }
    
// }
