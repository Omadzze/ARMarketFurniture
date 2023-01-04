using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItems : MonoBehaviour
{
    public List<GameObject> buttonsList = new List<GameObject>();

    public List<GameObject> itemList = new List<GameObject>();

    public int index;

    public int countPrefabs;

    public bool wasCreated;
    // Start is called before the first frame update
    void Start()
    {
    }
    // when user pressing the button you should create new gameoject
    // after he pressing another button you should delete your gameobject
    public void ItemIndex(int indexx)
    {
        index = indexx;
        if (wasCreated == false)
        {
            var instantiate = Instantiate(itemList[index], Vector3.zero, transform.rotation);
            instantiate.transform.parent = gameObject.transform;
            wasCreated = true;
        }
        else
        {
            DeleteItem();
            var instantiate = Instantiate(itemList[index], Vector3.zero, transform.rotation);
            instantiate.transform.parent = gameObject.transform;
        }
    }
    

    public void DeleteItem()
    {
        foreach (Transform item in transform)
        {
            GameObject.Destroy(item.gameObject);
        }
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
