using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public GameObject descriptionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void BackFromDescriptionToListView()
    {
        descriptionCanvas.SetActive(false);
    }

    public void BackFromChildObject()
    {
        GameObject parentGameobject = gameObject.transform.parent.gameObject;
        parentGameobject.SetActive(false);
    }
}
