﻿using UnityEngine;

namespace Assets.Scripts.Genes {
    class Healthy : Gene {

        float maxHealth;
        float healthRegen;

        public Healthy(Organism org, int magnitude) : base(org, magnitude) {
            word = "healthy";

            maxHealth = getBonus(absoluteMin: 0.3f, randomMax: 1.01f);
            healthRegen = getBonus(absoluteMin: 0.15f, randomMax: 1.01f);
        }

        public override void apply() {
            org.genetics.maxHealth += maxHealth;
            org.genetics.healthRegen += healthRegen;
        }
    }
}
