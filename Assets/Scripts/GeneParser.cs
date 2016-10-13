using UnityEngine;
using UniRx;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Genes;
using System;

public class GeneParser : MonoBehaviour {

    public List<string> GeneWords;

    public struct GeneParserData {
        public string sequence;
        public Organism organism;
    }

    public struct GeneData {
        public string word;
        public int magnitude;
        public Organism organism;
    }

    public static Queue<GeneParserData> queue;

    void Awake() {
        queue = new Queue<GeneParserData>();
        parseQueue()
            .ToObservable()
            .Where(_ => queue.Count > 0)
            .Subscribe();
    }

    public static void parse(Genome gen) {
        queue.Enqueue(
            new GeneParserData {
                organism = gen.organism,
                sequence = gen.sequence
            });
    }

    IEnumerator parseQueue() {
        while (true) {
            if (queue.Count > 0) {
                var data = queue.Dequeue();
                var parse = parseSequence(data)
                    .Subscribe(gdata => {
                        gdata[0].organism.genetics.reset();
                        foreach (var g in gdata) {
                            createGene(g).apply();
                        }
                    });
            }
            yield return null;
        }
    }

    public IObservable<List<GeneData>> parseSequence(GeneParserData data) {
        return Observable.CreateSafe<List<GeneData>>(observer => {

            var geneData = new List<GeneData>();
            foreach (var w in GeneWords) {
                geneData.Add(occurences(data, w));
            }
            observer.OnNext(geneData);
            observer.OnCompleted();

            return Disposable.Create(() => { Debug.Log("parseSequence Disposed"); });
        });
    }

    public List<string> substrings(string str) {

        List<string> subs = new List<string>();

        str.Length.times(i => {
            subs.Add(str.Substring(0, i + 1));
        });
        return subs;
    }

    public GeneData occurences(GeneParserData data, string word) {
        var occasions = new List<int>();
        //find occurences of substring in sequence

        var subs = substrings(word);

        foreach (var s in subs) {
            //Debug.Log($"{s} occurs in {data.sequence} {data.sequence.countOccurences(s)} times");
            occasions.Add(data.sequence.countOccurences(s));
        }

        return new GeneData {
            word = word,
            magnitude = occasions.Sum(),
            organism = data.organism
        };

    }

    private Gene createGene(GeneData data) {
        Debug.Log($"Creating gene: {data.word}:{data.magnitude} on {data.organism.name}");
        return getGeneFromWord(data.word, data.organism, data.magnitude);
    }

    private Gene getGeneFromWord(string word, Organism org, int magnitude) {
        switch (word) {
            case "big":
                return new Big(org, magnitude);
            case "fast":
                return new Fast(org, magnitude);
            case "healthy":
                return new Healthy(org, magnitude);
            default: return null;
        }
    }
}