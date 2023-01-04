using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToScene : MonoBehaviour
{
    
    public string category;

    private static TransitionToScene instance = null;

    public static TransitionToScene Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transition(string categoryName)
    {
        category = categoryName;
        SceneManager.LoadScene(1);
    }

    public void TransitionByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
