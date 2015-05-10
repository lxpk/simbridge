using UnityEngine;
using System.Collections;

public class ZoomMode : MonoBehaviour {

	public bool clickToZoom = false;
	public bool panWhileZoomed = false;
	public bool zoomed = false;
	public bool zoomingIn = false;
	public bool zoomingOut = false;
	public float zoomingStartTime = 0f;
	public float zoomingSpeed = 0.5f;
	public bool resetOnZoomOut = true;
	public bool enableMouseOnZoomOut = false;

	public Camera zoomingCamera;
	public float zoomingCameraFOV;
	public float zoomingCameraFOVZoomed;
	public Vector3 zoomingCameraLocalPosition;

	// CAMERA PANNING
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2, MouseXYZ }
	public RotationAxes axes = RotationAxes.MouseXYZ;
	public float sensitivityX = 15f;
	public float sensitivityY = 15f;
	public float sensitivityZ = 15f;

	public float minimumX = -360f;
	public float maximumX = 360f;
	
	public float minimumY = -60f;
	public float maximumY = 60f;

	public float minimumZ = -60f;
	public float maximumZ = 60f;

	public float localPositionY = 0f;
	public float localPositionX = 0f;
	public float localPositionZ = 0f;

	public float zoomFactor = 0.7f;
	// Use this for initialization
	void Start () {
		if(zoomingCamera == null)
		{
			zoomingCamera = Camera.main;
		}
		zoomingCameraFOV = zoomingCamera.fieldOfView;
		zoomingCameraFOVZoomed = zoomingCameraFOV * zoomFactor;
		zoomingCameraLocalPosition = zoomingCamera.transform.localPosition;
		localPositionX = zoomingCameraLocalPosition.x;
		localPositionY = zoomingCameraLocalPosition.y;
		localPositionZ = zoomingCameraLocalPosition.z;

	}
	
	// Update is called once per frame
	void Update () {
		if (clickToZoom)
		{
			if(Input.GetMouseButtonDown(1) || (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(0)))
			{
				ZoomToggle();
			}
		}
		if (zoomed)
		{
			if (panWhileZoomed)
			{
				if (axes == RotationAxes.MouseXYZ)
				{
					localPositionX += Input.GetAxis("Mouse X") * sensitivityX;
					//localPositionY += Input.GetAxis("Mouse Y") * sensitivityY;
					localPositionZ += Input.GetAxis("Mouse Y") * sensitivityZ;
					localPositionY = Mathf.Clamp (localPositionY, minimumY, maximumY);
					localPositionX = Mathf.Clamp (localPositionX, minimumX, maximumX);
					localPositionZ = Mathf.Clamp (localPositionZ, minimumZ, maximumZ);
					zoomingCamera.transform.localPosition = new Vector3(localPositionX, zoomingCameraLocalPosition.y, localPositionZ);
				}
				else if (axes == RotationAxes.MouseX)
				{
					localPositionX += Input.GetAxis("Mouse X") * sensitivityX;
					localPositionX = Mathf.Clamp (localPositionX, minimumX, maximumX);
					zoomingCamera.transform.localPosition = new Vector3(localPositionY, localPositionX, 0);
				}
				else
				{
					localPositionY += Input.GetAxis("Mouse Y") * sensitivityY;
					localPositionY = Mathf.Clamp (localPositionY, minimumY, maximumY);
					zoomingCamera.transform.localPosition = new Vector3(localPositionY, localPositionX, 0);
				}
			}
		}
		else
		{
			//zoomingCamera.transform.position = zoomingCameraLocalPosition;
		}
		float timeElapsed = Time.time - zoomingStartTime;
		float zoomElapsed;
		if(zoomingSpeed == 0)
		{
			zoomElapsed = 1f;
		}
		else
		{
			zoomElapsed = timeElapsed / zoomingSpeed;
		}
		if(zoomingIn)
		{
			zoomingCamera.fieldOfView = Mathf.Lerp(zoomingCamera.fieldOfView,zoomingCameraFOVZoomed,zoomElapsed);
			if(Time.time-zoomingStartTime > zoomingSpeed) 
			{
				zoomingIn = false;
				zoomingCamera.fieldOfView = zoomingCameraFOVZoomed;
			}
		}
		else if (zoomingOut)
		{
			zoomingCamera.fieldOfView = Mathf.Lerp(zoomingCamera.fieldOfView,zoomingCameraFOV,zoomElapsed);
			if(Time.time-zoomingStartTime > zoomingSpeed) 
			{
				zoomingOut = false;
				if(enableMouseOnZoomOut)
				{
					Cursor.lockState = CursorLockMode.Confined;
					Cursor.visible = true;
				}
				zoomingCamera.fieldOfView = zoomingCameraFOV;
				if(resetOnZoomOut)
				{
					zoomingCamera.transform.localPosition = zoomingCameraLocalPosition;
				}
			}
		}
	}

	public void ZoomToggle()
	{
		if(zoomed) {ZoomOut();}
		else if(!zoomed){ ZoomIn();}
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void ZoomIn()
	{
		zoomingIn = true;
		zoomingOut = false;
		zoomed = true;
		zoomingStartTime = Time.time;
	}
	public void ZoomOut()
	{
		zoomingIn = false;
		zoomingOut = true;
		zoomed = false;
		zoomingStartTime = Time.time;
	}
}
