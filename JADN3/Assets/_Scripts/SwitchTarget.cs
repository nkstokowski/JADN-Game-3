using UnityEngine;

public interface SwitchTarget{

    // Receive the signal that the switch was turned on
    void HandleSwitchOn();

    // Receive the signal that the switch was turned off
    void HandleSwitchOff();

    // Receive the signal that the switch was triggered
    void HandleSwitchTrigger();

}
