using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class DescriptionCanvas : MonoBehaviour
{
    #region UI_Elements
    public Image itemImage;

    public Image likeImage;

    public TMP_Dropdown dropdownColor;

    public TMP_Dropdown dropdownQuantity;

    public Text nameItem;

    public Text priceItem;

    public Text descriptionItem;
    #endregion

    #region ProgressBar

    public TextMeshProUGUI progressText;
    public GameObject ProgressBar;
    #endregion
    
    #region Instance

    private static DescriptionCanvas instance = null;

    public static DescriptionCanvas Instance
    {
        get { return instance; }
    }

    #endregion

    public string urlAndroid;
    public string hash;

    #region Canvas
    public GameObject viewInARCanvas;

    public GameObject descriptionCanvas;

    public GameObject listCanvas;
    #endregion

    public GameObject ARCamera;
    public Object game;

    #region Values

    private string name;
    private int price;
    public List<ItemData> list;

    #endregion
    private void Awake()
    {
        instance = this;
    }
    
    public void ShowCanvas(int index)
    {
        name = ListAdapter.Instance.nameList[index];
        price = ListAdapter.Instance.priceList[index];
        nameItem.text = name;
        priceItem.text = "$" + price;
        descriptionItem.text = ListAdapter.Instance.descriptionList[index];
        itemImage.sprite = ListAdapter.Instance.sprite[index];
        urlAndroid = ListAdapter.Instance.modelAndroid[index];
        hash = ListAdapter.Instance.hash[index];
    }

    public void ViewinAR()
    {
        ProgressBar.SetActive(true);
        StartCoroutine(GetAssetBundle());
    }
    IEnumerator GetAssetBundle()
    {
        Hash128 hashCode = new Hash128();
        hashCode.Append(hash);
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(urlAndroid, hashCode);
        Debug.Log( "isCached" + Caching.IsVersionCached(urlAndroid, hashCode));

        www.SendWebRequest();
        
        while (!www.isDone)
        {
            progressText.text = (string.Format("Downloading: " + "{0:P0}", www.downloadProgress));
            //Debug.Log(string.Format("Downloading: " + "{0:P0}", www.downloadProgress));
            yield return null;
        }

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            game = bundle.LoadAllAssets()[0];
            viewInARCanvas.SetActive(true);
            descriptionCanvas.SetActive(false);
            listCanvas.SetActive(false);
            ARCamera.SetActive(true);
        }
    }

    public void AddToCard()
    {
        ItemData items = new ItemData(name, "black", 1, price);
        list = new List<ItemData>();
        list.Add(items);
        Debug.Log("Added to card");
    }
}
