using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void OnButtonClick(){
        Debug.Log("Start");
        SceneManager.LoadScene("Game_1", LoadSceneMode.Single);
    }
    public void QuitClick(){
        Debug.Log("Quit");
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }
}
