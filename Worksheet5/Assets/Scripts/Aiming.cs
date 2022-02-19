using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Transform mGunTransform;
    public LayerMask mPlayerMask;
    public Canvas mCanvas;
    public RectTransform mCrossHair;

    public void Aim()
    {
        Vector3 dir = -mGunTransform.right.normalized;
        // Find gunpoint as mentioned in the worksheet.
        Vector3 gunpoint = mGunTransform.transform.position +
                       dir * 1.2f -
                       mGunTransform.forward * 0.1f;
        // Fine the layer mask for objects that you want to intersect with.
        LayerMask objectsMask = ~mPlayerMask;

        // Do the Raycast
        RaycastHit hit;
        bool flag = Physics.Raycast(gunpoint, dir,
                    out hit, 50.0f, objectsMask);
        if (flag)
        {
            // Draw a line as debug to show the aim of fire in scene view.
            Debug.DrawLine(gunpoint, gunpoint +
                (dir * hit.distance), Color.red, 0.0f);

            // Find the transformed intersected point to screenspace
            // and then transform the crosshair position to this
            // new position.
            // first you need the RectTransform component of your mCanvas
            RectTransform CanvasRect = mCanvas.GetComponent<RectTransform>();

            // then you calculate the position of the UI element.
            // Remember that 0,0 for the mCanvas is at the centre of the screen. 
            // But WorldToViewPortPoint treats the lower left corner as 0,0. 
            // Because of this, you need to subtract the height / width 
            // of the mCanvas * 0.5 to get the correct position.

            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(hit.point);
            Vector2 WorldObject_ScreenPosition = new Vector2(
                ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the UI element
            mCrossHair.anchoredPosition = WorldObject_ScreenPosition;


            // Enable or set active the crosshair gameobject.
            mCrossHair.gameObject.SetActive(true);
        }
        else
        {
            // Hide or set inactive the crosshair gameobject.
            mCrossHair.gameObject.SetActive(false);
        }
    }
}
