using UnityEngine;

public class Snapping : MonoBehaviour
{
    [SerializeField]
    private GameObject[] snapPoints;

    public void Snap(Vector3 ogPoint) { //refactor this
        for (int i = 0; i < snapPoints.Length; i++)
        {
            if (Vector3.Distance(snapPoints[i].transform.position, transform.position) < 1.0f) {
                transform.position = snapPoints[i].transform.position;
            }
            else { //Set it back to the original settings
                transform.position = ogPoint;
            }
        }
    }
}
