using System.Collections;
using UnityEngine;
using UnityEditor;
public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }
    IEnumerator Init()
    {
        Debug.Log("chegou aqui?");
        board.Load(levelData);
        Debug.Log("Carregou?");
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        SelectTile(p);
        SpawnTestUnits(); //this is new
        yield return null;
        //owner.ChangeState<SelectUnitState> (); //this is changed
        owner.ChangeState<CutSceneState>(); //testing cutscenes xd
        owner.round = owner.gameObject.AddComponent<TurnOrderController>().Round();
        //owner.ChangeState<SelectUnitState>();
    }
    void SpawnTestUnits()
    {
        string[] jobs = new string[] {"Dragoon", "White Mage", "Knight" };
        for (int i = 0; i < jobs.Length; ++i)
        {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
            instance.name = jobs[i];
            Stats s = instance.AddComponent<Stats>();
            s[StatTypes.LVL] = 1;
            GameObject jobPrefab = Resources.Load<GameObject>("Jobs/" + jobs[i]);
            GameObject jobInstance = Instantiate(jobPrefab) as GameObject;
            jobInstance.transform.SetParent(instance.transform);
            Job job = jobInstance.GetComponent<Job>();
            job.Employ();
            job.LoadDefaultStats();
            Point p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].z);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();
            instance.AddComponent<WalkMovement>();
            units.Add(unit);
            //rank rank = instance.AddComponent<Rank>();
            //rank.Init (10);
        }
    }
}