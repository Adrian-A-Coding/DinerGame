using UnityEngine;

public class Snapping : MonoBehaviour, IDraggable
{
    public GameObject[] snapPoints;
    private Rigidbody rb;
    private Vector3 originalPoint;

    private void Start()
    {
        originalPoint = transform.position; //Store the objects starting location followed by rotation
        rb = GetComponent<Rigidbody>(); //Get the objects rigid body to store for velocity
    }

    public void onStartDrag()
    {
        Debug.Log("Dragging started");
        rb.useGravity = false;
    }
    //Likely move these two to their respective scripts for the object
    public void onEndDrag()
    {
        snap();//By default at the end of dragging cause the snap
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
    }

    public void snap() {
        for (int i = 0; i < snapPoints.Length; i++)
        {
            if (Vector3.Distance(snapPoints[i].transform.position, transform.position) < 1.0f) {
                transform.position = snapPoints[i].transform.position;
            }
            else { //Set it back to the original settings
                transform.position = originalPoint;
            }
        }
    }
}
