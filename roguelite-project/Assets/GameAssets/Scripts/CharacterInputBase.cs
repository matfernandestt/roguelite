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
        if(input.GetButtonDown(JumpAction) && input.GetAxis(VerticalAction) >= 0)
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
