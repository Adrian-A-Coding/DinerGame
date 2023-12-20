using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingContainer : MonoBehaviour, IDetail, IDraggable
{
    private Snapping snapper;
    private Rigidbody rb;
    public Vector3 originalPoint;

    private void Awake()
    {
        snapper = GetComponent<Snapping>();
        originalPoint = transform.position; //Store the objects starting location followed by rotation
        rb = GetComponent<Rigidbody>(); //Get the objects rigid body to store for velocity
    }

    public void DetailDisplay()
    {
        throw new System.NotImplementedException();
    }

    public void OnRightClick()
    {
        throw new System.NotImplementedException();
    }

    public void OnStartDrag()
    {
        rb.useGravity = false;
    }

    public void OnEndDrag()
    {
        snapper.Snap();//By default at the end of dragging cause the snap called from script component
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
    }
}
