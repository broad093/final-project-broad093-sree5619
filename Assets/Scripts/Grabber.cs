using UnityEngine;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    Brush brush;

    public virtual void Grab(InputAction.CallbackContext context)
    {

    }

    public virtual void Release(InputAction.CallbackContext context)
    {

    }

    public virtual void TouchDown(InputAction.CallbackContext context)
    {
        brush.laserPointer.enabled = false;
    }

    public virtual void TouchUp(InputAction.CallbackContext context)
    {
        brush.laserPointer.enabled = false;
    }
}