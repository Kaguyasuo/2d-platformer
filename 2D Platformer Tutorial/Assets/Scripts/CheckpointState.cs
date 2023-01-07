using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        }
    }
}
  
