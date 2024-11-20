using UnityEngine;

public class UnitInGameData : MonoBehaviour
{
    [SerializeField]  private UnitBaseData unitBaseData;

    [SerializeField] public bool isTeam2;
    [SerializeField] public float currentActionPoints;
    
    [SerializeField] private bool hasAttackedThisTurn;
    [SerializeField] private int isStunned; // 0= false 1=true for one turn  2 = true for two turns etc.
    
}
