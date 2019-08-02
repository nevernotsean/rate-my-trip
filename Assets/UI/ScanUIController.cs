using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanUIController : MonoBehaviour
{
  AppManager app;

  void Start()
  {
    app = AppManager.Instance;
  }

  void Update()
  {

  }

  public void handleScanButtonClick()
  {
    app.log("scan button clicked");
  }
}
