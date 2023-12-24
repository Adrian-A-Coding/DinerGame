using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingContainer : MonoBehaviour, IDetail, IDraggable
{
    private Snapping snapper;
    private Rigidbody rb;
    private Vector3 originalPoint;

    //Details now about the recipe and cooking features of this cooking pot
    [SerializeField] private List<string> yourIngredients = new List<string>(); //What the container has for the recipe
    private Recipe currentRecipe; //The recipe we're cooking up
    //[SerializeField] private int servingAmount; //How many customers will be fed from this specific container

    private void Awake()
    {
        snapper = GetComponent<Snapping>();
        originalPoint = transform.position; //Store the objects starting location followed by rotation
        rb = GetComponent<Rigidbody>(); //Get the objects rigid body to store for velocity
    }

    public void DetailDisplay()
    {
        throw new System.NotImplementedException();//Store and compare what ingredients are present and give it to the ui
    }

    public void OnRightClick()
    {
        throw new System.NotImplementedException();//Enable these details in the ui and show it
    }

    public void OnStartDrag()
    {
        rb.useGravity = false;
    }

    public void OnEndDrag()
    {
        snapper.Snap(originalPoint);//By default at the end of dragging cause the snap called from script component and give it the og point
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
    }
}
