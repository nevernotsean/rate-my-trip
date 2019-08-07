using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
  private static AppManager instance = null;

  public string VideoURL;

  // Game Instance Singleton
  public static AppManager Instance
  {
    get
    {
      return instance;
    }
  }

  private void Awake()
  {
    // if the singleton hasn't been initialized yet
    if (instance != null && instance != this)
    {
      Destroy(this.gameObject);
      return; //Avoid doing anything else
    }

    instance = this;
    DontDestroyOnLoad(this.gameObject);
  }

  /// Public 

  void Start()
  {
    console.Clear();
    // Debug.Log("SINGLETON Start");
  }

  void Update()
  {
    // Debug.Log("SINGLETON Update");
  }


  // debugging
  public List<string> console;

  public void log(string message)
  {
    if (console.Count > 5)
    {
      console.RemoveAt(0);
    }
    console.Add(DateTime.Now.ToString("hh:mm:ss") + ": " + message);
    print(message);
  }

  private void OnGUI()
  {
    for (int i = 0; i < console.Count; i++)
    {
      var message = console[i];
      GUI.Label(new Rect(20, 20 * i, Screen.width, 200), message);
    }
  }
}
