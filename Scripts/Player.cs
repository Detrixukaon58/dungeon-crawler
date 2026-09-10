using Godot;
using System;

public partial class Player : RigidBody3D
{
    
    private bool menuToggle = false;

    private Vector2 mouse = Vector2.Zero;
    private Vector2 positional_movement = Vector2.Zero;
    private float turn_angle_y = 0.0f;

    public Camera3D main;

    public PlayerAnimationTree playerAnimationTree;

    public Callable resetSlash;

    public override void _Ready()
    {
        base._Ready();
        main = this.GetViewport().GetCamera3D();
        playerAnimationTree = GetNode<PlayerAnimationTree>("AnimationTree");
        resetSlash = new Callable(this, "ResetSlash");
    }

    public void GatherInput()
    {
        mouse = Input.GetLastMouseVelocity();

        positional_movement = Input.GetVector("down", "up", "strafe_left", "strafe_right") * 0.5f;

        turn_angle_y = (Input.GetActionStrength("turn_right") - Input.GetActionStrength("turn_left")) * 0.025f;

        menuToggle = Input.IsActionJustPressed("menu") ? !menuToggle : menuToggle;
        
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        main.Rotate(Vector3.Right, (-mouse.Y / this.GetViewport().GetVisibleRect().Size.Y) * 0.025f);
        // we only rotate if the 
        main.Rotation = new Vector3(
            Mathf.Clamp(main.Rotation.X, Mathf.DegToRad(-56.0f), Mathf.DegToRad(90.0f)),
            main.Rotation.Y,
            main.Rotation.Z
        );
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
        Godot.Collections.Dictionary result = GetWorld3D().DirectSpaceState.IntersectRay(new PhysicsRayQueryParameters3D
        {
            From=GlobalPosition + Vector3.Up,
            To=GlobalPosition - Vector3.Up,
            Exclude=new Godot.Collections.Array<Rid>
            {
                this.GetRid(),
                GetNode<Area3D>("L/Sprite3D/UIArea").GetRid()
            },
            CollideWithAreas=true,
            CollideWithBodies=true,
        });
        //DebugDraw3D.DrawArrow(GlobalPosition + Vector3.Up, GlobalPosition - Vector3.Up);
        if (Input.IsActionJustPressed("jump") && result.Count > 0)
        {
            //GD.Print(((Node3D)result["collider"]).Name);
            this.ApplyImpulse(this.Basis.Y * 1000.0f);
        }

    }

    public void SendAttack(Attack attack)
    {
        // TestBullet projectile = GD.Load<PackedScene>("res://Prefabs/TestBullet.tscn").Instantiate<TestBullet>();
        // projectile.Position = GlobalPosition + Vector3.Up;
        // projectile.playerVelocity = LinearVelocity;
        // projectile.direction = -GlobalTransform.Basis.Z;
        // projectile.timeout = 5.0f;
        // GetTree().Root.AddChild(projectile);

        // we need to manage each attack

        // each attack should have a preset action applied to it that we can run
        // we just need to supply the approriate parameters for the attack
        // for the meantime, we will use this to send the neeeded animation
        // we will switch between the attack types
        // They will either be Slash, Peirce Melee, Peirce Shot or AOE
        // we will also need to switch between the sub types
        // first we need to find out if they have a sub type that matches
        // Slash
        // Pierce Melee
        // Peirce Shot
        // anything else will be AOE
        //GD.Print("lol");

        switch (attack.attackType)
        {
            case AttackType.SLASH:
                playerAnimationTree.isSlashing = true;
                if(!playerAnimationTree.IsConnected(PlayerAnimationTree.SignalName.AnimationFinished, resetSlash))
                    playerAnimationTree.Connect(PlayerAnimationTree.SignalName.AnimationFinished, resetSlash);
            break;
            case AttackType.PEIRCE_SHOT:
            // we want to shoot something out
            // this thing will have the effect applied to it
            break;
            case AttackType.PEIRCE_MELEE:
            break;
            case AttackType.AOE:
            default:
            break;
        }

        attack.PlayAttack();
    }

    public void ResetSlash(String animName)
    {
        playerAnimationTree.isSlashing = false;
        playerAnimationTree.Disconnect(PlayerAnimationTree.SignalName.AnimationFinished, resetSlash);
    }


}
