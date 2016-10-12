using UnityEngine;
using System.Collections;
using UniRx;

public class Aging : MonoBehaviour {

    public Organism organism;

    public float age;
    public SpriteRenderer renderer;
    public Color colorNormal, colorOld;

    public float decay {
        get {
            return age/organism.stats.Longevity;
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
                renderer.color = new Color(
                    r: Mathf.Lerp(colorNormal.r, colorOld.r, decay),
                    b: Mathf.Lerp(colorNormal.b, colorOld.b, decay),
                    g: Mathf.Lerp(colorNormal.g, colorOld.g, decay),
                    a: Mathf.Lerp(colorNormal.a, colorOld.a, decay));

            });

    }

}
