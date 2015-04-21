using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraStartAnim : MonoBehaviour {
	public bool done = false;
	List<Transform> allTargets = new List<Transform>();
	int zoom_step = 0;
	//public Transform[] targets;
	List<Transform> targets = new List<Transform>();
	float boundingBoxPadding = 0.1f;
	float minCamera = 8f;
	float xAngle = 1.012291f; // angle in rad
	Camera camera;

	private static CameraStartAnim _instance;
	
	//This is the public reference that other classes will use
	public static CameraStartAnim Instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<CameraStartAnim>();
			return _instance;
		}
	}

	void Awake () 
	{
		// Scene transition protection for singleton
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
		camera = GetComponent<Camera>();
		allTargets.Clear ();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject obj in objs) {
			allTargets.Add(obj.transform);
		}
		Invoke ("ZoomIn", 0.1f);
		Invoke ("ZoomOut", 2);
		Invoke ("ZoomIn", 3);
		Invoke ("ZoomOut", 5);
		Invoke ("ZoomIn", 6);
		Invoke ("ZoomOut", 8);
		Invoke ("ZoomIn", 9);
		Invoke ("ZoomDone", 11);

	}
	public void UpdateAllTarget(){
		allTargets.Clear ();
		UserData[] udatas = UserInfoManager.UserDataCollection;
		foreach (UserData ud in udatas) {
			allTargets.Add(ud.wizardInstance.transform);
		}	
	}


	void ZoomIn(){
		targets.Clear ();
		targets.Add(allTargets[zoom_step]);
		zoom_step++;
	}
	void ZoomOut(){
		targets.Clear ();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject obj in objs) {
			targets.Add(obj.transform);
		}
	}
	void ZoomDone(){
		done = true;
	}
	void LateUpdate()
	{
		if (done) {
			return;
		}
		Rect boundingBox = CalculateTargetsBoundingBox();
		Vector3 camerNextPos = CalculateCameraPosition(CalculateCameraBoundingBox(boundingBox));
		Vector3 velocity = Vector3.zero;
		transform.position = SmoothDampVector3 (transform.position, camerNextPos);
	}
	
	/// <summary>
	/// Calculates a bounding box that contains all the targets.
	/// </summary>
	/// <returns>A Rect containing all the targets.</returns>
	Rect CalculateTargetsBoundingBox()
	{
		//		float minX = Mathf.Infinity;
		//		float maxX = Mathf.NegativeInfinity;
		//		float minZ = Mathf.Infinity;
		//		float maxZ = Mathf.NegativeInfinity;
		
		float minX = 0;
		float maxX = 0;
		float minZ = 0;
		float maxZ = 0;

		if (targets.Count == 1) {
			Vector3 position = targets[0].position;

			minX = maxX = position.x;
			minZ = maxZ = position.z;
		} else {
			foreach (Transform target in targets) {
				Vector3 position = target.position;
			
				minX = Mathf.Min (minX, position.x);
				minZ = Mathf.Min (minZ, position.z);
				maxX = Mathf.Max (maxX, position.x);
				maxZ = Mathf.Max (maxZ, position.z);
			}
		}
		return Rect.MinMaxRect(minX - boundingBoxPadding, maxZ + boundingBoxPadding, maxX + boundingBoxPadding, minZ - boundingBoxPadding);
	}
	
	Rect CalculateCameraBoundingBox(Rect inR){
		
		Vector2 center = inR.center;
		center.y = inR.center.y - transform.position.y * Mathf.Tan (xAngle) ;
		float w = inR.width > minCamera ? inR.width : minCamera;
		float h = Mathf.Abs(inR.height) > Mathf.Abs(minCamera) ? Mathf.Abs(inR.height) : Mathf.Abs(minCamera);
		// print (h);
		return Rect.MinMaxRect(center.x - w/2f , center.y + h / 2f, center.x + w/2f,  center.y - h / 2f);
		
		
	}
	/// <summary>
	/// Calculates a camera position given the a bounding box containing all the targets.
	/// </summary>
	/// <param name="boundingBox">A Rect bounding box containg all targets.</param>
	/// <returns>A Vector3 in the center of the bounding box.</returns>
	Vector3 CalculateCameraPosition(Rect boundingBox)
	{
		Vector2 boundingBoxCenter = boundingBox.center;
		float size = boundingBox.width > Mathf.Abs (boundingBox.height) ? boundingBox.width : Mathf.Abs (boundingBox.height);
		Camera.main.fieldOfView = SmoothDampFloat(Camera.main.fieldOfView, Mathf.Atan (size * 0.043944f) / Mathf.PI * 180 / 8);
		return new Vector3(boundingBoxCenter.x, Camera.main.transform.position.y,boundingBoxCenter.y);
	}
	
	float SmoothDampFloat(float from, float to, float time = 0.2f){
		float delta = (to - from)/ time * Time.deltaTime;
		return from + CameraMoveThreshold(delta);
	}
	Vector3 SmoothDampVector3(Vector3 from, Vector3 to, float time = 0.2f){
		Vector3 delta = (to - from) / time * Time.deltaTime;
		//	return from + delta;
		return from + CameraMoveThreshold (delta);
	}
	float CameraMoveThreshold(float delta, float thresold = 0.2f){
		if (Mathf.Abs (delta) > thresold) {
			return delta / Mathf.Abs(delta) * thresold;
		} else {
			return delta;
		}
	}
	Vector3 CameraMoveThreshold(Vector3 delta, float threshold = 1.5f){
		if (delta.magnitude > threshold) {
			return delta.normalized * threshold;
		} else {
			return delta;
		}
	}
}
