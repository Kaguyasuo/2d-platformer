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
            Destroy(collision.gameObject);
            ApplesCollected++;
            AppleText.text = "Apples: " + ApplesCollected;
        }
    }

}
