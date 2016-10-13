using UnityEngine;
using System.Collections;
using UniRx;

public class Aging : MonoBehaviour {

    public Organism organism;

    public float age;
    public SpriteRenderer sr;
    public Color colorNormal, colorOld;

    public float decay {
        get {
            var decay = 1 - (age - (organism.stats.Longevity * 4 / 5)) / (organism.stats.Longevity / 5);
            var clamped = Mathf.Clamp(decay, 0, 1);
            return clamped;
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

    }

}
