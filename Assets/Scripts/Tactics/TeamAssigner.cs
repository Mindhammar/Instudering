using UnityEngine;

public class TeamAssigner : MonoBehaviour
{
    private bool _team;  // 0 = Player 1    1 = Player 2
 [SerializeField] private UnitMoveManager unitMoveManager;
 [SerializeField] private MapManager mapManager;
   
   

   void Start()
   {
       if (unitMoveManager == null)
       {
           Debug.LogWarning("No UnitMoveManager assigned!");
       }

       if (mapManager == null)
       {
           Debug.LogWarning("No MapManager assigned!");
       }
       
       AssignTeams();

      
   }

   void AssignTeams()
   {
       foreach (var units in unitMoveManager.UnitPositions  )
       {
           if (!mapManager.TopTiles.ContainsKey(units.Key))
           {
               
           }
       }
   }


}
