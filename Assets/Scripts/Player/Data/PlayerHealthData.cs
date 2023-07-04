using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealthData", menuName = "Data/Player Data/Health Data")]
public class PlayerHealthData : ScriptableObject
{
    [SerializeField] private float _maxHealth;

    public float MaxHealth => _maxHealth;
}
