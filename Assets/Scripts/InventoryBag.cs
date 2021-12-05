using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBag : MonoBehaviour
{
    [Header("Toolbox")]
    public bool brush;
    public bool bow;
    public bool arrow;
    public int arrowCount;

    [Header("Colors")]
    public bool blue;
    public bool green;
    public bool red;
    public bool brown;
    public bool yellow;
    public bool grey;

    // Start is called before the first frame update
    void Start()
    {
        brush = false;
        bow = false;
        arrow = false;
        arrowCount = 0;


        blue = false;
        green = false;
        red = false;
        brown = false;
        yellow = false;
        grey = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToInventory(Grabbable tool)
    {
        // TODO: if tool in bag is false and tool is equal to correct gametag set to true
        if (!brush)
        {
            if (tool.gameObject.CompareTag("Brush"))
            {
                brush = true;
            }
        }

        if (!bow)
        {
            if (tool.gameObject.CompareTag("Bow"))
            {
                bow = true;
            }
        }

        if (!arrow)
        {
            if (tool.gameObject.CompareTag("Arrow"))
            {
                arrow = true;
            }
        }
        else
        {
            arrowCount++;
        }
    }

    public Grabbable SwitchCurrentTool(Grabbable tool)
    {
        // TODO: Create a switch statement to set different tools
        // if in inventory through check inventory method, set tool position and return tool
        return tool;
    }
}
