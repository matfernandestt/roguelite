using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBasicParameters", menuName = "Parameters/CharacterBase")]
public class CharacterBasicParameters : ScriptableObject
{
    public float walkSpeed;
    public float jumpForce;
}
