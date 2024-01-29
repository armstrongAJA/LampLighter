using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [Header("Lamp Stuff")]
    public LampData lastLamp;
    public float spawnWaitTime = 2f;
    public bool isExitingScene = false;

    [Header("Player Health")]
    public int MaxHealth = 100;
    public int CurrentHealth;
    public bool dead = false;
    public bool respawning = false;


    [Header("Player Movement Stats")]
    public float maxSpeed = 8f;
    public float jumpSpeed = 10f;
    public float wallJumpSpeedx = 20f;
    public float wallJumpSpeedy = 14f;
    public float wallSlideSpeed = 4f;
    public float WallJumpTime = .5f;
    public float wallcheckDistance = .1f;
    public float coyoteTime = .1f;
    public float maxFallSpeed = 18f;
    public float trampolineJumpSpeed = 20f;
    public float groundCheckDistance = .2f;
    public float jumpDisabledWindowTime = .3f;

    [Header("Player Combat stuff")]
    public float downRecoilSpeed = 10f;
    public float horRecoilSpeed = 10f;
    public float recoilHorLerpAmount = 0.5f;
    public float recoilTime = .5f;
    public float attackRange = .5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    public float attackTime = .1f;

    [Header("Gravity")]
    public float normalGravity = 3f;
    public float fallGravityMult = 1.9f;

    [Header("Unlocked Movement Abilities")]
    public bool wallJumpActive = false;
    public bool doubleJumpActive = false;
    public bool dashActive = false;



    [Header("Layer Masks")]
    public LayerMask EnemyLayers;
    public LayerMask jumpableGround;
    public LayerMask jumpableWall;

    public void ResetStatsOnSpawn()
    {
        CurrentHealth = MaxHealth;
        dead = false;
        respawning = false;
        return;
    }

}
