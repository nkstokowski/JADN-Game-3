using System.Collections.Generic;
using UnityEngine;

// Switch type - 
//      Switch: The switch flips from on to off on each hit
//      Button: The switch sends out the same signal on each hit
public enum SwitchType
{
    Switch,
    Button
}

public class Switch : MonoBehaviour{

    // Whether this switch acts like a switch or a button
    public SwitchType switchType = SwitchType.Switch;
    // Whether or not this switch is on
    public bool switchOn = false;
    public bool sendsNotification = false;
    // The list of game objects this switch will send a signal to
    public List<GameObject> targetObjects; 
    public Indication notificationCenter;

    // The list of SwitchTarget components to send signals to
    protected List<SwitchTarget> targets; 


    // Build the list of targets to send signals to
    public void BuildTargetList()
    {
        // Clear the old list
        targets = new List<SwitchTarget>();
        SwitchTarget st;

        // Remove any objects that were destroyed
        targetObjects.RemoveAll(obj => obj == null);

        // Build the new list
        foreach (GameObject obj in targetObjects)
        {
            st = obj.GetComponent<SwitchTarget>();
            if ((Component)st)
            {
                targets.Add(st);
            }
        }
    }

    public void AddSwitchTarget(GameObject obj)
    {
        targetObjects.Add(obj);
        BuildTargetList();
    }

    public void RemoveSwitchTarget(GameObject obj)
    {
        targetObjects.Remove(obj);
        BuildTargetList();
    }

    // Send the turn on signal to all targets
    protected void TurnOn()
    {
        bool recalcList = false;

        foreach(SwitchTarget target in targets)
        {
            if ((Component)target!= null)
            {
                target.HandleSwitchOn();
            }
            else
            {
                recalcList = true;
            }
        }

        if (recalcList)
        {
            BuildTargetList();
        }
        if(sendsNotification)
        {
            notificationCenter.TriggerNotification();
        }
    }

    // Send the turn off signal to all targets
    protected void TurnOff()
    {
        bool recalcList = false;

        foreach (SwitchTarget target in targets)
        {
            if ((Component)target != null)
            {
                target.HandleSwitchOff();
            }
            else
            {
                recalcList = true;
            }
        }

        if (recalcList)
        {
            BuildTargetList();
        }
        if(sendsNotification)
        {
            notificationCenter.TriggerNotification();
        }
    }

    // Send the trigger signal to all targets
    protected void Trigger()
    {
        bool recalcList = false;

        foreach (SwitchTarget target in targets)
        {
            if ((Component)target != null)
            {
                target.HandleSwitchTrigger();
            }
            else
            {
                recalcList = true;
            }
        }

        if (recalcList)
        {
            BuildTargetList();
        }
    }

}
