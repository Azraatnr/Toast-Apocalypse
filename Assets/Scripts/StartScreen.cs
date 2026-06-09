using UnityEngine;
using UnityEngine.SceneManagement;

// attached to the StartScreenManager object in the StartScreen scene
// the play button calls this StartGame() via its OnClick event
public class StartScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); //this is loading the main game scene
    }
}