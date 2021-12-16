using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    Brush brush;
    public Material newColor;
    public GameObject brushTip;

    // Start is called before the first frame update
    void Start()
    {
        newColor = brushTip.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("blue"))
        {
            brushTip.GetComponent<Renderer>().material = col.gameObject.GetComponent<Renderer>().material;
            newColor = col.gameObject.GetComponent<Renderer>().material;
            print("collision");
        }
        print(newColor);
<<<<<<< Updated upstream
        brush.SetBrushColor(brushTip.GetComponent<Renderer>());
=======
        //brush.SetBrushColor(brushTip.GetComponent<Renderer>());
>>>>>>> Stashed changes
    }
}
