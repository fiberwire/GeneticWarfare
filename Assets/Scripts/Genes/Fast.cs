using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fast : Gene { 
        float moveSpeed;

        public Fast(Organism org, int magnitude): base(org, magnitude) {
            word = "fast";

            moveSpeed = getBonus(absoluteMin: 0.15f, randomMax: 1.03f);
        }

        public override void apply() {
            org.genetics.moveSpeed += moveSpeed;
        }
    }
}
