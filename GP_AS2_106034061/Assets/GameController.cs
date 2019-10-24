using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public AudioSource audioSource;
    
    public AudioClip[] bgList;

    public void OnToggleChange(bool t){
        audioSource.mute = t;
        cancelFocus();
    }

    public void OnDropdownChange(int index){
        audioSource.clip = bgList[index];
        audioSource.Play();
        cancelFocus();
    }

    public void OnSliderChange(float value){
        audioSource.volume = value;
    }

    void cancelFocus(){
        EventSystem.current.SetSelectedGameObject(null);
    }
}
