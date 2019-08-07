using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

// see https://github.com/yasirkula/UnityNativeCamera

// http://dotween.demigiant.com/getstarted.php
// http://dotween.demigiant.com/documentation.php#shortcuts

public class RecordUIController : MonoBehaviour
{
  AppManager app;
  bool hasCam;
  private bool isRecording = false;
  public float TimeToRecord;

  public TMPro.TextMeshProUGUI questionText;
  public string[] questions;
  public float questionSeconds;

  int currentQuestion = 0;


  void Start()
  {
    app = AppManager.Instance;

    // Check if device has camera
    //hasCam = NativeCamera.DeviceHasCamera();
    //app.log("DeviceHasCamera is " + hasCam);


    // Set the time you are allowing the user to record gameplay
    ReplayKitUnity.AllowedTimeToRecord = TimeToRecord;

    // Tells ReplayKit to use a default interface that is excluded in playback         
    ReplayKitUnity.ShowDefaultButtonUI();

    // Subscribe to the ReplayKit callbacks 
    if (ReplayKitUnity.IsScreenRecorderAvailable)
    {
      ReplayKitUnity.Instance.onStopScreenCaptureWithFile += OnStopCallback;
      ReplayKitUnity.Instance.onStartScreenCapture += OnStartRecording;
    }


  }

  // Call back that is triggered from iOS native 
  public void OnStartRecording()
  {
    if (!isRecording)
    {
      isRecording = true;
    }

    StartCoroutine("AskQuestions");
  }


  // You will recieve the file path to the recorded gameplay session here 
  public void OnStopCallback(string files)
  {
    isRecording = false;

    // Play the recorded video 
    //recordCallback(file);
    app.log("FilePath is totally rthis  " + files);

    app.VideoURL = files.Remove(0, 7);
    GoToShare();
  }

  void Update()
  {
    // Handle screen touches.
    if (Input.touchCount > 0)
    {
      Touch touch = Input.GetTouch(0);
      if (touch.phase == TouchPhase.Began)
      {
        ReplayKitUnity.StartRecording();
      }

      if (touch.phase == TouchPhase.Ended)
      {
        ReplayKitUnity.StopRecording();
      }
    }
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

    //NativeCamera.RecordVideo(recordCallback, NativeCamera.Quality.Default, 0, 0);
  }

  void recordCallback(string filepath)
  {
    if (filepath != null)
    {

    }
    else
    {
      app.log("filepath was null");
    }
  }

  void GoToShare()
  {
    SceneManager.LoadScene("3_ReviewShare");
  }

  IEnumerator AskQuestions()
  {
    while (currentQuestion <= questions.Length)
    {
      questionText.text = questions[currentQuestion];

      questionText.transform.DOPunchScale(Vector3.one * 2, 2.0f, 10, 1);

      yield return new WaitForSeconds(questionSeconds);

      currentQuestion++;
    }
  }
}
