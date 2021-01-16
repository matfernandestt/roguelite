using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private PlatformEffector2D effector;

    public void OneWayUp()
    {
        effector.rotationalOffset = 0f;
    }
    
    public void OneWayDown()
    {
        effector.rotationalOffset = 180f;
    }
}
