using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Genes;

public class Chromosome {
    public string sequence;
    private Organism org;
    private static readonly string[] letters = new string[] {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
    };

    public Chromosome(Organism org, string sequence) {
        this.org = org;
        this.sequence = sequence;
    }

    public static Chromosome RandomChromosome(Organism org, int length) {
        return new Chromosome(org, RandomSequence(length));
    }

    public static string RandomSequence(int length) {
        var seq = "";

        length.times(i => {
            seq += letters.random();
        });

        return seq;
    }

}