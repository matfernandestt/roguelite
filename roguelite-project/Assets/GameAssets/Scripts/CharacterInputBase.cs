using System;
using Rewired;
using UnityEngine;

public class CharacterInputBase : MonoBehaviour
{
    private const string HorizontalAction = "Horizontal";
    private const string VerticalAction = "Vertical";
    private const string JumpAction = "Jump";
    private const string RunAction = "Run";
    
    private Player input;

    protected Action<float> horizontalInputEvent;
    protected Action<float> verticalInputEvent;
    protected Action jumpInputEvent;
    protected Action runBeginInputEvent;
    protected Action runEndInputEvent;
    protected Action dropDownInputEvent;

    protected bool isGrounded;

    [SerializeField] private BoxCollider2D mainCollider;
    [SerializeField] private LayerMask groundMask;

    protected virtual void Awake()
    {
        input = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        AxisInputs();
        JumpEvent();
        RunEvent();
        DropDown();
    }

    private void AxisInputs()
    {
        horizontalInputEvent?.Invoke(input.GetAxisRaw(HorizontalAction));
        verticalInputEvent?.Invoke(input.GetAxisRaw(VerticalAction));
    }

    private void JumpEvent()
    {
        var hit1 = Physics2D.Raycast(mainCollider.bounds.center + new Vector3(mainCollider.bounds.extents.x, 0), Vector2.down, mainCollider.bounds.extents.y + .1f, groundMask);
        var hit2 = Physics2D.Raycast(mainCollider.bounds.center - new Vector3(mainCollider.bounds.extents.x, 0), Vector2.down, mainCollider.bounds.extents.y + .1f, groundMask);
        var c1 = hit1.collider != null ? Color.green : Color.red;
        var c2 = hit2.collider != null ? Color.green : Color.red;
        Debug.DrawRay(mainCollider.bounds.center + new Vector3(mainCollider.bounds.extents.x, 0), Vector2.down * (mainCollider.bounds.extents.y + .1f), c1);
        Debug.DrawRay(mainCollider.bounds.center - new Vector3(mainCollider.bounds.extents.x, 0), Vector2.down * (mainCollider.bounds.extents.y + .1f), c2);
        isGrounded = hit1.collider != null || hit2.collider != null;
        
        if(input.GetButtonDown(JumpAction) && input.GetAxis(VerticalAction) >= 0 && isGrounded)
            jumpInputEvent?.Invoke();
    }

    private void RunEvent()
    {
        if (input.GetButtonDown(RunAction))
            runBeginInputEvent?.Invoke();
        if(input.GetButtonUp(RunAction))
            runEndInputEvent?.Invoke();
    }

    private void DropDown()
    {
        if (input.GetAxisRaw(VerticalAction) < 0 && input.GetButtonDown(JumpAction))
            dropDownInputEvent?.Invoke();
    }
}
