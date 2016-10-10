using UnityEngine;

namespace Assets.Scripts.Genes {
    class Healthy : UnitGene {

        float maxHealth;
        float healthRegen;

        Organism org;

        public Healthy(Organism org, float magnitude) {
            word = "healthy";
            this.org = org;
            maxHealth = org.genetics.maxHealth.absDiff(Mathf.Max(0.3f, org.genetics.maxHealth * Random.Range(1f, 1.01f) * magnitude));
            healthRegen = org.genetics.healthRegen.absDiff(Mathf.Max(0.15f, org.genetics.healthRegen * Random.Range(1f, 1.01f) * magnitude));
        }

        public override void apply() {
            org.genetics.maxHealth += maxHealth;
            org.genetics.healthRegen += healthRegen;
        }
    }
}
