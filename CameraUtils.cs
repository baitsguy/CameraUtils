using UnityEngine;
using System.Collections;

public class CameraUtils : MonoBehaviour {

	private Transform _POI;
	private Camera _camera;

	//Parameters
	private float _followSpeed;
	private Vector3 _offset;

	//States
	private bool _followX;
	private bool _followY;
	private bool _followZ;
	private bool _fpv;

	//Bounds
	private bool _boundX;
	private float _maxX;
	private float _minX;
	private bool _boundY;
	private float _maxY;
	private float _minY;
	private bool _boundZ;
	private float _maxZ;
	private float _minZ;

	//FPS mode
	private float _sensitivityX = 15;
	private float _sensitivityY = 15;
	private float _angleX = 360;
	private float _angleY = 60;

	
	
	float rotationY = 0F;


	//Get and sets
	public float followSpeed {
		get {
			return(_followSpeed);
		}
		set {
			_followSpeed = value;
		}
	}
	public Camera camera {
		get {
			return _camera;
		}
		set {
			_camera = value;
		}
	}
	public Transform POI {
		get {
			return _POI;
		}
		set {
			_POI = value;
		}
	}
	public bool followX {
		get {
			return _followX;
		}
		set {
			_followX = value;
		}
	}
	public bool followY {
		get {
			return _followY;
		}
		set {
			_followY = value;
		}
	}
	public bool followZ {
		get {
			return _followZ;
		}
		set {
			_followZ = value;
		}
	}
	public bool fpv {
		get {
			return _fpv;
		}
		set {
			_fpv = value;
		}
	}
	public Vector3 offset {
		get {
			return _offset;
		}
		set {
			_offset = value;
		}
	}
	public bool boundX {
		get {
			return _boundX;
		}
		set {
			_boundX = value;
		}
	}
	public bool boundY {
		get {
			return _boundY;
		}
		set {
			_boundY = value;
		}
	}
	public bool boundZ {
		get {
			return _boundZ;
		}
		set {
			_boundZ = value;
		}
	}
	public float minX {
		get {
			return _minX;
		}
		set {
			_minX = value;
			_boundX = true;
		}
	}
	public float minY {
		get {
			return _minY;
		}
		set {
			_minY = value;
			_boundY = true;
		}
	}
	public float minZ {
		get {
			return _minZ;
		}
		set {
			_minZ = value;
			_boundZ = true;
		}
	}
	public float maxX {
		get {
			return _maxX;
		}
		set {
			_maxX = value;
			_boundX = true;
		}
	}
	public float maxY {
		get {
			return _maxY;
		}
		set {
			_maxY = value;
			_boundY = true;
		}
	}
	public float maxZ {
		get {
			return _maxZ;
		}
		set {
			_maxZ = value;
			_boundZ = true;
		}
	}
	public float sensitivityX {
		get {
			return _sensitivityX;
		}
		set {
			_sensitivityX = value;
		}
	}
	public float sensitivityY {
		get {
			return _sensitivityY;
		}
		set {
			_sensitivityY = value;
		}
	}
	public float angleX {
		get {
			return _angleX;
		}
		set {
			_angleX = value;
		}
	}
	public float angleY {
		get {
			return _angleY;
		}
		set {
			_angleY = value;
		}
	}


	//Defaults
	void Awake() {
		_followX = true;
		_followY = true;
		_followZ = true;
		_fpv = false;
		_followSpeed = 10;
		_camera = Camera.main;
		_offset = new Vector3(2,1,1);
		_boundX = false;
		_boundY = false;
		_boundZ = false;
		_minX = 0;
		_minY = 0;
		_minZ = 0;
		_maxX = 0;
		_maxY = 0;
		_maxZ = 0;
		_sensitivityX = 15;
		_sensitivityY = 15;
		_angleX = 360;
		_angleY = 60;
	}

	// Update is called once per frame
	void Update () {

		if(!_fpv)
		{
			Vector3 target = _camera.transform.position;
		Debug.Log ("Follows are " + followX + followY + followZ); 

		if(_followX) {
			target.x = _POI.position.x;
			if(_boundX && target.x < _minX)
				target.x = _minX;
			else if(_boundX && target.x > _maxX)
				target.x = _maxX;
			target.x += _offset.x;
		}
		if(_followY) {
			target.y = _POI.position.y;
			if(_boundY && target.y < _minY)
				target.y = _minY;
			else if(_boundY && target.y > _maxY)
				target.y = _maxY;
			target.z += _offset.z;
		}
		if(_followZ) {
			target.z = _POI.position.z;
			if(_boundZ && target.z < _minZ)
				target.z = _minZ;
			else if(_boundZ && target.z > _maxZ)
				target.z = _maxZ;
			target.z += _offset.z;
		}
	
		_camera.transform.position = Vector3.Lerp(_camera.transform.position, target, _followSpeed * Time.deltaTime);
		//Vector3 pos = ((1 - _followSpeed/10.0f) * _camera.transform.position + _followSpeed/10.0f * target);
		//_camera.transform.position = pos;
		}
		else fpsUpdate();

	}

	void fpsUpdate() {
		float rotationX = _camera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, -angleY, angleY);
		_camera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
	}
	//Methods
	
	/* Add a camera shake for a definite period of time */
	// Code based on http://unitytipsandtricks.blogspot.com/2013/05/camera-shake.html

	public void shake(float duration, float magnitude) {
		StartCoroutine(startShake (duration, magnitude));
	}

	private IEnumerator startShake(float duration, float magnitude) {
		
		float time = 0.0f;
		Vector3 initial = _camera.transform.position;
		while (time < duration) {
			
			time += Time.deltaTime;

			float percentComplete = time / duration;   
			Debug.Log("done with "  + percentComplete);
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
			
			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;
			
			_camera.transform.position = new Vector3(x, y, initial.z);	
			yield return null;

		}
		
		_camera.transform.position = initial;
	}

	/* Smooth zoom in or out */

	public void zoom(float speed, bool zoomIn) {
		Vector3 dir = (_camera.transform.position - _POI.transform.position).normalized;
		if(zoomIn) {
			if(!_camera.isOrthoGraphic) {
				_camera.transform.position -= dir * speed;
				_offset -= dir;
			}
			else {
				_camera.orthographicSize -=0.1f * speed;
				_offset.x -= 0.05f * speed * _offset.x;
				_offset.y -= 0.05f * speed * _offset.y;
				_offset.z -= 0.05f * speed * _offset.z;

			}
		}
		else {
			if(!_camera.isOrthoGraphic) {
				_camera.transform.position += dir * speed;
				_offset -= dir;
			}
			else {
				_camera.orthographicSize += 0.1f * speed;
				_offset.x += 0.05f * speed;
				_offset.y += 0.05f * speed;
				_offset.z += 0.05f * speed;
			}
		}
	}
}
