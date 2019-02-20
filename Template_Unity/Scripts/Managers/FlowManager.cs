public class FlowManager {
    private Flow currentFlow;

    public GV.SCENENAMES currentScene;
    private bool flowInitialized;
    private ToCallOnStart toCall;
    
    public void InitializeFlowManager(GV.SCENENAMES _scene) {
        TimerManager.Instance.Init();

        currentScene = _scene;
        currentFlow = CreateFlow(_scene);
    }

    public void Update(float _dt) {
        TimerManager.Instance.Update(_dt);

        if (currentFlow != null && flowInitialized)
            currentFlow.UpdateFlow(_dt, InputManager.Instance.Update());
    }

    public void FixedUpdate(float _dt) {
        if (currentFlow != null && flowInitialized)
            currentFlow.FixedUpdateFlow(_dt);
    }

    /// <summary>
    /// Switch flow and scene
    /// </summary>
    /// <param name="_flowToLoad">name of the scene (you have to update CreateFlow() to manage new scenes/flows)</param>
    /// <param name="toCall">Optionnal, this delegate will be call just after the scene generation</param>
    public void ChangeFlows(GV.SCENENAMES _flowToLoad, ToCallOnStart toCall = null) {
        this.toCall = toCall ?? Empty;
        flowInitialized = false;
        currentFlow.Finish();
        currentFlow = CreateFlow(_flowToLoad);
    }

    private Flow CreateFlow(GV.SCENENAMES _flow) {
        Flow flow;

        switch (_flow) {
            case GV.SCENENAMES.StartScene:
                flow = new StartSceneFlow();
                break;
            case GV.SCENENAMES.MainMenu:
                flow = new MainMenuFlow();
                break;
            case GV.SCENENAMES.GameScene:
                flow = new GameFlow();
                break;
            default:
                flow = null;
                break;
        }

        if (flow != null)
            SceneManager.Instance.LoadScene(_flow.ToString(), SceneLoaded);

        return flow;
    }

    private void SceneLoaded() {
        toCall.Invoke();
        currentFlow.InitializeFlow();
        flowInitialized = true;
    }

    public delegate void ToCallOnStart();

    private void Empty() { }

    #region Singleton

    private static FlowManager instance;

    private FlowManager() {
        toCall = new ToCallOnStart(Empty);
    }

    public static FlowManager Instance {
        get {
            if (instance == null)
                instance = new FlowManager();

            return instance;
        }
    }

    #endregion
}