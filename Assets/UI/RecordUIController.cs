using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// see https://github.com/yasirkula/UnityNativeCamera

public class RecordUIController : MonoBehaviour
{
  AppManager app;
  bool hasCam;

  void Start()
  {
    app = AppManager.Instance;

    // Check if device has camera
    hasCam = NativeCamera.DeviceHasCamera();
    app.log("DeviceHasCamera is " + hasCam);
  }

  void Update()
  {

  }

  public void handleRecordButtonClick()
  {
    app.log("record button clicked");
    // Don't attempt to use the camera if it is already open
    if (NativeCamera.IsCameraBusy())
    {
      app.log("Camera is busy");
      return;
    }



    if (!hasCam) return;

    NativeCamera.RecordVideo(recordCallback, NativeCamera.Quality.Default, 0, 0);
  }

  void recordCallback(string filepath)
  {
    if (filepath != null)
      app.log("filepath is " + filepath);
    else
      app.log("filepath was null");
  }
}
