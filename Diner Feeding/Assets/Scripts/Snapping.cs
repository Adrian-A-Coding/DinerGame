using UnityEngine;

public class Snapping : MonoBehaviour
{
    private Vector3 startPoint;
    [SerializeField]
    private GameObject[] snapPoints;

    private void Awake()
    {
        startPoint = GetComponent<CookingContainer>().GivePoint(); //Get whatever starting point is present for the obj then store here
    }

    public void Snap() {
        for (int i = 0; i < snapPoints.Length; i++)
        {
            if (Vector3.Distance(snapPoints[i].transform.position, transform.position) < 1.0f) {
                transform.position = snapPoints[i].transform.position;
            }
            else { //Set it back to the original settings
                transform.position = startPoint;
            }
        }
    }
}
