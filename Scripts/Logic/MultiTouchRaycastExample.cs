#if AR_FOUNDATION_5_0_OR_NEWER
    using ARSessionOrigin = Unity.XR.CoreUtils.XROrigin;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using ARFoundationRemoteExamples;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiTouchRaycastExample : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager = null;
    [SerializeField] ARSessionOrigin origin = null;
    [CanBeNull] [SerializeField] GameObject optionalPointerPrefab = null;
    [SerializeField] bool disablePointersOnTouchEnd = false;
    [SerializeField] TrackableType trackableTypeMask = TrackableType.Planes | TrackableType.FeaturePoint;

    readonly Dictionary<int, Transform> pointers = new Dictionary<int, Transform>();

    public GameObject trackables;
    bool oneTime = false;
    public GameObject findVideoCanvas;

    private GameObject spawnedObject;
    public Renderer shader;
    //public UIManager _uiManager;
    private void Start()
    {
        //_arPlaneMeshVisualizer = new ARPlaneMeshVisualizer();
        trackables = GameObject.Find("Trackables");
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
                    findVideoCanvas.SetActive(false);
                    oneTime = true;
                }
            }
        }

        for (int i = 0; i < InputWrapper.touchCount; i++)
        {
            var touch = InputWrapper.GetTouch(i);
            var pointer = getPointer(touch.fingerId);
            var touchPhase = touch.phase;
            if (touchPhase == TouchPhase.Ended || touchPhase == TouchPhase.Canceled)
            {
                if (disablePointersOnTouchEnd)
                {
                    pointer.gameObject.SetActive(false);
                }
            }
            else
            {
                var ray = origin.GetCamera().ScreenPointToRay(touch.position);
                var hits = new List<ARRaycastHit>();
                var hasHit = raycastManager.Raycast(ray, hits, trackableTypeMask);
                if (hasHit)
                {
                    var pose = hits.First().pose;
                    pointer.position = pose.position;
                    pointer.rotation = pose.rotation;
                }

                pointer.gameObject.SetActive(hasHit);
            }
        }
    }

    Transform getPointer(int fingerId)
    {
        if (pointers.TryGetValue(fingerId, out var existing))
        {
            return existing;
        }
        else
        {
            var newPointer = createNewPointer();
            pointers[fingerId] = newPointer;
            return newPointer;
        }
    }

    Transform createNewPointer()
    {
        var result = instantiatePointer();
        result.parent = transform;
        return result;
    }

    Transform instantiatePointer()
    {
        if (optionalPointerPrefab == null)
        {
            //return Instantiate(Description.Instance.game).transform;
            spawnedObject = Instantiate(DescriptionCanvas.Instance.game as GameObject);
            shader = spawnedObject.transform.GetChild(2).GetComponent<Renderer>();
            RotateAndScale.ApplyTarget(spawnedObject);
            //_uiManager.SwitchStetes(UIManager.ChangeStates.None);
            return Instantiate(spawnedObject).transform;
        }
        else
        {
            var result = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            result.localScale = Vector3.one * 0.05f;
            return result;
        }
    }
}