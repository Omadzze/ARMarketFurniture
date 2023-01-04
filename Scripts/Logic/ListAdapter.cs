using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListAdapter : MonoBehaviour
{
    [SerializeField] private Sprite defaultIcon;
    public GameObject progressBar;
    public GameObject canvasDescription;
    public GameObject whitePanel;

    #region Lists
    public List<string> nameList;
    public List<int> priceList;
    public List<string> urlImagesList;
    public List<Sprite> sprite;
    public List<string> descriptionList;
    public List<string> modelAndroid;
    public List<string> hash;
    #endregion

    #region Instance

    private static ListAdapter instance = null;

    public static ListAdapter Instance
    {
        get { return instance; }
    }

    #endregion
    
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(GetImages());
    }

    void RenderList()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        //int N = allFurnitures.Length;
        for (int i = 0; i < FirebaseDatabase.Instance.nameList.Count; i++)
        {
            sprite.Add(FirebaseDatabase.Instance.sprite[i]);
            nameList.Add(FirebaseDatabase.Instance.nameList[i]);
            priceList.Add(FirebaseDatabase.Instance.priceList[i]);
            descriptionList.Add(FirebaseDatabase.Instance.description[i]);
            modelAndroid.Add(FirebaseDatabase.Instance.modelAndroid[i]);
            hash.Add(FirebaseDatabase.Instance.hashValue[i]);
            
            g = Instantiate(buttonTemplate, transform);
            g.transform.GetChild(0).GetComponent<Image>().sprite = sprite[i];
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = nameList[i];
            g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "$" + priceList[i];
            
            g.GetComponent<Button>().AddEventListener(i, ItemClicked);
            g.SetActive(true);
        }
        progressBar.SetActive(false);
        whitePanel.SetActive(false);
        
        Destroy(buttonTemplate);
    }

    IEnumerator GetImages()
    {
        for (int i = 0; i < FirebaseDatabase.Instance.urlImagesList.Count; i++)
        {
            Debug.Log("Download");
            WWW w = new WWW(FirebaseDatabase.Instance.urlImagesList[i]);
            yield return w;

            if (w.error != null)
            {
                FirebaseDatabase.Instance.sprite[i] = defaultIcon;
            }
            else
            {
                if (w.isDone)
                {
                    Texture2D tx = w.texture;
                    FirebaseDatabase.Instance.sprite[i] =
                        Sprite.Create(tx, new Rect(0f, 0f, tx.width, tx.height), Vector2.zero, 10f);
                }
            }
        }
        
        RenderList();
    }

    public void ItemClicked(int itemIndex)
    {
        Debug.Log("Item " + itemIndex + " clicked" );
        canvasDescription.SetActive(true);
        DescriptionCanvas.Instance.ShowCanvas(itemIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate
        {
            OnClick(param);
        });
    }
}
