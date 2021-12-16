using UnityEngine;
using UnityEngine.InputSystem;

public class GraspWhiteSpace : Grabber
{
    public Transform controller;

    public InputActionProperty grabAction;

    public Vector3 point;
    public Vector3 controllerPos;

    Grabbable currentObject;
    Grabbable grabbedObject;
    Grabbable currentTool;
    bool equipped;

    public float distance;
    public float contrMovement;

    public bool buttonPress;

    // Start is called before the first frame update
    void Start()
    {
        buttonPress = false;
        equipped = false;

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

            
            if (grabbedObject.GetComponent<ToolsWhiteSpace>())
            {
                currentTool = grabbedObject;
                currentTool.transform.parent = this.transform;
                currentTool.GetComponent<ToolsWhiteSpace>().SetCurrentTool(currentTool);
                grabbedObject = null;
                equipped = true;
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

        // Locks the position of the brush into the controller's position so that one can use it without
        // continously holding the grab button. Disables the mesh render of the controller holding the brush.
        else if (equipped)
        {
            currentTool.transform.parent = this.transform;
            currentTool.GetComponent<ToolsWhiteSpace>().SetToolPosition(currentTool);
            this.gameObject.transform.Find("controller_ply").GetComponent<Renderer>().enabled = false;

            // Checks which hand is holding the brush and makes sure that the opposite hand's controller mesh is showing.
            if (this.gameObject.CompareTag("RightHand"))
            {
                GameObject other = GameObject.FindWithTag("LeftHand");
                other.transform.Find("controller_ply").GetComponent<Renderer>().enabled = true;
            }
            else if (this.gameObject.CompareTag("LeftHand"))
            {
                GameObject other = GameObject.FindWithTag("RightHand");
                other.transform.Find("controller_ply").GetComponent<Renderer>().enabled = true;
            }

            // Set to false so that you can change which hand is holding the brush
            equipped = false;
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