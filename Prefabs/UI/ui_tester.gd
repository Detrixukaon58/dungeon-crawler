extends Control

var mouse: Vector2;

# func _draw() -> void:
#     draw_circle(mouse,20.0, Color.WHITE);


func _input(event: InputEvent) -> void:
    if event as InputEventMouse != null:
        var mouse_event = event as InputEventMouse;
        mouse = mouse_event.position;
        print(mouse);
    