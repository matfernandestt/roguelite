using UnityEngine;

public class SwordCharacterBase : CharacterInputBase
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    
    [Header("Parameters")]
    [SerializeField] private CharacterBasicParameters parameters;
    
    protected override void Awake()
    {
        base.Awake();

        horizontalInputEvent += HorizontalInput;
        verticalInputEvent += VerticalInput;
        jumpInputEvent += JumpInput;
    }

    private void HorizontalInput(float axis)
    {
        rb.velocity = new Vector2(axis * parameters.walkSpeed, rb.velocity.y);
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
