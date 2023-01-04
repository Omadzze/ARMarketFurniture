using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    public RawImage imageRectTransform;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angles = imageRectTransform.gameObject.transform.eulerAngles; 
        angles.z = angles.z - 70 * Time.deltaTime; // + rotationSpeed for right button
        imageRectTransform.gameObject.transform.eulerAngles = angles; 
    }
}
