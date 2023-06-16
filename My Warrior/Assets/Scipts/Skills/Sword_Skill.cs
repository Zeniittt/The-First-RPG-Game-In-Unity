using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill Information")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchDirection;
    [SerializeField] private float swordGravity;
}
