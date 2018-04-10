
using UnityEngine;

public interface Interactable{

    /*
     * Should be called when a collision is detected
     * between a spell and an interactable object.
     */
    void OnSpellHit(Transform spell);

    /*
     * Should return the world point that the player should
     * turn towards in order to hit this interactable object.
     * 
     * This should return transform.position in most cases,
     * but if you ever need spells to aim at somewhere else
     * this is where you can change that.
     */
    Vector3 GetSpellHitPoint();
}
