using UnityEngine;

public class PlayerStateController : StateController 
{
    public void RemoveCurrentActor()
    { 
        Destroy(actor);
	}

    public void UpdateCurrentActor(GameActor anActor)
    {
        actor = anActor;
        Debug.Log("Update Actor: " + actor.actorName);
        actor.controller = this;
	}
}
