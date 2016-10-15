using UnityEngine;
using System.Collections;
using UniRx;

public class Aging : MonoBehaviour {

    public Unit unit;

    public float age;
    public SpriteRenderer sr;
    public Color colorNormal, colorOld;

    public float decay {
        get {
            var decay = 1 - ((age - (unit.stats.Longevity * 4 / 5)) / (unit.stats.Longevity / 5));
            return Mathf.Max(0f, decay);
        }
    }

    void Start() {

        var update = Observable.EveryUpdate();

        //increment age over time
        update
            .Subscribe(_ => {
                age += Time.deltaTime;
            });

        //update color according to age
        update
            .Subscribe(_ => {
                sr.color = new Color(
                    r: Mathf.Lerp(colorNormal.r, colorOld.r, 1 - decay),
                    b: Mathf.Lerp(colorNormal.b, colorOld.b, 1 - decay),
                    g: Mathf.Lerp(colorNormal.g, colorOld.g, 1 - decay),
                    a: Mathf.Lerp(colorNormal.a, colorOld.a, 1 - decay));

            });

        //damage unit over time depending on decay
        update
            .Where(_ => decay < 0.8f)
            .Subscribe(_ => {
                var damage = unit.maxHealth * 0.05f * (1 - decay) * Time.deltaTime;
                Debug.Log($"Old Age Damage: {damage}");
                unit.DoDamage(damage);
        });

    }

}
