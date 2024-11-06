using UnityEngine;

public interface IState
{
    public string Name { get; set; }
    public Color GizmoColor { get; set; }

    public void Enter(StateController controller);
    public void Update(StateController controller);
    public void Exit(StateController controller);
    public void CheckTransition(StateController controller);

    public void DrawGizmos(StateController controller);
}
