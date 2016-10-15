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

        public Big(Unit unit, int magnitude) : base(unit, magnitude) {
            word = "big";

            size = getBonus(randomMax: 1.075f);
            moveSpeed = -getBonus(absoluteMin: 0.01f, randomMin: 0f, randomMax: 0.0025f);
            maxHealth = getBonus();
            metabolicRate = getBonus();

            //still need to get subtractive gene components working
            //moveSpeed = unit.genetics.moveSpeed.absDiff(Mathf.Max(0.01f, unit.genetics.moveSpeed * UnityEngine.Random.Range(0f, 0.0025f) * magnitude));
        }

        public override void apply() {
            unit.genetics.size += size;
            unit.genetics.moveSpeed += moveSpeed;
            unit.genetics.maxHealth += maxHealth;
            unit.genetics.metabolicRate += metabolicRate;
        }
    }
}
