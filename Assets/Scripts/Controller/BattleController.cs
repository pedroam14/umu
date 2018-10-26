using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    public GameObject heroPrefab;
    public Unit currentUnit;
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public IEnumerator round;
    public List<Unit> units = new List<Unit>();
    public Tile currentTile { get { return board.GetTile(pos); } }
    public StatPanelController statPanelController;
    
    void Start()
    {
        Debug.Log("chegou no start do battlecontroller?");
        ChangeState<InitBattleState>();

    }
}