using Godot;
using Godot.Collections;
using System;

public partial class MouseHandling : Sprite3D
{
    
    public SubViewport UiViewport;
    public bool isMouseInside = false;

    public override void _Ready()
    {
        UiViewport = GetNode<SubViewport>("../../UI");
    }

    public void mouse_entered()
    {
        UiViewport.Notification((int)NotificationVpMouseEnter);
        isMouseInside = true;
    }
    
    public void mouse_exited()
    {
        UiViewport.Notification((int)NotificationVpMouseExit);
        isMouseInside = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event == null)
        {
            return;
        }

        if (@event is InputEventMouse mouseEvent){
            // we now need to work out the mouse position from the centre of the UI

            Vector3 centre = GlobalPosition;

            // Raycast the mouse from the camera

            Vector3 mouseRay = GetViewport().GetCamera3D().ProjectRayNormal(mouseEvent.GlobalPosition);

            Dictionary result = GetWorld3D().DirectSpaceState.IntersectRay(new PhysicsRayQueryParameters3D
            {
                From = GetViewport().GetCamera3D().GlobalPosition,
                To = GetViewport().GetCamera3D().GlobalPosition + mouseRay * 10.0f,
                Exclude=new Array<Rid>(),
                CollideWithAreas=true
            });

            //DebugDraw3D.DrawArrow(GetViewport().GetCamera3D().GlobalPosition, GetViewport().GetCamera3D().GlobalPosition + mouseRay * 10.0f);
            //GD.Print(result);
            if(result.Count != 0){
                Vector3 mouseRayHitPosition = (Vector3)result["position"];

                if (((Node3D)result["collider"]).IsInGroup("UI") && !isMouseInside)
                {
                    mouse_entered();
                }
                
                //Vector3 mouseLocalPosition = (centre - mouseRayHitPosition);

                // rotate to position
                //DebugDraw3D.DrawSphere(mouseRayHitPosition, 0.1f, Colors.Blue);
                mouseRayHitPosition = GlobalTransform.AffineInverse() * mouseRayHitPosition;
                //DebugDraw3D.DrawSphere(mouseRayHitPosition, 0.1f);
                //GD.Print(mouseRayHitPosition);
                mouseEvent.Position = mouseEvent.GlobalPosition = -new Vector2(
                    -mouseRayHitPosition.X,
                    mouseRayHitPosition.Y
                ) / PixelSize
                + new Vector2(
                    Texture.GetWidth() /2.0f,
                    Texture.GetHeight() / 2.0f
                );
                //GD.Print(mouseEvent.GlobalPosition);
            }
            else
            {
                if (isMouseInside)
                {
                    mouse_exited();
                }
            }
            

            UiViewport.PushInput(mouseEvent);
        }
    }



}
