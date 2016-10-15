using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public abstract class Gene {
    public string word;
    public abstract void apply();
    public Unit unit;
    public int magnitude;

    public Gene(Unit unit, int magnitude) {
        this.unit = unit;
        this.magnitude = magnitude;
    }

    public float getBonus(float absoluteMin = 0.1f, float randomMin = 1f, float randomMax = 1.05f) {

        List<float> bonuses = new List<float>();
        magnitude.times(i => {
            if (bonuses.Any()) {
                bonuses.Add(getStackedBonus(bonuses.Sum(), absoluteMin, randomMin, randomMax));
            } else {
                bonuses.Add(getStackedBonus(0, absoluteMin, randomMin, randomMax));
            }
        });
        var debug = "";
        foreach (var g in bonuses) {
            debug += $"{g}, ";
        }
        Debug.Log(debug);
        return bonuses.Sum();
    }

    private float getStackedBonus(float bonus, float absoluteMin, float randomMin, float randomMax) {
        return bonus.absDiff(Mathf.Max(absoluteMin, bonus * UnityEngine.Random.Range(randomMin, randomMax)));
    }
}



/*
 * To add a new gene, extend this class, 
 * add a new word in Chromosome.words,
 * add a case for it in Chromosome.getGeneFromWord
 */


