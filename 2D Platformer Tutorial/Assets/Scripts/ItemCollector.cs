using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemCollector : MonoBehaviour
{
    public AudioSource collectSoundEffect;
    private int ApplesCollected = 0;
    [SerializeField] private Text AppleText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            collectSoundEffect.Play();
            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.SetTrigger("collected");
            StartCoroutine(Waiter((float) 0.25, collision));
            ApplesCollected++;
            AppleText.text = "Apples: " + ApplesCollected;
 

            anim.SetTrigger("collected");
        }
    }

    private IEnumerator Waiter(float waitTime, Collider2D collision)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(collision.gameObject);
    }

}
