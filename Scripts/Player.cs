using Godot;
using System;

public partial class Player : RigidBody3D
{
    
    private bool menuToggle = false;

    private Vector2 mouse = Vector2.Zero;
    private Vector2 positional_movement = Vector2.Zero;
    private float turn_angle_y = 0.0f;

    public Camera3D main;

    public override void _Ready()
    {
        base._Ready();
        main = this.GetViewport().GetCamera3D();
    }

    public void GatherInput()
    {
        mouse = Input.GetLastMouseVelocity();

        positional_movement = Input.GetVector("down", "up", "strafe_left", "strafe_right");

        turn_angle_y = (Input.GetActionStrength("turn_right") - Input.GetActionStrength("turn_left")) * 0.05f;

        menuToggle = Input.IsActionJustPressed("menu") ? !menuToggle : menuToggle;
        
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        main.Rotate(Vector3.Right, (-mouse.Y / this.GetViewport().GetVisibleRect().Size.Y) * 0.1f);
        //Input.WarpMouse(this.GetViewport().GetVisibleRect().End);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        GatherInput();
        this.Rotate(Vector3.Up, turn_angle_y);
        this.LinearVelocity = (
            (-this.Basis.Z * positional_movement.X
            + this.Basis.X * positional_movement.Y)
            *10.0f
            + this.Basis.Y * this.LinearVelocity
        );
        if (Input.IsActionJustPressed("jump"))
        {
            this.ApplyImpulse(this.Basis.Y * 1000.0f);
        }

    }


}
