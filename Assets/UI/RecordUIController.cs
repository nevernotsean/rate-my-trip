using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// see https://github.com/yasirkula/UnityNativeCamera

public class RecordUIController : MonoBehaviour
{
  AppManager app;

  void Start()
  {
    app = AppManager.Instance;
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

    // Check if device has camera
    if (NativeCamera.DeviceHasCamera())
    {
      app.log("DeviceHasCamera false");
      return;
    }

    NativeCamera.RecordVideo(recordCallback, NativeCamera.Quality.Default, 0, 0);
  }

  void recordCallback(string filepath)
  {
    app.log(filepath);
  }
}
