using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerCombat : MonoBehaviour
{//add AttacRange/EnemyLayers/attackDamage/attackRate to PlayerData Scriptable Object
    public Transform Attackpoint;
    public Transform sidewaysAttackpoint;
    public Transform upAttackpoint;
    public Transform downAttackpoint;

    public float AttackRange = 0.5f;

    public LayerMask EnemyLayers;

    public int attackDamage = 20;

    public float attackRate = 2f;

    private float nextAttackTime = 0f;

    private int attackDirection;//attack directions: up = 1, down = -1, horizontal = 0

    [SerializeField] float attackTime = .1f;//set time attack sprite is active for

    private SpriteRenderer attackSpriteDefault;
    private SpriteRenderer attackSpriteUp;
    private SpriteRenderer attackSpriteDown;
    private SpriteRenderer attackSprite;

    PlayerMovementOriginal playerMovement;

    // Update is called once per frame
    private void Awake()
    {
        attackSpriteUp = GameObject.Find("AttackSpriteUp").GetComponent<SpriteRenderer>();
        attackSpriteDown = GameObject.Find("AttackSpriteDown").GetComponent<SpriteRenderer>();
        attackSpriteDefault = GameObject.Find("AttackSpriteDefault").GetComponent<SpriteRenderer>();
        playerMovement = gameObject.GetComponent<PlayerMovementOriginal>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)//if last attack wasn't too recent
        {
            if (Input.GetButtonDown("Fire1"))//check if attack button was pressed
            {
                GetAttackDirection();//check which direction player is facing and update attack direction variable
                Attack();//perform attack
                
                nextAttackTime = Time.time + 1f / attackRate;//reset attack timer
            }
        }
    }
        


    void Attack()//code to perform an attack
    {
        Debug.Log("attacking");
       Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(Attackpoint.position, 
           AttackRange, EnemyLayers);//cast a circle from AttackPoint with AttackRange radius
                                     //and check for collision with any enemies, which are added to a list
        StartCoroutine(ShowAttackSprite());//show and disable attack sprite in a coroutine
        foreach (Collider2D Enemy in HitEnemies)//iterate through all enemies that were hit by attack
       {
           Debug.Log("We Hit"+Enemy.name);
           Enemy.GetComponent<Enemy>().TakeDamage(attackDamage);//get enemy to take damage
       }
        if (HitEnemies.Length > 0)//recoil if an enemy was hit
        {
            playerMovement.Recoil(attackDirection);
        }
    }

    private void OnDrawGizmosSelected()//draw attack hitbox when gizmos is selected
    {
        if (Attackpoint == null)//dont do this if there is no attack point
            return;
        Gizmos.DrawWireSphere(Attackpoint.position, AttackRange);
    }
    IEnumerator ShowAttackSprite()
    {
        attackSprite.enabled = true; //show the attack sprite
        yield return new WaitForSeconds(attackTime);
        attackSprite.enabled = false; //disable the attack sprite
    }
    private void GetAttackDirection()
    {
        if (Input.GetAxisRaw("Vertical") > 0)//set to up if player pressing up
        {
            attackDirection = 1;
            Attackpoint = upAttackpoint;
            attackSprite = attackSpriteUp;
        }
        if (Input.GetAxisRaw("Vertical") < 0 && !playerMovement.IsGrounded())//set to down if player is not grounded and pressing down
        {
            attackDirection = -1;
            Attackpoint = downAttackpoint;
            attackSprite = attackSpriteDown;
        }
        else//set to player facing direction if not pressing up or down
        {
            attackDirection = 0;
            Attackpoint = sidewaysAttackpoint;
            attackSprite = attackSpriteDefault;
        }
    }
}