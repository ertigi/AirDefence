using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turel : MonoBehaviour {
    public Transform Body;
    public Transform BarrelContainer;
    public List<Transform> Barrels;

    private List<Vector3> _barrelsStartPositions;
    private List<Tween> _tweens;

    public void Init() {
        _barrelsStartPositions = new List<Vector3>();
        _tweens = new List<Tween>();

        foreach (var item in Barrels) {
            _barrelsStartPositions.Add(item.localPosition);
            _tweens.Add(null);
        }
    }

    public void ShootAnim(int barrelIndex, float animDuration) {
        if (_tweens[barrelIndex] != null)
            _tweens[barrelIndex].Kill();

        _tweens[barrelIndex] = StartNewShootTween(barrelIndex, animDuration);
    }

    private Tween StartNewShootTween(int i, float duration) {
        return Barrels[i].transform.DOLocalMove(_barrelsStartPositions[i] + Vector3.back * .6f, duration / 2f)
            .From(_barrelsStartPositions[i])
            .SetEase(Ease.InBack)
            .OnComplete(() => {
                _tweens[i] = Barrels[i].transform.DOLocalMove(_barrelsStartPositions[i], duration / 2f);
            });
    }
}
