using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoot : MonoBehaviour {
    [SerializeField] private UIPanel _menuPanel, inGamePanel, _winPanel, _losePanel;
    private Dictionary<UIPanelType, IUIPanel> _panelsMap;
    private UIPanelType _currentPanel;

    public void Init(Enteringpoint enteringpoint, SceneLoad sceneLoad) {
        _panelsMap = new Dictionary<UIPanelType, IUIPanel> {
            [UIPanelType.Menu] = _menuPanel,
            [UIPanelType.InGame] = inGamePanel,
            [UIPanelType.Win] = _winPanel,
            [UIPanelType.Lose] = _losePanel,
        };

        foreach (var item in _panelsMap) {
            item.Value.Init(this, enteringpoint, sceneLoad);
            item.Value.Disable();
        }

        EnablePanel(UIPanelType.Menu);
    }

    public void GameUpdate() {
        foreach (var item in _panelsMap) {
            item.Value.GameUpdate();
        }
    }

    public void EnablePanel(UIPanelType panelType) {
        Debug.Log("EnablePanel: " + panelType.ToString());
        _panelsMap[_currentPanel].Disable();
        _currentPanel = panelType;
        _panelsMap[panelType].Enable();
    }

    public IUIPanel GetPanel(UIPanelType uIPanelType){
        return _panelsMap[uIPanelType];
    }
}
