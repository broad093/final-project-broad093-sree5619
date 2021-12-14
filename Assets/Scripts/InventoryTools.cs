using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class InventoryTools : InventoryBag
{
    private Grabbable currentTool;
    GameObject thisTool;

    public GameObject lContr;
    public GameObject rContr;

    private bool inInventory;
    private bool equipped;

    // Start is called before the first frame update
    void Start()
    {
        lContr.GetComponent<Brush>().enabled = false;
        lContr.GetComponent<LineRenderer>().enabled = false;
        rContr.GetComponent<Brush>().enabled = false;
        rContr.GetComponent<LineRenderer>().enabled = false;
        inInventory = false;
        equipped = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetCurrentTool(Grabbable tool)
    {
        currentTool = tool;
        equipped = true;
        thisTool = tool.GetComponent<Rigidbody>().gameObject;
        if (!inInventory)
        {
            AddToInventory(currentTool);
            inInventory = true;
        }
    }

    public Grabbable GetCurrentTool()
    {
        return currentTool;
    }

    public void SetToolPosition(Grabbable tool)
    {
        thisTool = tool.GetComponent<Rigidbody>().gameObject;

        if (thisTool.CompareTag("Brush"))
        {
            thisTool.transform.localPosition = new Vector3(0f, 0.15f, 0.15f);
            thisTool.transform.localRotation = Quaternion.Euler(-45, 0, 0);
            HoldingBrush();
        }
    }

    public GameObject GetCurrentToolasObject()
    {
        return thisTool;
    }

    public bool CheckInventory()
    {
        return inInventory;
    }

   public void SetEquipped(bool equip)
    {
        equipped = equip;
    }

    public bool isEquipped()
    {
        return equipped;
    }

    // Check which hand the brush is in and disable the line renderer and brush script in other if it's not currently held
    void HoldingBrush()
    {
        if (thisTool.transform.IsChildOf(rContr.transform))
        {
            print("works right");
            rContr.GetComponent<Brush>().enabled = true;
            rContr.GetComponent<LineRenderer>().enabled = true;
            lContr.GetComponent<Brush>().enabled = false;
            lContr.GetComponent<LineRenderer>().enabled = false;
        }
        else if (thisTool.transform.IsChildOf(lContr.transform))
        {
            print("works left");
            lContr.GetComponent<Brush>().enabled = true;
            lContr.GetComponent<LineRenderer>().enabled = true;
            rContr.GetComponent<Brush>().enabled = false;
            rContr.GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            lContr.GetComponent<Brush>().enabled = false;
            lContr.GetComponent<LineRenderer>().enabled = false;
            rContr.GetComponent<Brush>().enabled = false;
            rContr.GetComponent<LineRenderer>().enabled = false;
        }
    }
}
