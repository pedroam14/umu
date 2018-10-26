using UnityEngine;
using System.Collections;
public abstract class Feature : MonoBehaviour
{

    //the main use of an item is to modify something
    //this script is meant to describe the ability to modify something as a feature of an item
    protected GameObject _target { get; private set; }



    /*
    this class will support 2 main cases:
    1 - a feature can be activated and deactivated in different times (such as the case with equipment), the Activate and Deactivate methods will handle this use case
    2 - a feature can have a one-shot and permanent application (such as a health potion), the Apply method will handle this one
    */
    public void Activate(GameObject target)
    {
        if (_target == null)
        {
            _target = target;
            OnApply();
        }
    }
    public void Deactivate()
    {
        if (_target != null)
        {
            OnRemove();
            _target = null;
        }
    }
    public void Apply(GameObject target)
    {
        _target = target;
        OnApply();
        _target = null;
    }
    protected abstract void OnApply();
    protected virtual void OnRemove() { }
}
