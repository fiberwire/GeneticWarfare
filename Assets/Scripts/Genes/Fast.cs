using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fast : Gene {
        Organism org;
        float moveSpeed;

        public Fast(Organism org, float magnitude) {
            word = "fast";
            this.org = org;
            moveSpeed = org.genetics.moveSpeed.absDiff(Mathf.Max(0.15f, org.genetics.moveSpeed * Random.Range(1f, 1.03f)) * magnitude);
        }

        public override void apply() {
            org.genetics.moveSpeed += moveSpeed;
        }
    }
}
