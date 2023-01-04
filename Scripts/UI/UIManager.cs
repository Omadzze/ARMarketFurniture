using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public VideoClip moveVideo;
    public VideoClip placeVideo;
    
    public VideoPlayer videoObject;
    public TextMeshProUGUI videoText;

    const string moveText = "Move Device Slowly";
    const string placeText = "Tap to Place";
    
    public enum ChangeStates
    {
        Move,
        Place,
        None
    }

    public void SwitchStetes(ChangeStates currentState)
    {
        switch (currentState)
        {
            case ChangeStates.Move:
                videoText.text = moveText;
                videoObject.clip = moveVideo;
                break;
            case ChangeStates.Place:
                videoText.text = placeText;
                videoObject.clip = placeVideo;
                break;
            case ChangeStates.None:
                videoText.transform.gameObject.SetActive(false);
                videoObject.transform.gameObject.SetActive(false);
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
