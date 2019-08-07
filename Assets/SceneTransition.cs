using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void GotoMainScene()
    {
        SceneManager.LoadScene("0_Start");
    }

    public void GotoRecordScene()
    {
        SceneManager.LoadScene("2_Record");
    }
}