using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MobileCam : MonoBehaviour {

  AppManager app;

	private bool camAvailable;
	private WebCamTexture cameraTexture;
	private Texture defaultBackground;

	public RawImage background;
	public AspectRatioFitter fit;
	public bool frontFacing;
	
	// Use this for initialization
	void Start () {

        app = AppManager.Instance;


		defaultBackground = background.texture;
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length == 0)
        {
            app.log("No Camera detected");
            camAvailable = false;
            return;
        }


		for (int i = 0; i < devices.Length; i++)
		{
			var curr = devices[i];

			if (curr.isFrontFacing == frontFacing)
			{
				cameraTexture = new WebCamTexture(curr.name, Screen.width, Screen.height);
				break;
			}
		}	

		if (cameraTexture == null)
        {
            app.log("Unable to find specific camera");
            return;
        }

        app.log("starting camera texture ");
		cameraTexture.Play (); // Start the camera
		background.texture = cameraTexture; // Set the texture

		camAvailable = true; // Set the camAvailable for future purposes.
        app.log("DeviceHasCamera is " + camAvailable);
	}
	
	// Update is called once per frame
	void Update () {
		if (!camAvailable)
			return;

		float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
		fit.aspectRatio = ratio; // Set the aspect ratio

		float scaleY = cameraTexture.videoVerticallyMirrored ? -1f : 1f; // Find if the camera is mirrored or not
		background.rectTransform.localScale = new Vector3(1f, scaleY, 1f); // Swap the mirrored camera

		int orient = -cameraTexture.videoRotationAngle;
		background.rectTransform.localEulerAngles = new Vector3(0,0, orient);
	}
}