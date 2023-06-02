using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Unity UI library


public class ItemCollector : MonoBehaviour
{
    // initialised melons with 0
    private int melons = 0;

    // a reference to melonsText
    [SerializeField] private Text melonsText;

    [SerializeField] private AudioSource collectionSoundEffect;

    // overwrite OnTriggerEnter2D in Unity - passing collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melon"))
        {
            collectionSoundEffect.Play();

            // remove collectible/melon from the screen
            Destroy(collision.gameObject);

            // increment melons
            melons++;

            /*Debug.Log("Melons: " + melons);*/
            // display counter to screen
            melonsText.text = "Melons: " + melons;
        }
    }
}
