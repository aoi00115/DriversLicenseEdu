using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class sceneManager : MonoBehaviour
{
    public static List<string> QuestionScenes = new List<string>(){
        "Q1",
        "Q2",
        "Q3",
    };
    public Button SelectButton;
    private static int RandomIndex;
    private static string SelectedSceneName;


    private void Update()
    {
        foreach (string Question in QuestionScenes)
        {
            Debug.Log(Question);
        }
    }

    public void SelectRandomScene()
    {
        if (QuestionScenes.Count > 0)
        {
            RandomIndex = Random.Range(0, QuestionScenes.Count);
            SelectedSceneName = QuestionScenes[RandomIndex];
            SceneManager.LoadScene(SelectedSceneName);

        }
        else
        {
            SceneManager.LoadScene("res");
        }
        QuestionScenes.RemoveAt(RandomIndex);
    }

}

//int a = questionScenes.RemoveAll(x => x.Equals(sceneRandom));
//var sceneRandom = questionScenes[Random.Range(0, questionScenes.Count)];
