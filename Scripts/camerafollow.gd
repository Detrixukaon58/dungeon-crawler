extends Node3D

var camera: Camera3D;

@export var extents_top: float = 45.0;

@export var extents_bottom: float = -85;

func _ready() -> void:
	camera = get_viewport().get_camera_3d();



func _process(_delta: float) -> void:
	rotation_degrees = Vector3(
		clamp(camera.rotation_degrees.x, extents_bottom, extents_top),
		camera.rotation_degrees.y,
		camera.rotation_degrees.z
	);
