using UnityEngine;

public interface IState
{
    public string Name { get; set; }
    public Color GizmoColor { get; set; }

    public void Enter();
    public void Update();
    public void Exit();
    public void CheckTransition();
    public void DrawGizmos();
}
