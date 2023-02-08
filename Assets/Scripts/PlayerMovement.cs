using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private DialougeUI dialougeUI;
    public DialougeUI DialougeUI => dialougeUI;
    public Iinteractible Interactible { get; set; }
    private Vector3 moveInput;
    public float speed;
    private Vector3 moveVelocity;
    private Rigidbody rb;
    private Camera mainCamera;

    public Transform projectileSpawnPoint;
    public GameObject projectile;

    public float timeBetweenShots;
    [SerializeField]
    private float shotCounter;
    public float dashTime;
    public float dashSpeed;
    public float trailWait;
    public float wait = 1.5f;

    public bool movementEnable = true;
    public TimeTravel time;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y, Input.GetAxisRaw("Vertical") * speed);
        moveVelocity = moveInput;

        LookAtMouse();

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            shotCounter = 0;
        }
        wait -= Time.deltaTime;
        
        

        if (Input.GetKeyDown(KeyCode.Space) && wait<= 0f)
        {
            StartCoroutine(Dash());
            wait = 1.5f;
        }
        if (dialougeUI.Isopen) return;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Dialog")
        {           
           Interactible?.Interact(this);
           movementEnable = false;
           rb.velocity = new Vector3(0, 0, -2);
            other.gameObject.SetActive(false);
            
        }   

        if(other.gameObject.tag == "Infinite Past")
        {
            time.timeLeft = 100000;
        }
    }
    IEnumerator Dash()
    {
        float time = Time.time;
        float mainSpeed = speed;
        while (Time.time < time + dashTime)
        {
            speed = dashSpeed;
            yield return null;
            gameObject.GetComponent<TrailRenderer>().enabled = true;
        }
        speed = mainSpeed;
        yield return new WaitForSeconds(trailWait);
        gameObject.GetComponent<TrailRenderer>().enabled = false;
    }

    void LookAtMouse()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundplane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundplane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    void FixedUpdate()
    {
        if (movementEnable)
        {
            rb.velocity = moveVelocity;
        }
    }
}
