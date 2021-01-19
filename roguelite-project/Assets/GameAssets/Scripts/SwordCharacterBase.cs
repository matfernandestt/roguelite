using System;
using System.Collections;
using UnityEngine;

public class SwordCharacterBase : CharacterInputBase
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CharacterCollisionBase charCol;

    
    [Header("Parameters")]
    [SerializeField] private CharacterBasicParameters parameters;

    private float characterMovementSpeed;
    
    protected override void Awake()
    {
        base.Awake();

        horizontalInputEvent += HorizontalInput;
        verticalInputEvent += VerticalInput;
        
        jumpInputEvent += JumpInput;
        runBeginInputEvent += RunBeginInput;
        runEndInputEvent += RunEndInput;
        dropDownInputEvent += DropDownInput;

        characterMovementSpeed = parameters.walkSpeed;
    }

    private void OnDestroy()
    {
        horizontalInputEvent -= HorizontalInput;
        verticalInputEvent -= VerticalInput;
        
        jumpInputEvent -= JumpInput;
        runBeginInputEvent -= RunBeginInput;
        runEndInputEvent -= RunEndInput;
        dropDownInputEvent -= DropDownInput;
    }

    private void HorizontalInput(float axis)
    {
        rb.velocity = new Vector2(axis * characterMovementSpeed, rb.velocity.y);
        FlipCharacter(axis);
    }

    private void VerticalInput(float axis)
    {
        
    }

    private void JumpInput()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        
        rb.AddForce(Vector2.up * parameters.jumpForce);
    }

    private void RunBeginInput()
    {
        characterMovementSpeed = parameters.runSpeed;
    }

    private void DropDownInput()
    {
        if (charCol.overPlatform)
        {
            charCol.platformConnected.OneWayDown();
            StartCoroutine(ReenablePlatform());
        }

        IEnumerator ReenablePlatform()
        {
            yield return new WaitForSeconds(.4f);
            charCol.platformConnected.OneWayUp();
        }
    }

    private void RunEndInput()
    {
        characterMovementSpeed = parameters.walkSpeed;
    }

    #region ETC
    private void FlipCharacter(float direction)
    {
        if (direction > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        if (direction < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
    #endregion
}