using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float currentHealth;
    public GameObject mesh;
    public bool poisoned;
    public float poisonTime = 5f;
    public Text healthDisplay;
    public Image slider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(DestroyPlayer());
        }

        if (poisoned)
        {
            poisonTime -= Time.deltaTime;
            if (poisonTime <= 0)
            {
                poisoned = false;
            }
            else
            {
                currentHealth -= Time.deltaTime;
            }
        }

        healthDisplay.text = Mathf.RoundToInt(currentHealth).ToString();
        slider.fillAmount = currentHealth * 0.01f;
    }

    IEnumerator DestroyPlayer()
    {
        Destroy(mesh);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }
}
