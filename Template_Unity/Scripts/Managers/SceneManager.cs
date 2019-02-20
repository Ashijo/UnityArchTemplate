using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneManager {
    public delegate void InitToCall();

    private UnityAction<Scene, LoadSceneMode> lastHandler;
    private InitToCall onSceneLoadedDelegate;

    public string CurrentScene { get; private set; }

    private void LoadScene(string sceneName, UnityAction<Scene, LoadSceneMode> handler = null) {
        if (lastHandler != null) UnityEngine.SceneManagement.SceneManager.sceneLoaded -= lastHandler;
        CurrentScene = sceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        if (handler != null) {
            lastHandler = handler;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += handler;
        }
    }

    public void LoadScene(string sceneName, InitToCall handler = null) {
        onSceneLoadedDelegate = handler ?? Empty;
        LoadScene(sceneName, DelegateCaller);
    }

    private void DelegateCaller(Scene scene, LoadSceneMode load) {
        onSceneLoadedDelegate.Invoke();
    }

    private void Empty() { }

    #region singleton

    private static SceneManager instance;

    private SceneManager() {
        onSceneLoadedDelegate = Empty;

    }

    public static SceneManager Instance {
        get {
            if (instance == null)
                instance = new SceneManager();

            return instance;
        }
    }

    #endregion singleton
}