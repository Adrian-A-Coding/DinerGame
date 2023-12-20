using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private float mouseDragPhysicsSpeed = 10;
    [SerializeField]
    private float mouseDragSpeed = 0.1f;

    private WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    private Vector3 velocity = Vector3.zero;
    private CursorControls controls;
    private Camera cam;

    public Texture2D cursor;
    public Texture2D cursorClicked;

    private void Awake()
    {
        controls = new CursorControls();
        cam = Camera.main;
        //Setting the base cursor to the default texture
        CursorChanged(cursor);
        Cursor.lockState = CursorLockMode.Confined; //Lock cursor to game screen
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Kitchen.Click.started += LeftClick;
    }

    private void OnDisable()
    {
        controls.Kitchen.Click.started -= LeftClick;
        controls.Disable();
    }

    private void Start() {
        controls.Kitchen.RightClick.performed += _ => RightClick();
    }

    private void RightClick() {
        //Right click actions when clicking on object
        //Similar to left click only just check if it has collider and layer for selectability and then use interface for details
        //This should be something like fillable or loaded to show an ingredient count for a recipe and what's present
        //Should result as a bool to display the UI element for above functionality
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            if(hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Selectable")) {
                Debug.Log("Right Clicked");
            }
        }
    }

    private void LeftClick(InputAction.CallbackContext context) {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) { //If and object is hit store it
            if (hit.collider != null && hit.collider.gameObject.GetComponent<IDraggable>() != null) { //Check that it must have a collider
                StartCoroutine(DragUpdate(hit.collider.gameObject));
                CursorChanged(cursorClicked);//Change cursor as mouse clicked
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject) {
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        clickedObject.TryGetComponent<IDraggable>(out var draggable);
        draggable?.OnStartDrag();//If interface isn't null it will run the method for events when dragged
        float initialDistance = Vector3.Distance(clickedObject.transform.position, Camera.main.transform.position);

        while (controls.Kitchen.Click.ReadValue<float>() != 0)
        { //Add here condition that draggable object is ready to be dragged
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * mouseDragPhysicsSpeed;
                yield return fixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position,
                    ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                yield return null;
            }
        }
        draggable?.OnEndDrag();//Run end method once dragging has stopped if not null
        CursorChanged(cursor);//Change it again afterwards once mouse is no longer held down
    }

    //Function to change our cursor to the correct one according to action
    public void CursorChanged(Texture2D cursorType) {
        //Vector2 to check middle of cursor for selection instead of top left like a pointint one does
        //Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }
}
