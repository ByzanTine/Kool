using UnityEngine;
using System.Collections.Generic;
public class DynamicCamera : MonoBehaviour {
	public float dampTime = 0.1f;

	[SerializeField] 
	//public Transform[] targets;
	List<Transform> targets;
	[SerializeField] 
	float boundingBoxPadding = 0.1f;
	
	[SerializeField] 
	float minCamera = 8f;
	
	[SerializeField]
	float minimumOrthographicSize = 18f;
	
	[SerializeField]
	float zoomSpeed = 20f;

	[SerializeField]
	float xAngle = 1.012291f; // angle in rad
	Camera camera;
	
	void Awake () 
	{
		camera = GetComponent<Camera>();
	}
	
	void LateUpdate()
	{
		targets.Clear ();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject obj in objs) {
			targets.Add(obj.transform);
		}
		Rect boundingBox = CalculateTargetsBoundingBox();
			Vector3 camerNextPos = CalculateCameraPosition(CalculateCameraBoundingBox(boundingBox));
		Vector3 velocity = Vector3.zero;
		transform.position = Vector3.SmoothDamp(transform.position, camerNextPos, ref velocity, dampTime);;

			//camera.orthographicSize = CalculateOrthographicSize(boundingBox);
	}
	
	/// <summary>
	/// Calculates a bounding box that contains all the targets.
	/// </summary>
	/// <returns>A Rect containing all the targets.</returns>
	Rect CalculateTargetsBoundingBox()
	{
		float minX = Mathf.Infinity;
		float maxX = Mathf.NegativeInfinity;
		float minZ = Mathf.Infinity;
		float maxZ = Mathf.NegativeInfinity;
		
		foreach (Transform target in targets) {
			Vector3 position = target.position;
			
			minX = Mathf.Min(minX, position.x);
			minZ = Mathf.Min(minZ, position.z);
			maxX = Mathf.Max(maxX, position.x);
			maxZ = Mathf.Max(maxZ, position.z);
		}
		
		return Rect.MinMaxRect(minX - boundingBoxPadding, maxZ + boundingBoxPadding, maxX + boundingBoxPadding, minZ - boundingBoxPadding);
	}
	
	Rect CalculateCameraBoundingBox(Rect inR){
		
		Vector2 center = inR.center;
		center.y = inR.center.y - transform.position.y * Mathf.Tan (xAngle) ;
		float w = inR.width > minCamera ? inR.width : minCamera;
		float h = Mathf.Abs(inR.height) > Mathf.Abs(minCamera) ? Mathf.Abs(inR.height) : Mathf.Abs(minCamera);
		return Rect.MinMaxRect(center.x - w/2f , center.y + h / 2f, center.x + w/2f,  center.y - h / 2f);
		
		
	}
	/// <summary>
	/// Calculates a camera position given the a bounding box containing all the targets.
	/// </summary>
	/// <param name="boundingBox">A Rect bounding box containg all targets.</param>
	/// <returns>A Vector3 in the center of the bounding box.</returns>
	Vector3 CalculateCameraPosition(Rect boundingBox)
	{
		//	print (boundingBox);
		Vector2 boundingBoxCenter = boundingBox.center;
		//	print (boundingBoxCenter);
		float size = boundingBox.width > Mathf.Abs (boundingBox.height) ? boundingBox.width : Mathf.Abs (boundingBox.height);
		//print (Mathf.Atan (size * 0.043944f)/ Mathf.PI * 180 / 8);
		Camera.main.fieldOfView = Mathf.Atan (size * 0.043944f) / Mathf.PI * 180 / 8;
		return new Vector3(boundingBoxCenter.x, Camera.main.transform.position.y,boundingBoxCenter.y);
	}
}