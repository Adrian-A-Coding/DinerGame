using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;

    private CursorControls controls;

    private void Awake()
    {
        controls = new CursorControls(); //Instatiate new controls
        //Setting the base cursor to the default texture
        CursorChanged(cursor);
        Cursor.lockState = CursorLockMode.Confined; //Lock cursor to game screen
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.performed += _ => EndedClick();
    }

    private void StartedClick() {
        //Set clicked cursor when clicked
        CursorChanged(cursorClicked);
    }

    private void EndedClick() {
        //Change back to pointer when finished grabbing
        CursorChanged(cursor);
    }

    //Function to change our cursor to the correct one according to action
    public void CursorChanged(Texture2D cursorType) {
        //Vector2 to check middle of cursor for selection instead of top left like a pointint one does
        //Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }
}
