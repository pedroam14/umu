using UnityEngine;
using System.Collections;
public abstract class ValueModifier : Modifier
{
    // base abstract class for a value modifier 
    public ValueModifier(int sortOrder) : base(sortOrder) { }
    public abstract float Modify(float value);
}