public interface IUIPanel {
    void Init(UIRoot uIRoot, Enteringpoint enteringpoint, SceneLoad sceneLoad);
    void GameUpdate();
    void Enable();
    void Disable();
}
