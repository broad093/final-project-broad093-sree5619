using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class InventoryTools : InventoryBag
{
    private Grabbable currentTool;
    GameObject thisTool;

    public Brush lContrBrush;
    public Brush rContrBrush;

    private bool inInventory;
    private bool equipped;

    // Start is called before the first frame update
    void Start()
    {

        HoldingBrush();
        inInventory = true;
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
        GameObject rContr = GameObject.Find("RightHand Controller");
        GameObject lContr = GameObject.Find("LeftHand Controller");
        GameObject brush = GameObject.FindGameObjectWithTag("Brush");

        lContrBrush.paintAction.action.performed -= lContrBrush.Paint;
        lContrBrush.paintAction.action.canceled -= lContrBrush.NoPaint;
        lContrBrush.primaryTouch.action.performed -= lContrBrush.TouchDown;
        lContrBrush.primaryTouch.action.canceled -= lContrBrush.TouchUp;

        rContrBrush.paintAction.action.performed -= rContrBrush.Paint;
        rContrBrush.paintAction.action.canceled -= rContrBrush.NoPaint;
        rContrBrush.primaryTouch.action.performed -= rContrBrush.TouchDown;
        rContrBrush.primaryTouch.action.canceled -= rContrBrush.TouchUp;

        if (brush.transform.IsChildOf(rContr.transform) || brush.transform.IsChildOf(lContr.transform))
        {
            SetCurrentTool(brush.GetComponent<Grabbable>());
        }

        if (thisTool.transform.IsChildOf(rContr.transform))
        {
            rContrBrush.paintAction.action.performed += rContrBrush.Paint;
            rContrBrush.paintAction.action.canceled += rContrBrush.NoPaint;
            rContrBrush.primaryTouch.action.performed += rContrBrush.TouchDown;
            rContrBrush.primaryTouch.action.canceled += rContrBrush.TouchUp;
            rContrBrush.paintAction.action.Enable();

            lContrBrush.paintAction.action.performed -= lContrBrush.Paint;
            lContrBrush.paintAction.action.canceled -= lContrBrush.NoPaint;
            lContrBrush.primaryTouch.action.performed -= lContrBrush.TouchDown;
            lContrBrush.primaryTouch.action.canceled -= lContrBrush.TouchUp;
            lContrBrush.paintAction.action.Disable();
        }
        else if (thisTool.transform.IsChildOf(lContr.transform))
        {
            rContrBrush.paintAction.action.performed -= rContrBrush.Paint;
            rContrBrush.paintAction.action.canceled -= rContrBrush.NoPaint;
            rContrBrush.primaryTouch.action.performed -= rContrBrush.TouchDown;
            rContrBrush.primaryTouch.action.canceled -= rContrBrush.TouchUp;
            rContrBrush.paintAction.action.Disable();

            lContrBrush.paintAction.action.performed += lContrBrush.Paint;
            lContrBrush.paintAction.action.canceled += lContrBrush.NoPaint;
            lContrBrush.primaryTouch.action.performed += lContrBrush.TouchDown;
            lContrBrush.primaryTouch.action.canceled += lContrBrush.TouchUp;
            rContrBrush.paintAction.action.Enable();
        }
    }
}
