using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class AbilityMenuPanelController : MonoBehaviour
{
    const string ShowKey = "Show";
    const string HideKey = "Hide";
    const string EntryPoolKey = "AbilityMenuPanel.Entry";
    const int MenuCount = 4;
    [SerializeField] GameObject entryPrefab;
    [SerializeField] Text titleLabel;
    [SerializeField] Panel panel;
    [SerializeField] GameObject canvas;
    List<AbilityMenuEntry> menuEntries = new List<AbilityMenuEntry>(MenuCount);
    public int selection { get; private set; }
    private void Awake()
    {
        //configure the Pool Manager so that it can generate the menu entries for us and have them ready
        GameObjectPoolController.AddEntry(EntryPoolKey, entryPrefab, MenuCount, int.MaxValue);
    }

    //Enqueue and Dequeue are convenient methods to get menu entries and return them to the pool manager
    AbilityMenuEntry Dequeue()
    {
        Poolable p = GameObjectPoolController.Dequeue(EntryPoolKey);
        AbilityMenuEntry entry = p.GetComponent<AbilityMenuEntry>();
        entry.transform.SetParent(panel.transform, false);
        entry.transform.localScale = Vector3.one;
        entry.gameObject.SetActive(true);
        entry.Reset();
        return entry;
    }
    void Enqueue(AbilityMenuEntry entry)
    {
        Poolable p = entry.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    //to clear the menu you have to loop through each entry and Enqueue it
    void Clear()
    {
        for (int i = menuEntries.Count - 1; i >= 0; --i)
        {
            Enqueue(menuEntries[i]);
        }
        menuEntries.Clear();
    }
    void Start()
    {
        panel.SetPosition(HideKey, false);
        canvas.SetActive(false);
    }
    Tweener TogglePos(string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.easingControl.duration = 0.5f;
        t.easingControl.equation = EasingEquations.EaseOutQuad;
        return t;
    }//animate the menu into position
    bool SetSelection(int value)
    {
        if (menuEntries[value].IsLocked)
        {
            return false;
        }

        //deselect the previously selected entry
        if (selection >= 0 && selection < menuEntries.Count)
        {
            menuEntries[selection].IsSelected = false;
        }
        selection = value;

        //delect the new entry
        if (selection >= 0 && selection < menuEntries.Count)
        {
            menuEntries[selection].IsSelected = true;
        }

        return true;
    }


    public void Next()
    {
        for (int i = selection + 1; i < selection + menuEntries.Count; i++)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
            {
                break;
            }
        }
    }
    public void Previous()
    {
        for (int i = selection - 1 + menuEntries.Count; i > selection; --i)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
            {
                break;
            }
        }
    }

    //pass along the title to display in the header, as well as a list of string which are the text to show for each entry in the menu
    public void Show(string title, List<string> options)
    {
        canvas.SetActive(true);
        Clear();
        titleLabel.text = title;
        for (int i = 0; i < options.Count; ++i)
        {
            AbilityMenuEntry entry = Dequeue();
            entry.Title = options[i];
            menuEntries.Add(entry);
        }
        SetSelection(0);
        TogglePos(ShowKey);
    }
    public void SetLocked(int index, bool value)
    {
        if (index < 0 || index >= menuEntries.Count)
        {
            return;
        }
        menuEntries[index].IsLocked = value;
        if (value && selection == index)
        {
            Next();
        }
    }

    //when the user confirms a menu selection (using the Fire1 input) then we can dismiss the panel
    public void Hide()
    {
        Tweener t = TogglePos(HideKey);
        t.easingControl.completedEvent += delegate (object sender, System.EventArgs e)
        {
            if (panel.CurrentPosition == panel[HideKey])
            {
                Clear();
                canvas.SetActive(false);
            }
        };
    }
}