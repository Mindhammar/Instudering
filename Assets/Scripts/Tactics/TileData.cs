using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;

    public bool canWalk;
    public bool[] hasModifier;

    public float modifierRange;
    public float modifierEvasion;
    public float modifierAccuracy;
    public float modifierMovement;
}

