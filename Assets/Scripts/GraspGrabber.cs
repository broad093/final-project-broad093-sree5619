using UnityEngine;
using UnityEngine.InputSystem;

public class GraspGrabber : Grabber
{
    public Transform controller;

    public InputActionProperty grabAction;

    public Vector3 leftContr;
    public Vector3 rightContr;
    public Vector3 point;
    public Vector3 controllerPos;

    Grabbable currentObject;
    Grabbable grabbedObject;
    Grabbable currentTool;

    public float distance;
    public float contrMovement;

    public bool buttonPress;

    // Start is called before the first frame update
    void Start()
    {
        buttonPress = false;

        grabbedObject = null;
        currentObject = null;
        currentTool = null;

        grabAction.action.performed += Grab;
        grabAction.action.canceled += Release;
    }

    private void OnDestroy()
    {

        grabAction.action.performed -= Grab;
        grabAction.action.canceled -= Release;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void Grab(InputAction.CallbackContext context)
    {

        if (currentObject && grabbedObject == null)
        {
           if (currentObject.GetCurrentGrabber() != null)
            {
                currentObject.GetCurrentGrabber().Release(new InputAction.CallbackContext());
            }

            grabbedObject = currentObject;
            grabbedObject.SetCurrentGrabber(this);

            if (grabbedObject.GetComponent<Rigidbody>())
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            }

            
            if (grabbedObject.GetComponent<InventoryTools>())
            {
                currentTool = grabbedObject;
                currentTool.transform.parent = this.transform;
                currentTool.GetComponent<InventoryTools>().SetCurrentTool(currentTool);
                grabbedObject = null;
            }
            grabbedObject.transform.parent = this.transform;

        }
        
    }

    public override void Release(InputAction.CallbackContext context)
    {
        if (grabbedObject)
        {
            if (grabbedObject.GetComponent<Rigidbody>())
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            }

            grabbedObject.SetCurrentGrabber(null);
            grabbedObject.transform.parent = null;
            grabbedObject = null;
        }
        else if (currentTool)
        {
            // WORKS
            currentTool.transform.parent = this.transform;
            currentTool.GetComponent<InventoryTools>().SetToolPosition(currentTool);
            currentTool.GetComponent<InventoryTools>().SetEquipped(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentObject == null && other.GetComponent<Grabbable>())
        {
            currentObject = other.gameObject.GetComponent<Grabbable>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (currentObject)
        {
            if (other.GetComponent<Grabbable>() && currentObject.GetInstanceID() == other.GetComponent<Grabbable>().GetInstanceID())
            {
                currentObject = null;
            }
        }
    }
}