using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemCollector : MonoBehaviour
{
    //declare variables
    public int cherries = 0;//declare this outside the trigger so it doesn't update to zero constantly
    private TMP_Text CherriesText;
    [SerializeField] private AudioSource collectionSoundEffect;
    private void Start()
    {
        CherriesText = GameObject.Find("CherriesText").GetComponent<TMP_Text>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)//Check if there's been a collision
    {
        if (collision.gameObject.CompareTag("Cherry"))//check if collision is with a cherry
        {
            
            collectionSoundEffect.Play();//play collect item sound effect
            Destroy(collision.gameObject);//Destroy collectible item
            cherries++;//increase number of cherries collected by 1
            CherriesText.text = cherries + "/10";//Update text in HUD for number of cherries collected in level
            Debug.Log("Cherries:" + cherries);
        }
    }
}
