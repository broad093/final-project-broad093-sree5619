using UnityEngine;

public class OnLiquidGrounded : MonoBehaviour
{

    [SerializeField] private Animator FluidAnimator;


    void Start()
    {
        if (FluidAnimator == null)
        {
            FluidAnimator = GetComponent<Animator>();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            FluidAnimator.SetBool("IsGrounded", true);
        }
        
        /* adjusted to add for case when collision is bucket so the blob doesn't do weird things
        if (col.gameObject.CompareTag("Bucket"))
        {
            col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        } */
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            FluidAnimator.SetBool("IsGrounded", false);
        }
    }
}
