using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    BoxCollider2D coll;
    DialogueTrigger dialogueTrigger;
    bool triggerStay = false;
    public DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Submit") && triggerStay)
        {
            if (!dialogueManager.isDialogueOpened)
            {
                Debug.Log("triggeringConvowithNPC");
                dialogueTrigger.TriggerDialogue();
            }
            else if (Input.GetButtonDown("Submit"))
            {
                Debug.Log("Displaying next sentence");
                dialogueManager.DisplayNextSentence();
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerStay = true;
            //Debug.Log("Player has entered npc zone");
            //display text "press enter to talk"
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //remove text "press enter to talk"
        triggerStay = false;
    }
}
