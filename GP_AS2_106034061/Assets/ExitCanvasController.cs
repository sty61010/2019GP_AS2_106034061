using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas exitCanvas;
    private bool OpenSetting=true;
    void Start()
    {
        exitCanvas = GameObject.Find("Exit_Canvas").GetComponent<Canvas>();
        exitCanvas.enabled=true;    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(OpenSetting == false){
                exitCanvas.enabled=true;    
                OpenSetting = true;
            }
            else {
                exitCanvas.enabled=false;
                OpenSetting = false;
            }

		}

    }
    public void QuitClick(){
        Debug.Log("Quit");
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }
}
