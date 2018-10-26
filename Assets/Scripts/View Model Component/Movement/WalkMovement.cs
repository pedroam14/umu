using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class WalkMovement : Movement
{
    protected override bool ExpandSearch(Tile from, Tile to)
    {
        //skip if the distance in height between the two tiles is more than the unit can jump
        if ((Mathf.Abs(from.height - to.height) > jumpHeight))
        {
            return false;
        }
        //skip if the tile is occupied by an enemy
        if (to.content != null)
        {
            return false;
        }
        return base.ExpandSearch(from, to);
    }
    public override IEnumerator Traverse(Tile tile)
    {
        this.unit.Place(tile);
        //build a list of way points from the unit's 
        //starting tile to the destination tile
        List<Tile> targets = new List<Tile>();
        while (tile != null)
        {
            targets.Insert(0, tile);
            tile = tile.prev;
        }
        //move to each way point in succession
        for (int i = 1; i < targets.Count; ++i)
        {
            Tile from = targets[i - 1];
            Tile to = targets[i];
            Directions dir = from.GetDirection(to);
            Debug.Log("Direção atual: " + dir + " de numero: " + (int)dir);
            animator.SetInteger("Direction", (int)dir);
            if ((int)dir == 1 || (int)dir == 2)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            //try to solve this with animation states instead cause it's really not good (MOSTLY SOLVED)
            if (from.height == to.height)
            {
                yield return StartCoroutine(Walk(to));
            }
            else
            {
                yield return StartCoroutine(Jump(to));
            }
        }
        yield return null;
    }
    IEnumerator Walk(Tile target)
    {
        Tweener tweener = this.transform.MoveTo(target.center, 0.5f, EasingEquations.Linear);
        while (tweener != null)
        {
            yield return null;
        }
    }
    IEnumerator Jump(Tile to)
    {
        Tweener tweener = this.transform.MoveTo(to.center, 0.5f, EasingEquations.Linear);
        Tweener t2 = this.jumper.MoveToLocal(new Vector3(0, Tile.stepHeight * 0.8f, 0), tweener.easingControl.duration / 2f, EasingEquations.EaseOutQuad);
        t2.easingControl.loopCount = 1;
        t2.easingControl.loopType = EasingControl.LoopType.PingPong;
        while (tweener != null)
        {
            yield return null;
        }
    }
}