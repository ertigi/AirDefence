using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class InGamePanel : UIPanel {
    [SerializeField] private List<Image> _aimImages = new List<Image>();
    [SerializeField] private Slider _healthBar;
    private List<Tween> _tweens;
    private float _showDuration = .3f;

    protected override void LocalInit() {
        _tweens = new List<Tween>(_aimImages.Count);
        for (int i = 0; i < _aimImages.Count; i++) {
            _tweens.Add(null);
        }
        HideAimImage();
        UpdateHealthBar(1f);
    }

    public override void GameUpdate() {

    }

    public void ShowHitAim() {
        ShowAim();
    }
    public void UpdateHealthBar(float value) {
        _healthBar.value = value;
    }

    private void ShowAim() {
        for (int i = 0; i < _aimImages.Count; i++) {
            if (_tweens[i] != null)
                _tweens[i].Kill();

            _tweens[i] = _aimImages[i].DOColor(new Color(1,0,0,0), _showDuration).From(Color.red);
        }
    }
    
    private void HideAimImage() {
        foreach (var item in _aimImages) {
            item.color = Color.clear;
        }
    }

}