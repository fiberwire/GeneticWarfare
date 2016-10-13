using UnityEngine;
using UniRx;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class GeneParser : MonoBehaviour {

    public Gene[] Genes;

    // Use this for initialization
    void Start() {

    }

    public IObservable<List<Gene>> parseSequence(string seq) {




        return null;
    }

    public IObservable<List<string>> substrings(string word) {
        return Observable.CreateSafe<List<string>>(observer => {
            List<string> subs = new List<string>();
            var words = from g in Genes select g.word;
            foreach (var w in words) {
                w.Length.times(i => {
                    subs.Add(w.Substring(0, i));
                });
            }
            observer.OnNext(subs);
            return Disposable.Create(() => { Debug.Log("substrings disposed"); });
        });
    }

    public IObservable<List<int>> occurences(string sub) {
        return Observable.CreateSafe<List<int>>(observer => {
            //find occurences of substring in sequence
            return Disposable.Create(() => Debug.Log("occurences disposed"));
        });
    }
}