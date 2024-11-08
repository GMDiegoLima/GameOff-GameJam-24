using UnityEngine;

public class PlayerStateController : StateController 
{
    [SerializeField] KeyCode attackKey;

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(attackKey))
        {
            Debug.Log(actor.actorName + "::Attack()");
            actor.Attack();
		}
    }

    public void RemoveCurrentActor()
    { 
        Destroy(actor);
	}

    public void UpdateCurrentActor(GameActor anActor)
    {
        actor = anActor;
        actor.controller = this;
	}
}
