using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Genes {
    class Big : Gene {

        float size;
        float metabolicRate; //increase energy requirement once it is added as a stat
        float maxHealth; //increase max health once it is added as a stat
        float moveSpeed;

        public Big(Organism org, int magnitude) : base(org, magnitude) {
            word = "big";

            size = getBonus(randomMax: 1.075f);
            moveSpeed = -getBonus(absoluteMin: 0.01f, randomMin: 0f, randomMax: 0.0025f);
            maxHealth = getBonus();
            metabolicRate = getBonus();

            //still need to get subtractive gene components working
            //moveSpeed = org.genetics.moveSpeed.absDiff(Mathf.Max(0.01f, org.genetics.moveSpeed * UnityEngine.Random.Range(0f, 0.0025f) * magnitude));
        }

        public override void apply() {
            org.genetics.size += size;
            org.genetics.moveSpeed += moveSpeed;
            org.genetics.maxHealth += maxHealth;
            org.genetics.metabolicRate += metabolicRate;
        }
    }
}
