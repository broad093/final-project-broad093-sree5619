using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public Quaternion bucketRot;
    public GameObject paint;

    // Start is called before the first frame update
    void Start()
    {
        bucketRot = Quaternion.identity;
        paint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bucketRot = this.transform.rotation;

        if (bucketRot.x < -0.5f || bucketRot.z < -0.5f || bucketRot.x > 0.5f || bucketRot.z > 0.5f)
        {
            PaintSpill(true);
        }

        PaintSpill(false);
    }

    void PaintSpill(bool bucketTipped)
    {
        if (bucketTipped)
        {
            paint.transform.rotation = Quaternion.identity;
            paint.SetActive(true);
            paint.transform.parent = null;

            SceneTransition();
        }
    }

    public IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(3);
    }

}
