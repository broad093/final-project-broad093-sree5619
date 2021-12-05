using UnityEngine;
public class Grabbable : InventoryTools
{
    private Grabber currentGrabber;

    // Start is called before the first frame update
    void Start()
    {
        currentGrabber = null;

        if (this.GetComponent<Rigidbody>())
        {
            this.GetComponent<Rigidbody>().Sleep();
        }
    }

    public void SetCurrentGrabber(Grabber grabber)
    {
        currentGrabber = grabber;
    }

    public Grabber GetCurrentGrabber()
    {
        return currentGrabber;
    }
}