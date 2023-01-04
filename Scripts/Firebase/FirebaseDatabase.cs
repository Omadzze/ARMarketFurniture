using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseDatabase : MonoBehaviour
{
    public GameObject listAdapter;

    #region Firebase
    public List<string> nameList;
    public List<int> priceList;
    public List<string> urlImagesList;
    public List<Sprite> sprite;
    public List<string> description;
    public List<string> modelAndroid;
    public List<string> hashValue;
    #endregion
    
    private static FirebaseDatabase instance = null;

    public TextMeshProUGUI nameOfCategory;

    public static FirebaseDatabase Instance
    {
        get { return instance; }
    }

    //public CloudData cloud;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Firebase.Database.FirebaseDatabase.DefaultInstance
            .GetReference(TransitionToScene.Instance.category)
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    // Handle the error...
                    Debug.LogError("Error occured with reading database");
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot furniture in snapshot.Children)
                    {
                        nameList.Add(furniture.Child("name").GetRawJsonValue().Replace("\"", ""));
                        priceList.Add(Convert.ToInt32(furniture.Child("price").GetRawJsonValue()));
                        urlImagesList.Add(furniture.Child("image").GetRawJsonValue().Replace("\"", ""));
                        description.Add(furniture.Child("description").GetRawJsonValue().Replace("\"", ""));
                        modelAndroid.Add(furniture.Child("modelAndroid").GetRawJsonValue().Replace("\"", ""));
                        hashValue.Add(furniture.Child("hash").GetRawJsonValue().Replace("\"", ""));
                        sprite.Add(null);
                    }
                    listAdapter.SetActive(true);
                    
                    // Do something with snapshot...
                }
            });
        
        nameOfCategory.text = TransitionToScene.Instance.category;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
