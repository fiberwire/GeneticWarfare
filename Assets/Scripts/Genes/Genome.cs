using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Genome {
    public List<Chromosome> chromosomes;

    public Organism organism;

    public Genome(Organism org, int chromes = 50, int chromeLength = 50) {
        organism = org;

        chromosomes = new List<Chromosome>();
        chromes.times(i => {
            chromosomes.Add(Chromosome.RandomChromosome(org, chromeLength));
        });
        GeneParser.queue.Enqueue(
            new GeneParser.GeneParserData {
                organism = org,
                sequence = sequence
            });
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

    public Genome Clone(Organism org) {
        return new Genome(org) {
            chromosomes = chromosomes
        };
    }
}
