using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cart : MonoBehaviour
{
    /*public TextMeshProUGUI name;

    public TextMeshProUGUI color;

    public TextMeshProUGUI quantity;

    public TextMeshProUGUI price;*/
    // Start is called before the first frame update
    void Start()
    {
        RenderList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void RenderList()
    {
        GameObject cart = transform.GetChild(0).gameObject;
        GameObject g;
        //int N = allFurnitures.Length;
        foreach (var item in DescriptionCanvas.Instance.list)
        {
            g = Instantiate(cart, transform);
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemName;
            g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.itemColor;
            g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "" + item.itemQuantity;
            g.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "$" + item.itemPrice;
        }
        for (int i = 0; i < DescriptionCanvas.Instance.list.Count; i++)
        {
            /*name.Add(FirebaseDatabase.Instance.sprite[i]);
            nameList.Add(FirebaseDatabase.Instance.nameList[i]);
            priceList.Add(FirebaseDatabase.Instance.priceList[i]);
            descriptionList.Add(FirebaseDatabase.Instance.description[i]);
            modelAndroid.Add(FirebaseDatabase.Instance.modelAndroid[i]);
            hash.Add(FirebaseDatabase.Instance.hashValue[i]);*/
            
            //g = Instantiate(cart, transform);
            //g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
            /*g.transform.GetChild(0).GetComponent<Image>().sprite = sprite[i];
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = nameList[i];
            g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "$" + priceList[i];
            
            g.GetComponent<Button>().AddEventListener(i, ItemClicked);*/
            //g.SetActive(true);
        }
        /*progressBar.SetActive(false);
        whitePanel.SetActive(false);
        
        Destroy(buttonTemplate);*/
    }
}
