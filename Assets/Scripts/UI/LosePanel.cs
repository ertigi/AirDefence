using UnityEngine;
using UnityEngine.UI;

public class LosePanel : UIPanel {
    [SerializeField] private Button _restartButton;

    protected override void LocalInit() {
        _restartButton.onClick.AddListener(() => _sceneLoad.Restart());
    }

    public override void GameUpdate() {

    }
}
