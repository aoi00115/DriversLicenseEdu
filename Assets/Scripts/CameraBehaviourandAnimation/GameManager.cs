using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 0.0f;
    public float timer = 0.0f;        // Timer that activates upon starting

    [Header("Stage Setting")]
    public bool answer; 
    public bool title;                // Change this variable in the inspector for every question
    public bool stage1;
    public bool stage2;
    public bool stage3;
    public bool stage4;
    public bool correctChecker;
    public static int scoreCounter = 0; 

    bool isAnswered = false;

    [Header("Reference")]
    Animator animator;
    public GameObject car;
    public GameObject pivotPoint;
    public GameObject copCar;
    public GameObject copCarLight;
    public PostProcessVolume postProcessingVolume;
    public ChromaticAberration chromaticAberration;
    public CanvasGroup questionCG;
    public RectTransform questionRT;
    public CanvasGroup correctCG;
    public RectTransform correctRT;
    public CanvasGroup wrongCG;
    public RectTransform wrongRT;
    public Vector2 correctLocalPos;
    public Vector2 wrongLocalPos;
    public CanvasGroup correctICG;
    public RectTransform correctIRT;
    public CanvasGroup wrongICG;
    public RectTransform wrongIRT;

    float postTimer = 0f;               // Timer that activates upon answering

    public static List<string> QuestionScenes = new List<string>(){
        "Stage1",
        "Stage2",
        "Stage3",
        "Stage4"
    };
    static int RandomIndex;
    static string SelectedSceneName;

    // Start is called before the first frame update
    void Start()
    {
        if(!title)
        {
            animator = GameObject.Find("Car").GetComponent<Animator>();
            car = GameObject.Find("Car");
            pivotPoint = GameObject.Find("Car/PivotPoint");

            if(stage1)
            {                
                copCar = GameObject.Find("CopCar");
                copCarLight = GameObject.Find("CopCar/CopCar/Cube.001");
                copCarLight.SetActive(false);
            }

            questionCG = GameObject.Find("Canvas/Question").GetComponent<CanvasGroup>();
            questionRT = GameObject.Find("Canvas/Question").GetComponent<RectTransform>();
            correctCG = GameObject.Find("Canvas/Correct").GetComponent<CanvasGroup>();
            correctRT = GameObject.Find("Canvas/Correct").GetComponent<RectTransform>();
            wrongCG = GameObject.Find("Canvas/Wrong").GetComponent<CanvasGroup>();
            wrongRT = GameObject.Find("Canvas/Wrong").GetComponent<RectTransform>();
            correctICG = GameObject.Find("Canvas/CorrectIndicator").GetComponent<CanvasGroup>();
            correctIRT = GameObject.Find("Canvas/CorrectIndicator").GetComponent<RectTransform>();
            wrongICG = GameObject.Find("Canvas/WrongIndicator").GetComponent<CanvasGroup>();
            wrongIRT = GameObject.Find("Canvas/WrongIndicator").GetComponent<RectTransform>();
            questionCG.alpha = 0f;
            correctCG.alpha = 0f;
            wrongCG.alpha = 0f;
            
            // Storing the original position of correct/wrong prompt in variables
            correctLocalPos = correctRT.transform.localPosition;
            wrongLocalPos = wrongRT.transform.localPosition;
        }
        
        postProcessingVolume = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>();
        postProcessingVolume.profile.TryGetSettings(out chromaticAberration);
    }

    // Update is called once per frame
    void Update()
    {
        if(!title)
        {
            timer += Time.deltaTime;

            if(!isAnswered && timer > 3.5f)
            {
                // Checing if the user input is correct
                if(Input.GetKeyDown(KeyCode.O))
                {
                    chromaticAberration.intensity.value = 0;
                    correctChecker = true;
                    isAnswered = true;
                    PromptFadeOut();
                }
                if(Input.GetKeyDown(KeyCode.X))
                {
                    chromaticAberration.intensity.value = 0;
                    correctChecker = false;
                    isAnswered = true;
                    PromptFadeOut();
                }
            }

            if(isAnswered)  // Set the timer after the answer for the 
            {              
                postTimer += Time.deltaTime;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SelectRandomScene();
            }
        } 

        // Debug
        // Debug.Log(speed);
        // Debug.Log(timer);
        // Debug.Log(chromaticAberration.intensity.value);
        // foreach (string Question in QuestionScenes)
        // {
        //     Debug.Log(Question);
        // }
        Debug.Log(scoreCounter);
    }

    void FixedUpdate()
    {
        if(!title)
        {
            QuestionFadeIn();
            ChoiceFadeIn();

            if(!isAnswered)                     // If not answered
            {
                PreAnswerAnimation();
            }
            else                                // If answered
            {
                if(correctChecker == answer)    // Play the correct animation
                {
                    CorrectWrongTextAnimation();
                    CorrectAnimation();       
                    scoreCounter++;
                }
                else                            // Play the wrong animation
                {
                    WrongAnimation();
                    CorrectWrongTextAnimation();
                }
                Invoke("SelectRandomScene", 5.0f); // Changing the scene and resetting everything
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
        speed = 3.0f;
        car.transform.position += new Vector3(0, 0, speed * Time.deltaTime);

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
        Debug.Log("CorrectAnswer");
    }

    void WrongAnimation()
    {
        if(stage1)
        {
            if(car.transform.position.z <= -1.33510f)
            {
                speed = 3.0f;
                car.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            }
            else
            {
                animator.enabled = true;
                animator.SetBool("isPlaying", true);
                copCarLight.SetActive(true);
                // copCar.transform.position = new Vector3(2.94000006f, 0.00600000005f, 44.2999992f);

                if(postTimer > 0.8f)
                {
                    Debug.Log("Cops Aproaching");
                    if(copCar.transform.position.z >= 4.4f)
                    {
                        copCar.transform.position -= new Vector3(0, 0, 20 * Time.deltaTime);
                    }
                }
            }
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

    void QuestionFadeIn()
    {
        if(timer > 1f && timer < 2f)
        {
            questionRT.transform.localPosition = new Vector3(0f, 100f, 0f);
            questionRT.DOAnchorPos(new Vector2(0f, 0f), 0.25f, false).SetEase(Ease.OutSine);
            questionCG.DOFade(1, 0.25f).SetEase(Ease.OutSine);
        }
    }

    void ChoiceFadeIn()
    {
        if(timer > 2f && timer < 3f)
        {
            correctRT.transform.localPosition = new Vector3(correctRT.transform.localPosition.x - 20f, correctRT.transform.localPosition.y, 0f);
            correctRT.DOAnchorPos(correctLocalPos, 0.25f, false).SetEase(Ease.OutSine);
            correctCG.DOFade(1, 0.25f).SetEase(Ease.OutSine);
        }
        if(timer > 3f && timer < 4f)
        {
            wrongRT.transform.localPosition = new Vector3(wrongRT.transform.localPosition.x - 20f, wrongRT.transform.localPosition.y, 0f);
            wrongRT.DOAnchorPos(wrongLocalPos, 0.25f, false).SetEase(Ease.OutSine);
            wrongCG.DOFade(1, 0.25f).SetEase(Ease.OutSine);
        }
    }

    void PromptFadeOut()
    {
        questionCG.DOFade(0, 0.25f);
        if(correctChecker)
        {  
            correctRT.DOAnchorPos(new Vector2(0f, 0f), 0.25f, false).SetEase(Ease.OutSine);
            correctCG.DOFade(0, 1f).SetEase(Ease.InQuint);
            wrongCG.DOFade(0, 0.25f);
        }
        else
        {
            correctCG.DOFade(0, 0.25f);
            wrongRT.DOAnchorPos(new Vector2(0f, 0f), 0.25f, false).SetEase(Ease.OutSine);
            wrongCG.DOFade(0, 1f).SetEase(Ease.InQuint);
        }
    }

    void CorrectWrongTextAnimation()
    {
        if(correctChecker == answer)
        {
            Debug.Log("CorrectPlayed");
            correctIRT.transform.localScale = new Vector3(2f, 2f, 2f);
            correctIRT.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.OutSine);
            correctICG.DOFade(1, 0.1f).SetEase(Ease.OutSine);
        }
        else
        {           
            if(postTimer > 2f)
            {
                Debug.Log("WrongPlayed");
                wrongIRT.transform.localScale = new Vector3(2f, 2f, 2f);
                wrongIRT.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.OutSine);
                wrongICG.DOFade(1, 0.1f).SetEase(Ease.OutSine);
            }
        }
    }

    public void SelectRandomScene()
    {
        if (QuestionScenes.Count > 0)
        {
            RandomIndex = Random.Range(0, QuestionScenes.Count);
            SelectedSceneName = QuestionScenes[RandomIndex];
            SceneManager.LoadScene(SelectedSceneName);
            QuestionScenes.RemoveAt(RandomIndex);
        }
        else
        {
            Debug.Log("Show Result");
            // ResultScreen();
        }
        isAnswered = false;
        timer = 0;
    }

    // public void ResultScreen()
    // {

    //     resultText.SetActive(true);
    //     scoreText.text = "/ 4";
    //     replayPrompt.SetActive(true);
    // }
}
