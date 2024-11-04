using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGraveyard : MonoBehaviour
{
    public void loadGrave()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
