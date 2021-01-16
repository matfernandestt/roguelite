using System;
using UnityEngine;

public class CharacterCollisionBase : MonoBehaviour
{
    public bool overPlatform { get; set; }
    public OneWayPlatform platformConnected { get; private set; }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var oneWayPlatform = other.gameObject.GetComponent<OneWayPlatform>();
        if (oneWayPlatform != null)
        {
            platformConnected = oneWayPlatform;
            overPlatform = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        var oneWayPlatform = other.gameObject.GetComponent<OneWayPlatform>();
        if (oneWayPlatform != null)
        {
            overPlatform = false;
        }
    }
}
