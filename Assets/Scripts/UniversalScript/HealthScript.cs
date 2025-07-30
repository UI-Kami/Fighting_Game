using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HealthScript : MonoBehaviour
{
    public float health = 100f;

    private CharacterAnimation animationScript;
    private EnemyMovement em;
    private bool characterDied;

    public bool is_Player;

    public bool isDead => characterDied;

    private HealthUI health_UI;

    void Awake()
    {
        animationScript = GetComponentInChildren<CharacterAnimation>();

        if (is_Player)
            health_UI = GetComponent<HealthUI>();
    }

    public void ApplyDamage(float damage, bool knockDown)
    {
        if (characterDied)
            return;

        health -= damage;

        // Health UI only for player
        if (is_Player && health_UI != null)
        {
            health_UI.DisplayHealth(health);
        }

        if (health <= 0f)
        {
            characterDied = true;

            if (animationScript != null)
                animationScript.Death();

            // Disable movement on death
            if (is_Player)
            {
                var playerMovement = GetComponent<PlayerMovement>();
                if (playerMovement != null)
                    playerMovement.enabled = false;

                // Optionally disable attack or input scripts too
                var inputScript = GetComponent<PlayerAttack>();
                if (inputScript != null)
                    inputScript.enabled = false;

                StartCoroutine(RestartGame());

            }
            else // Enemy
            {
                em = GetComponent<EnemyMovement>();
                if (em != null)
                    em.enabled = false;
            }

            return;
        }

        // Enemy gets hit animations
        if (!is_Player && animationScript != null)
        {
            if (knockDown)
            {
                if (Random.Range(0, 2) > 0)
                    animationScript.KnockDown();
            }
            else
            {
                if (Random.Range(0, 3) > 1)
                    animationScript.Hit();
            }
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3f); // Let death animation play
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}






// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class HealthScript : MonoBehaviour
// {
//     public float health = 100f;

//     private CharacterAnimation animationScript;
//     private EnemyMovement em;

//     private bool characterDied;

//     public bool is_Player;

//     public bool isDead => characterDied;
//     private HealthUI health_UI;

//     void Awake()
//     {
//         if (animationScript == null)
//         {
//             animationScript = GetComponentInChildren<CharacterAnimation>();
//         }
//         if(is_Player)
//             health_UI = GetComponent<HealthUI>();
//     }

//     public void ApplyDamage(float damage, bool knockDown)
//     {
//         if (characterDied)
//             return;
//         health -= damage;

//         //Health-UI
//         health_UI.DisplayHealth(health);
//         if (health <= 0f)
//         {
//             animationScript.Death();
//             characterDied = true;
            
//             //if is player deactivate enemy script.
//             if (is_Player)
//             {

//             }
//             return;

//         }
//         if (!is_Player)
//         {
//             if (knockDown)
//             {
//                 if (Random.Range(0, 2) > 0)
//                 {
//                     animationScript.KnockDown();
//                 }
//             }
//             else
//             {
//                 if (Random.Range(0, 3) > 1)
//                 {
//                     animationScript.Hit();
//                 }
//             }
//         }//if is player

//     }// damage Apply








// }//class
