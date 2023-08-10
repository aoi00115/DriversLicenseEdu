using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class sceneManager : MonoBehaviour
{
    public static List<string> questionScenes = new List<string>(){
        "Q1",
        "Q2",
        "Q3",
    };
    public Button selectButton;
    private static int randomIndex;
    private static string selectedSceneName;


    private void Update()
    {
        foreach (string question in questionScenes)
        {
            Debug.Log(question);
        }
    }

    public void SelectRandomScene()
    {
        if (questionScenes.Count > 0)
        {
            randomIndex = Random.Range(0, questionScenes.Count);
            selectedSceneName = questionScenes[randomIndex];
            SceneManager.LoadScene(selectedSceneName);

        }
        else
        {
            SceneManager.LoadScene("res");
        }
        questionScenes.RemoveAt(randomIndex);
    }

}

//int a = questionScenes.RemoveAll(x => x.Equals(sceneRandom));
//var sceneRandom = questionScenes[Random.Range(0, questionScenes.Count)];
