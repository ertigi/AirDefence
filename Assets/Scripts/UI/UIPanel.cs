using UnityEngine;

public abstract class UIPanel : MonoBehaviour, IUIPanel {
    protected UIRoot _uIRoot;
    protected Enteringpoint _enteringpoint;
    protected SceneLoad _sceneLoad;

    public virtual void Init(UIRoot uIRoot, Enteringpoint enteringpoint, SceneLoad sceneLoad) {
        _uIRoot = uIRoot;
        _enteringpoint = enteringpoint;
        _sceneLoad = sceneLoad;
        LocalInit();
    }

    protected abstract void LocalInit();

    public abstract void GameUpdate();

    public virtual void Enable() {
        gameObject.SetActive(true);
    }

    public virtual void Disable() {
        gameObject.SetActive(false);
    }
}

public enum UIPanelType {
    Menu, InGame, Win, Lose
}
