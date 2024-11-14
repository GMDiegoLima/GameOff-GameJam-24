using UnityEngine;
using UnityEngine.SceneManagement;

// Used to disable dialogue and collider border on the village when the player finish the tutorial
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    public bool tutorialFinished = false;
    public GameObject dialogueTutorial;
    public GameObject borderTutorial;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Village")
        {
            if (tutorialFinished)
            {
                dialogueTutorial = GameObject.Find("DialogueTutorial");
                borderTutorial = GameObject.Find("BorderTutorial");
                dialogueTutorial.SetActive(false);
                borderTutorial.SetActive(false);
            }
            else
            {
                dialogueTutorial = GameObject.Find("DialogueTutorial");
                borderTutorial = GameObject.Find("BorderTutorial");
                dialogueTutorial.SetActive(true);
                borderTutorial.SetActive(true);
            }
        }
    }
}
