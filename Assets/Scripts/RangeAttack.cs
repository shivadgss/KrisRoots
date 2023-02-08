using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    private Transform player;
    public GameObject projectile;
    public float attackCoolDown = 2f;
    private float coolDownValue;
    public float forwardForce = 32f, upForce = 8f;
    public float distance;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        coolDownValue = attackCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        attackCoolDown -= Time.deltaTime;
        if (dist < distance && attackCoolDown <= 0)
        {
            transform.LookAt(player);
            Rigidbody rb = Instantiate(projectile, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
            rb.AddForce(transform.up * upForce, ForceMode.Impulse);
            attackCoolDown = coolDownValue;
        }
    }
}