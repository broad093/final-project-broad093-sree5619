using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Brush : MonoBehaviour
{
    public GameObject brushTip;

    public LineRenderer laserPointer;
    public Material paintableMaterial;
    public Material rangeMaterial;
    Material laserMaterial;

    public int brushSize = 75;

    Vector2 brushPos, lastBrushPos;
    Paintable paintable;

    public InputActionProperty paintAction;
    public InputActionProperty primaryTouch;

    Renderer brushcolor;
    Color[] paint;
    bool lastPaintedFrame;

    public bool buttonpress;

    // Start is called before the first frame update
    void Start()
    {
        buttonpress = false;

        paintAction.action.performed += Paint;
        paintAction.action.canceled += NoPaint;

        primaryTouch.action.performed += TouchDown;
        primaryTouch.action.canceled += TouchUp;

        laserMaterial = laserPointer.material;

        SetBrushColor(brushTip.GetComponent<Renderer>());
    }

    private void OnDestroy()
    {
        paintAction.action.performed -= Paint;
        paintAction.action.canceled -= NoPaint;
        primaryTouch.action.performed -= TouchDown;
        primaryTouch.action.canceled -= TouchUp;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            laserPointer.SetPosition(1, new Vector3(0, 0, hit.distance));

            if (hit.collider.GetComponent<Paintable>())
            {
                laserPointer.material = paintableMaterial;
            }
            else
            {
                laserPointer.material = laserMaterial;
            }
        }

        if (paintAction.action.triggered && paintAction.action.enabled)
        {
            buttonpress = !buttonpress;
        }
        if (buttonpress)
        {
            Paint(new InputAction.CallbackContext());
        }
    }

    public void Paint(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5))
        {
            laserPointer.SetPosition(1, new Vector3(0, 0, hit.distance));
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;
            if (hit.transform.CompareTag("Paintable"))
            {
                laserPointer.material = rangeMaterial;
                if (paintable == null)
                {
                    paintable = hit.collider.GetComponent<Paintable>();
                }

                //RenderTexture.active = paintable.renderTex;
                brushPos = new Vector2(hit.textureCoord.x, hit.textureCoord.y);

                int x = (int)(brushPos.x * paintable.size.x - (brushSize / 2));
                int y = (int)(brushPos.y * paintable.size.y - (brushSize / 2));

                //out of bounds check for paintable object
                if (x < 0 || x > paintable.size.x || y < 0 || y > paintable.size.y)
                {
                    return;
                }

                if (lastPaintedFrame)
                {
                    paintable.texture.SetPixels(x, y, brushSize, brushSize, paint);

                    for (float f = 0.01f; f < 1.00; f += 0.01f)
                    {
                        int fillX = (int)Mathf.Lerp(lastBrushPos.x, x, f);
                        int fillY = (int)Mathf.Lerp(lastBrushPos.y, y, f);
                        paintable.texture.SetPixels(fillX, fillY, brushSize, brushSize, paint);
                    }
 
                    paintable.texture.Apply();
                    paintable.renderTex = null;
                }

                lastBrushPos = new Vector2(x, y);
                lastPaintedFrame = true;
                return;
            }
        }

        paintable = null;
        lastPaintedFrame = false;
    }


    public void NoPaint(InputAction.CallbackContext context)
    {
        buttonpress = false;
    }

    public void TouchDown(InputAction.CallbackContext context)
    {
        laserPointer.enabled = true;
    }

    public void TouchUp(InputAction.CallbackContext context)
    {
        laserPointer.enabled = false;
        laserPointer.material = laserMaterial;
        paintable = null;
        lastPaintedFrame = false;
    }

    public Renderer GetBrushColor()
    {
        return brushcolor;
    }

    public void SetBrushColor(Renderer color)
    {
        brushcolor = color;
        paint = Enumerable.Repeat(brushcolor.material.color, brushSize * brushSize).ToArray();
        print("brush color set");
    }

}
