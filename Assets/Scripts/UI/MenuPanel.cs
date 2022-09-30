using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : UIPanel {
    [SerializeField] private Button _startGameButton;

    protected override void LocalInit() {
        _startGameButton.onClick.AddListener(StartGameButtonAction);
    }

    public override void GameUpdate() {

    }

    private void StartGameButtonAction() {
        _enteringpoint.StartGame();
        _uIRoot.EnablePanel(UIPanelType.InGame);
    }
}
