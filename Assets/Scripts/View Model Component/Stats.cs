using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Stats : MonoBehaviour
{

    //reuse this logic regardless of which stat is changing
    public int this[StatTypes s]
    {
        get { return _data[(int)s]; }
        set { SetValue(s, value, true); }
    }
    int[] _data = new int[(int)StatTypes.Count];

    /*
    notifications for each stat are built dynamically and stored statically by the class
    this way both the listeners and the component itself can continually reuse a string instead of constantly needing to recreate it 
    */
    public static string WillChangeNotification(StatTypes type)
    {
        if (!_willChangeNotifications.ContainsKey(type))
        {
            _willChangeNotifications.Add(type, string.Format("Stats.{0}WillChange", type.ToString()));
        }
        return _willChangeNotifications[type];
    }
    public static string DidChangeNotification(StatTypes type)
    {
        if (!_didChangeNotifications.ContainsKey(type))
        {
            _didChangeNotifications.Add(type, string.Format("Stats.{0}DidChange", type.ToString()));
        }
        return _didChangeNotifications[type];
    }
    static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();
    static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

    //the setvalue will check whether or not there are any changes to the value, if not we just exit early
    //in case exceptions are allowed it creates a valuechangeexception and post it along with the will change notification
    //if the value does not chain, a new value is assigned in the array and a notification that the stat value changed is posted
    public void SetValue(StatTypes type, int value, bool allowExceptions)
    {
        int oldValue = this[type];
        if (oldValue == value)
        {
            return;
        }
        if (allowExceptions)
        {
            //allow exceptions to be the rule here
            ValueChangeException exc = new ValueChangeException(oldValue, value);

            //the notification is unique per stat type
            this.PostNotification(WillChangeNotification(type), exc);

            //did anything modify the value?
            value = Mathf.FloorToInt(exc.GetModifiedValue());

            //did something nullify the change?
            if (exc.toggle == false || value == oldValue)
            {
                return;
            }
        }
        _data[(int)type] = value;
        this.PostNotification(DidChangeNotification(type), oldValue);
    }
}