using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPromptTrigger : MonoBehaviour
{
    private TMP_Text interactionTextPrompt;
    private bool triggerStay = false;
    private BoxCollider2D trigger;

    void Start()
    {
        interactionTextPrompt = GetComponent<TMP_Text>();
        trigger = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerStay)
        {
            interactionTextPrompt.enabled = true;
        }
        else
        {
            interactionTextPrompt.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        triggerStay = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerStay = false;
    }
}
