using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeToDelete : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform topView;

    private Vector3 mouseStartPosition;
    private Vector3 topViewStartPosition;

    private float horizontalDragMinDistance = 30f;
    private float verticalDragMinDistance = 0f;

    private bool horizontalDrag = false;
    private bool verticalDrag = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseStartPosition = Input.mousePosition;
        topViewStartPosition = topView.localPosition;

        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.beginDragHandler);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 distance = Input.mousePosition - mouseStartPosition;

        if (!horizontalDrag && !verticalDrag)
        {
            if (distance.x > horizontalDragMinDistance)
            {
                horizontalDrag = true;
                return;
            } else if (Math.Abs(distance.y) > verticalDragMinDistance)
            {
                verticalDrag = true;
                return;
            }
        }

        if (horizontalDrag)
        {
            float newPositionX = topViewStartPosition.x + MathF.Max(0f, distance.x);
            topView.localPosition = new Vector3(newPositionX, topViewStartPosition.y, topViewStartPosition.z);
        } else if (verticalDrag)
        {
            ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.dragHandler);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (horizontalDrag)
        {
            Vector3 distance = Input.mousePosition - mouseStartPosition;

            if (distance.x > 100f)
            {
                Destroy(gameObject);
            }
            else
            {
                topView.localPosition = topViewStartPosition;
            }
        }

        horizontalDrag = false;
        verticalDrag = false;
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.endDragHandler);
    }
}
