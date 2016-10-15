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
        public Unit unit;
    }

    public struct GeneData {
        public string word;
        public int magnitude;
        public Unit unit;
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
                unit = gen.unit,
                sequence = gen.sequence
            });
    }

    IEnumerator parseQueue() {
        while (true) {
            if (queue.Count > 0) {
                var data = queue.Dequeue();
                var parse = parseSequence(data)
                    .Subscribe(gdata => {
                        gdata[0].unit.genetics.reset();
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
            unit = data.unit
        };

    }

    private Gene createGene(GeneData data) {
        Debug.Log($"Creating gene: {data.word}:{data.magnitude} on {data.unit.name}");
        return getGeneFromWord(data.word, data.unit, data.magnitude);
    }

    private Gene getGeneFromWord(string word, Unit unit, int magnitude) {
        switch (word) {
            case "big":
                return new Big(unit, magnitude);
            case "fast":
                return new Fast(unit, magnitude);
            case "healthy":
                return new Healthy(unit, magnitude);
            default: return null;
        }
    }
}