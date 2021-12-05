using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTools : InventoryBag
{
    [Header("Currently Equipped Tool")]
    private Grabbable currentTool;
    GameObject thisTool;

    private bool inInventory;
    private bool equipped;

    // Start is called before the first frame update
    void Start()
    {
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
}
