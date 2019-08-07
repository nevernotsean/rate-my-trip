using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySavedVideo : MonoBehaviour
{

    AppManager app;

    // Start is called before the first frame update
    void Start()
    {
        app = AppManager.Instance;

        //Handheld.PlayFullScreenMovie( "file://" + app.VideoURL );
        new NativeShare().AddFile( app.VideoURL ).ShareStory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
