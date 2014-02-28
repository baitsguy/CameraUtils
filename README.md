Camera Utilites
===

Instantiation
The script can be added to any game object in the scene. It then need to be set with the POI transform.

private CameraUtils c;

void Start() {
	c.this.GetComponent<CameraUtils>();
	c.POI = transform;
	//Can set a new camera to be followed by using c.camera = X;
...

Modes

Axes tracking
By default the script will follow the POI in the xyz directions. The following parameters can be modified:

bool followX, followY, followZ: Used to turn the axes on/off for tracking.
float followSpeed: The higher the speed the faster the camera gets to the object
bool boundX, boundY, boundZ: Used to enable/disable bounds on axes
float maxX, minX, … minZ: These are initialized to 0. On being given a value, that axis turns on its bound variable and the camera will only follow the POI within the bounds.

c.followZ = false;
c.followY = false;
c.minX = -5;
c.maxX = 5;
...

First person view
The FPV can be enabled by setting c.fpv = true. It'll track the mouse position and use it as the camera's rotation.
The following parameters can be modified:
sensitivityX, sensitivityY: The mouse sensitivity
angleX, angleY: To limit your field of view

Zoom
The class contains some basic code to allow the Camera to zoom in and out of the POI with varying speed.

//void zoom (float duration, bool zoomIn);
c.zoom(0.1f, false);

The zoom works for both orthographic and perspective cameras. The physical position of the camera is only changed when it is perspective. To use this feature, the user must call the function in a loop. e.g.

void Update() {
	c.zoom(0.1f, false);
}

This will continuously zoom out the camera.

Camera shake
One can make the camera shake for a given duration and amplitude.

c.shake(5, 0.5f);

This will shake the camera for 5 seconds by moving with an amplitude of 0.5f in the x and y directions.
