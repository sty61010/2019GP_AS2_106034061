using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void OnButtonClick(){
        Debug.Log("click");
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
