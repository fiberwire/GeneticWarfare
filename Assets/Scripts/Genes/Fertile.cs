using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fertile : Gene {
        Organism org;
        float reproductionRate;

        public Fertile(Organism org, float magnitude) {
            word = "fertile";
            this.org = org;
            reproductionRate = org.genetics.reproductionRate.absDiff(Mathf.Max(0.1f, org.genetics.reproductionRate * Random.Range(1f, 1.01f)) * magnitude);
        }

        public override void apply() {
            org.genetics.reproductionRate += reproductionRate;
        }
    }
}
