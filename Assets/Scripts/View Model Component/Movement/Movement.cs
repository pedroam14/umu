using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public abstract class Movement : MonoBehaviour
{
    public int range { get { return stats[StatTypes.MOV]; } }
    public int jumpHeight { get { return stats[StatTypes.JMP]; } }
    protected Stats stats;
    protected Unit unit;
    protected Transform jumper;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    protected virtual void Start()
    {
        stats = GetComponent<Stats>();
    }
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        jumper = transform.Find("Jumper"); //if that doesn't work it's because that's obsolete and you should use "Find" instead
        animator = GetComponentInChildren<Animator>();
    }
    public virtual List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(unit.tile, ExpandSearch);
        Filter(retValue);
        return retValue;
    }
    protected virtual void Filter(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            if (tiles[i].content != null)
            {
                tiles.RemoveAt(i);
            }
        }
    }
    protected virtual bool ExpandSearch(Tile from, Tile to)
    {
        return (from.distance + 1) <= range;
    }
    public abstract IEnumerator Traverse(Tile tile);
    protected virtual IEnumerator Turn(Directions dir)
    {
        unit.dir = dir;

        yield return null;
    }
}