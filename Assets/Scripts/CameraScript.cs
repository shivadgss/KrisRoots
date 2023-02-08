using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Transform target;
    public Vector3 offset;
    private Animator anim;
    public Camera mCamera;

    private void Start()
    {
        anim = GetComponent < Animator>();
    }

    void FixedUpdate()
    {
        Vector3 position = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
        transform.position = smoothPosition;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.Play("fov",-1,0f);
        }
    }
}

