using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInGameData : MonoBehaviour
{
    [SerializeField]  private UnitBaseData unitBaseData;
    
    
    public float currentActionPoints;
    
    private bool hasAttackedThisTurn;
    private int isStunned; // 0= false 1=true for one turn  2 = true for two turns etc.
    
}
