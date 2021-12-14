using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bucket : MonoBehaviour
{
    public Quaternion bucketRot;
    public GameObject paint;
    bool switchScene = false;

    public Material whiteSky;
    public Material blueSky;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = whiteSky;
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

            if (switchScene)
            {
                StartCoroutine(SceneTransition());
            }
        }
        PaintSpill(false);
    }

    void PaintSpill(bool bucketTipped)
    {
        if (bucketTipped)
        {
            RenderSettings.skybox = blueSky;
            paint.transform.rotation = Quaternion.identity;
            paint.SetActive(true);
            paint.transform.parent = null;
            switchScene = true;
        }
    }

    public IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(3);
        print("scene");
        Load();
    }

    void Load()
    {
        SceneManager.LoadScene(1);
    }

}
