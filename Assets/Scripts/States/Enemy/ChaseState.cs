using UnityEngine;

public class ChaseState : IState
{
    private string name;
    private Color gizmoColor = Color.red;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    private EnemyAIController controller;
    public Transform target;

    public ChaseState(EnemyAIController anEnemyAIController, Transform aTarget)
    {
        controller = anEnemyAIController;
        target = aTarget;
    }

    public void Enter()
    {
        Debug.Log(controller.actor.actorTag + " Enter Chase State");
        controller.targetFootprints.Clear();
        controller.ChangeToChaseView();
    }

    public void Update()
    {
        controller.Chase(target);
        CheckTransition();
    }

    public void Exit()
    {
        //controller.targetFootprints.Clear();
        Debug.Log(controller.actor.actorTag + " Exit Chase State");
    }


    public void CheckTransition()
    {
        if (!controller.IsTargetInSight())
        {
            controller.TransitionToState(new SeekState(controller));
        }
        if (controller.IsTargetInAttackRange())
        {
            controller.TransitionToState(new AttackState(controller));
        }
        if (controller.IsActorDead())
        {
            controller.TransitionToState(new DeadState(controller));
        }
    }

    public void DrawGizmos()
    {
        if (!controller.GetTargetTransform()) return;
        Gizmos.color = Color.white;
        Vector2 dir = (controller.GetTargetTransform().position - controller.transform.position).normalized;
        Gizmos.DrawRay(controller.transform.position, dir * controller.actor.attackRange);

        foreach(Vector3 v in controller.targetFootprints)
        {
            Gizmos.DrawWireSphere(v, 0.5f);
		}
    }
}
