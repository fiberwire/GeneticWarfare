using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Genome {

    public Unit unit;
    public string sequence;

    public Genome(Unit unit, string seq = null, int length = 100) {
        this.unit = unit;
        sequence = seq ?? RandomSequence(length);
        GeneParser.parse(this);
    }

    private string RandomSequence(int length) {
        var letters = "abcdefghijklmnopqrstuvwxyz".Split();
        var seq = "";

        length.times(i => seq += letters.random());

        return seq;
    }

    public bool Equals(Genome gen) {
        return sequence == gen.sequence;
    }

    public Genome Clone(Unit unit) {
        return new Genome(unit) {
            sequence = sequence
        };
    }
}
