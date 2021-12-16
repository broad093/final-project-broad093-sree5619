using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    public Texture2D texture;
    public RenderTexture renderTex;
    public Vector2 size;
    public Material baseMaterial;

    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2(2048, 2048);
        var render = GetComponent<Renderer>();
        texture = new Texture2D((int)size.x, (int)size.y);

        //baseMaterial = render.material;
        
        //WORKS
        baseMaterial.SetTexture("_MainTex", texture);
        render.sharedMaterial = baseMaterial;
    }

}
