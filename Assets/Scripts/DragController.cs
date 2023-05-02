// This code is originally from https://www.youtube.com/watch?v=fOHK-pbgiD8 and
// has been modified for horizontal functionality and locking tiles.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class DragController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform currentTransform;
    private GameObject mainContent;
    private Vector3 currentPosition;

    public AudioSource tileShuffle;

    public Texture2D cursorGrab;

    private int totalChild;

    public void OnPointerDown(PointerEventData eventData)
    {
        currentPosition = currentTransform.position;
        mainContent = currentTransform.parent.gameObject;
        totalChild = mainContent.transform.childCount;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!currentTransform.CompareTag("Locked"))
        {
            Cursor.SetCursor(cursorGrab, new Vector2(16f, 16f), CursorMode.ForceSoftware);
            currentTransform.position =
                new Vector3(eventData.position.x, currentTransform.position.y, currentTransform.position.z);

            for (int i = 0; i < totalChild; i++)
            {
                if (i != currentTransform.GetSiblingIndex())
                {
                    Transform otherTransform = mainContent.transform.GetChild(i);
                    if (!otherTransform.CompareTag("Locked"))
                    {
                        int distance = (int)Vector3.Distance(currentTransform.position,
                        otherTransform.position);
                        if (distance <= 15)
                        {
                            Vector3 otherTransformOldPosition = otherTransform.position;
                            otherTransform.position = new Vector3(currentPosition.x, otherTransform.position.y,
                                otherTransform.position.z);
                            currentTransform.position = new Vector3(otherTransformOldPosition.x, currentTransform.position.y,
                                currentTransform.position.z);
                            currentTransform.SetSiblingIndex(otherTransform.GetSiblingIndex());
                            currentPosition = currentTransform.position;
                        }
                    }
                    
                }
            }
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        currentTransform.position = currentPosition;
        tileShuffle.Play();
    }
}