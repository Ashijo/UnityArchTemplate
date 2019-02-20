public class GV {
    // GLOBAL VARIABLES
    public enum SCENENAMES {
        DUMMY,
        MainMenu,
        GameScene,
        StartScene
    }

    public static WS ws;

    #region Singleton

    private static GV instance;


    private GV() { }

    public static GV Instance {
        get {
            if (instance == null) instance = new GV();
            return instance;
        }
    }

    #endregion
}