using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
    public float playerHealth = 3f;
    private PlayerCombat playerCombat;
    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }

    public void loseHealth(float dmg)
    {
        if (playerCombat.immune)
            return;

        playerHealth -= dmg;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
        }

        if (healthBar.transform.childCount > 0)
        {
            GameObject heart = healthBar.transform.GetChild(healthBar.transform.childCount - 1).gameObject;
            Destroy(heart);
        }

    }
}

