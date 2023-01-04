using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RotateAndScale : MonoBehaviour
{

    [Tooltip("Object to rotate")] public Transform target;

    [Tooltip("Rotation axies")] public Axises rotationAxis = Axises.Y;

    [Header("Rotation speed")] [Tooltip("Rotation speed")] [Range(0.05f, 2)]
    public float speed = .3f;

    private Vector3 axis;
    private Vector3 oldMousePos;
    [Header("Scale options")] public float minScale = 0.00048f;
    public float maxScale = 0.00123f;

    public static string preferredAxis = string.Empty;

    public GameObject parentGameObject;

    public Transform Target;
    public float TurnSpeed = 500.00F;
    private Transform myTransform;
    public bool video = false;
    Vector3 directionToObject;


    Vector3 oldMousepos; //Used for windows buld
    Vector2 startPos; //Used for mobile
    Vector2 startPos0; //Used for mobile
    Vector2 startPos1; //Used for mobile    

    public static RotateAndScale Instance;

    //public Vector3 scale = new Vector3(1, 1, 1);

    private void Start()
    {
        parentGameObject = gameObject;
        
        if (preferredAxis != string.Empty)
        {
            SetNewAxis(preferredAxis);
        }
        else
        {
            InitAxis();
        }

        Instance = this; //TODO

        myTransform = parentGameObject.GetComponent<Transform>(); // Cache own Transform component
        GameObject target = GameObject.FindGameObjectWithTag("MainCamera");
        Target = target.transform;
        startPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
            gameObject.transform.position.z);
    }

    private void InitAxis()
    {
        axis = Vector3.zero;
        axis[(int) rotationAxis] = 1;
    }

    private void OnEnable()
    {
        if (gameObject.transform.childCount == 0)
        {
            if (gameObject.transform.parent)
                gameObject.transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).localScale = new Vector3(1, 1, 1);
            Debug.Log("ResetScale");
        }
    }

    /// <summary>
    /// Set new rotation axis
    /// </summary>
    /// <param name="axisName"></param>
    public void SetNewAxis(string axisName)
    {
        axisName = axisName.ToUpper();
        Axises naxe;
        if (Enum.TryParse<Axises>(axisName, out naxe))
        {
            rotationAxis = naxe;
            InitAxis();
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            oldMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            //rotate like touch
            //X ONLY
            float delta = oldMousePos.x - Input.mousePosition.x;
            target.Rotate(axis, delta * speed, Space.Self);
            oldMousePos = Input.mousePosition;
        }

#endif
        if (Input.touchCount == 1)
            {
                //start detect touch
                float delta = 0f;
                Touch touch = Input.GetTouch(0);

                // Handle finger movements based on touch phase.
                switch (touch.phase)
                {
                    // Record initial touch position.
                    case TouchPhase.Began:
                        startPos = touch.position;
                        break;
                    case TouchPhase.Moved:
                        //Rotate by direction
                        delta = startPos.x - touch.position.x;
                        break;
                }

                startPos = touch.position;
                target.Rotate(axis, delta * speed);
            }

            
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            if (touchZero.phase == TouchPhase.Began)
            {
                startPos0 = touchZero.position;
            }

            if (touchOne.phase == TouchPhase.Began)
            {
                startPos1 = touchOne.position;
            }

            if (touchZero.phase == TouchPhase.Moved && touchZero.phase == TouchPhase.Moved)
            {
                float olddist = Vector3.Distance(startPos0, startPos1);
                float ndist = Vector3.Distance(touchZero.position, touchOne.position);
                Vector3 localScale = target.localScale;
                float sing = Mathf.Sign(ndist - olddist);
                // working example
                //localScale = localScale + sing * Vector3.one * (ndist / olddist) * 0.04f;
                localScale = localScale + sing * Vector3.one * (ndist / olddist) * 0.0004f;

                //Clamp 
                float cscale = Mathf.Clamp(localScale.x, minScale, maxScale);
                localScale = Vector3.one * cscale;

                target.localScale = localScale;

                startPos0 = touchZero.position;
                startPos1 = touchOne.position;
            }
        }
    }
    
public static void ApplyTarget(GameObject target)
{
    RotateAndScale rtsc = target.AddComponent<RotateAndScale>();
    rtsc.target = target.transform;
}

}


public enum Axises
{
    X = 0,
    Y = 1,
    Z = 2
}