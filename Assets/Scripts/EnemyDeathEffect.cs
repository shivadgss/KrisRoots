using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    public GameObject gas;
    private Transform player;
    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void OnDestroy()
    {
        Instantiate(gas, transform.position, transform.rotation);
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist < 3.25f)
        {
            if (!playerHealth.poisoned)
            {
                playerHealth.poisoned = true;
                playerHealth.poisonTime = 5;
            }
            else
                playerHealth.poisonTime = 5;
        }
    }

}
