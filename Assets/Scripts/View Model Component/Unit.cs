using System.Collections;
using UnityEngine;
public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;
    public void Place(Tile target)
    {
        // Make sure old tile location is not still pointing to this unit
        if (this.tile != null && this.tile.content == gameObject)
        {
            this.tile.content = null;
        }

        // Link unit and tile references
        this.tile = target;

        if (target != null)
        {
            target.content = gameObject;
        }
    }
    public void Match()
    {
        this.transform.localPosition = this.tile.center;
        //transform.localEulerAngles = dir.ToEuler ();
    }
}