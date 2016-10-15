﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Genome {
    public List<Chromosome> chromosomes;

    public Unit unit;

    public Genome(Unit unit, int chromes = 50, int chromeLength = 50, string seq = "") {
        this.unit = unit;

        chromosomes = new List<Chromosome>();

        if (seq.Equals("")) { //if no sequence is given, generate random one
            chromes.times(i => {
                chromosomes.Add(Chromosome.RandomChromosome(unit, chromeLength));
            });
        } else { //if sequence is given, split into chromosomes
            if (seq.Length > chromeLength) {
                Debug.Log($"sequence is longer than {chromeLength}");
                chromosomes.AddRange(splitSequenceIntoChromosomes(unit, seq, chromeLength));
                Debug.Log($"Chromosomes: {chromosomes.Count}");
            } else {
                chromosomes.Add(new Chromosome(unit, seq));
            }
        }

        GeneParser.parse(this);
    }

    public List<Chromosome> splitSequenceIntoChromosomes(Unit unit, string seq, int chromeLength) {
        var chromosomes = new List<Chromosome>();
        
        foreach (var subSeq in seq.ChunksUpto(chromeLength)) {
            chromosomes.Add(new Chromosome(unit, subSeq));
        }

        return chromosomes;
    }
    public string sequence {
        get {
            var list = (from c in chromosomes select c.sequence).ToList();
            var seq = "";
            foreach (var s in list) seq += s;
            return seq;
        }
    }

    //get a random half of the genome, for reproduction purposes
    public List<Chromosome> inheritance {
        get {
            Queue<int> indexes = new Queue<int>();
            List<Chromosome> half = new List<Chromosome>();
            while (indexes.Count < chromosomes.Count) {
                int i = Random.Range(0, chromosomes.Count - 1);
                if (!indexes.Contains(i)) {
                    indexes.Enqueue(i);
                }
            }

            foreach (var i in indexes) {
                half.Add(chromosomes[i]);
            }

            return half;
        }
    }

    public bool Equals(Genome gen) {
        return sequence == gen.sequence;
    }

    public Genome Clone(Unit unit) {
        return new Genome(unit) {
            chromosomes = chromosomes
        };
    }
}
