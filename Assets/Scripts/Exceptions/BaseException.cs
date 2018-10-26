using UnityEngine;
using System.Collections;
public class BaseException{
    //very basic level exception to handle the complex interactions necessary in an RPG


    public bool toggle {get; private set;}
    private bool defaultToggle;

    public BaseException(bool defaultToggle){
        this.defaultToggle = defaultToggle;
        toggle = defaultToggle;
    }
    public void FlipToggle(){
        toggle = !defaultToggle;
    }
}