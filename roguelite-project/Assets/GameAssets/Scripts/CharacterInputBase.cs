using System;
using Rewired;
using UnityEngine;

public class CharacterInputBase : MonoBehaviour
{
    private const string HorizontalAction = "Horizontal";
    private const string VerticalAction = "Vertical";
    private const string JumpAction = "Jump";
    
    private Player input;

    protected Action<float> horizontalInputEvent;
    protected Action<float> verticalInputEvent;
    protected Action jumpInputEvent;

    protected virtual void Awake()
    {
        input = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        AxisInputs();
        JumpEvent();
    }

    private void AxisInputs()
    {
        horizontalInputEvent?.Invoke(input.GetAxisRaw(HorizontalAction));
        verticalInputEvent?.Invoke(input.GetAxisRaw(VerticalAction));
    }

    private void JumpEvent()
    {
        if(input.GetButtonDown(JumpAction))
            jumpInputEvent?.Invoke();
    }
}
