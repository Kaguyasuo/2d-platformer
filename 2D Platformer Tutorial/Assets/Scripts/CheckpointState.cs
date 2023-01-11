using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointState : MonoBehaviour
{
    [SerializeField] private Animator checkpointAnimator;
    [SerializeField] private AudioSource winSound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Player")
        {
            if (!checkpointAnimator.GetBool("win"))
            {
                winSound.Play();
            }
            checkpointAnimator.SetBool("win", true);
            StartCoroutine(Waiter(4));
        }
    }

    private IEnumerator Waiter(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        CompleteLevel();
    }
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}

  
