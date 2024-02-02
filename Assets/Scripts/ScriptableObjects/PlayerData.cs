using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int MaxLevel = 1;

    [Header("Conserving Momentum")]
    public bool doConserveMomentum = false;

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
    public float runMaxSpeed = 8f;
    public float runAccelAmount = 0.2f;
    public float runDeccelAmount = 0.2f;
    public float maxFallSpeed = 20f;
    public bool isFacingRight;

    [Header("Jumping")]
    public float jumpForce = 10f;
    public float jumpHangTimeThreshold = 0.1f;
    public float jumpInputBufferTime = 0.1f;
    public float accelInAir = 0.1f;
    public float deccelInAir = 0.1f;
    public float jumpHangAccelerationMult = 0.1f;
    public float jumpHangMaxSpeedMult = 1f;

    [Header("Wall Jump")]
    public float wallJumpForceX = 20f;
    public float wallJumpForceY = 14f;
    public float wallSlideSpeed = 4f;
    public float wallJumpTime = .5f;
    public float wallcheckDistance = .1f;
    public float wallJumpRunLerp = .2f;

    [Header("Sliding")]
    public float slideSpeed = .5f;
    public float slideAccel = .5f;

    [Space]
    public float coyoteTime = .1f;
    public float maxFastFallSpeed = 18f;

    public float trampolineJumpSpeed = 20f;
    public float groundCheckDistance = .2f;
    public float jumpDisabledWindowTime = .3f;

    [Header("Player Combat stuff")]
    public float attackRange = .5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    public float attackTime = .1f;

    [Header("Recoiling stuff")]
    public bool isRecoilingVert = false;
    public bool isRecoilingHor = false;
    public float downRecoilSpeed = 10f;
    public float horRecoilSpeed = 10f;
    public float recoilHorLerpAmount = 0.5f;
    public float recoilTime = .5f;

    [Header("Gravity")]
    public float gravityScale = 3f;
    public float fallGravityMult = 1.9f;
    public float fastFallGravityMult = 1.9f;
    public float jumpCutGravityMult = 1.9f;
    public float jumpHangGravityMult = 0.5f;

    [Header("Unlocked Movement Abilities")]
    public bool wallJumpActive = false;
    public bool doubleJumpActive = false;
    public bool dashActive = false;

    [Header("Spawning Stuff")]
    public bool isMovingEnabled = true;
    public bool isSceneTransitionActive = false;
    public float sceneTransitionMovementDirectionHorizontal;
    public float sceneTransitionMovementDirectionVertical;


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
