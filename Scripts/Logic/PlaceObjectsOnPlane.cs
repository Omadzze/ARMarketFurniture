using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectsOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    /*public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }*/

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    [SerializeField]
    int m_MaxNumberOfObjectsToPlace = 1;

    int m_NumberOfPlacedObjects = 0;

    [SerializeField]
    bool m_CanReposition = true;

    public bool canReposition
    {
        get => m_CanReposition;
        set => m_CanReposition = value;
    }
    
    public GameObject trackables;
    bool oneTime = false;
    public UIManager _uiManager;

    #region Timer

    public float timer = 10;

    public bool timerIsRunning = false;

    #endregion

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    public void Start()
    {
        //placedPrefab = DescriptionCanvas.Instance.game;
        trackables = GameObject.Find("Trackables");
        _uiManager.SwitchStetes(UIManager.ChangeStates.Move);
    }

    void Update()
    {
        if (oneTime == false)
        {
            for (int i = 0; i < trackables.transform.childCount; i++)
            {
                if (trackables.transform.GetChild(i).GetComponent<ARPlaneMeshVisualizer>() != null)
                {
                    Debug.Log("Plane found");
                    _uiManager.SwitchStetes(UIManager.ChangeStates.Place);
                    oneTime = true;
                }
            }
        }
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;
                    
                    if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
                    {
                        spawnedObject = Instantiate(DescriptionCanvas.Instance.game, hitPose.position, hitPose.rotation) as GameObject;
                        spawnedObject.transform.parent = m_PlacedPrefab.transform;
                        RotateAndScale.ApplyTarget(spawnedObject);
                        _uiManager.SwitchStetes(UIManager.ChangeStates.None);
                        m_NumberOfPlacedObjects++;
                        //ShowObjectHighlight();
                        timerIsRunning = true;
                        Shader.SetGlobalInteger("_IsHighligted", 1);
                    }
                    else
                    {
                        if (m_CanReposition)
                        {
                            spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                            timerIsRunning = true;
                            //ShowObjectHighlight();
                        }
                    }
                    
                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }
        
        if (timerIsRunning)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Highlighted disabled");
                Shader.SetGlobalInteger("_IsHighligted", 0);
                timerIsRunning = false;
            }
        }
    }

    /*public void ShowObjectHighlight()
    {
        //StartCoroutine(Timer());
    }
    
    IEnumerator Timer(){
        while (true)
        {
            //shader.material.SetInt("_Boolean", 1);
            Shader.SetGlobalInteger("_IsHighligted", 1);
            //shader.material.SetGl
            yield return new WaitForSeconds(5);
            Shader.SetGlobalInteger("_IsHighligted", 0);
            StopCoroutine(Timer());
            //Shader.SetGlobalInteger("_Boolean", 0);
            //shader.material.SetInt("_Boolean", 0);
        }
    }*/
}
