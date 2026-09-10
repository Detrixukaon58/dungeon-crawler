using Godot;
using System;

public partial class TestBullet : Node3D
{
    
    public Vector3 direction = Vector3.Forward;

    public Vector3 playerVelocity = Vector3.Zero;

    public float timeout;

    private float tmr;
    public override void _Process(double delta)
    {
        float f_delta = (float)delta;
        Position += f_delta * direction.Normalized() * 2.0f;
        tmr += f_delta;

        if(tmr >= timeout)
        {
            QueueFree();
        }
    }

}
