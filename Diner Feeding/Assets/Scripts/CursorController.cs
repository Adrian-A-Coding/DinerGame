using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClick;
    [SerializeField]
    private float mouseDragPhysicsSpeed = 10;
    [SerializeField]
    private float mouseDragSpeed = 0.1f;

    private WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    private Vector3 velocity = Vector3.zero;

    public Texture2D cursor;
    public Texture2D cursorClicked;

    private void Awake()
    {
        //Setting the base cursor to the default texture
        CursorChanged(cursor);
        Cursor.lockState = CursorLockMode.Confined; //Lock cursor to game screen
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context) {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) { //If and object is hit store it
            if(hit.collider != null) { //Check that it must have a collider
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject) {
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        float initialDistance = Vector3.Distance(clickedObject.transform.position, Camera.main.transform.position);
        while (mouseClick.ReadValue<float>() != 0) {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null) {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * mouseDragPhysicsSpeed;
                yield return fixedUpdate;
            }
            else {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, 
                    ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                yield return null;
            }
        }
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
