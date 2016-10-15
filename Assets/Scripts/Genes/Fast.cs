using UnityEngine;

namespace Assets.Scripts.Genes {
    class Fast : Gene { 
        float moveSpeed;

        public Fast(Unit unit, int magnitude): base(unit, magnitude) {
            word = "fast";

            moveSpeed = getBonus(absoluteMin: 0.15f, randomMax: 1.03f);
        }

        public override void apply() {
            unit.genetics.moveSpeed += moveSpeed;
        }
    }
}
