using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods {

    //Calculates the absolute value of the difference of two ints
    public static int absDiff(this int from, int to) {
        return Mathf.Abs(from - to);
    }

    //Calculates the absolute value of the difference of two floats
    public static float absDiff(this float from, float to) {
        return Mathf.Abs(from - to);
    }

    //perform an action x times, passing the index to the action
    public static void times(this int x, System.Action<int> closure) {
        for (int i = 0; i < x; i++) {
            closure(i);
        }
    }

    public static string random(this string[] strings) {
        return strings[Random.Range(0, strings.Length-1)];
    }

    public static List<string> Substrings(this string str) {
        var subs = new List<string>();

        for (var i = 1; i <= str.Length; i++) {
            var sub = str.Substring(0, i);
            subs.Add(sub);
        }
        return subs;
    }

    public static int countOccurences(this string seq, string word) {
        int count = (seq.Length - seq.Replace(word, "").Length) / word.Length;
        return count;
    }

    public static List<GameObject> ToList(this GameObject[] objs) {
        var list = new List<GameObject>();
        foreach(var o in objs) {
            list.Add(o);
        }
        return list;
    }

    public static float Average(this List<Vector3> v) {
        var sum = 0f;
        v.Count.times( i => {
            sum += (v[i].x + v[i].y) / 2;
        });

        return sum / v.Count;
    }

    public static string makeString(this IEnumerable<char> chars) {
        string result = "";

        foreach (var c in chars) {
            result += c;
        }

        return result;
    }

    public static IEnumerable<string> ChunksUpto(this string str, int maxChunkSize) {
        for (int i = 0; i < str.Length; i += maxChunkSize)
            yield return str.Substring(i, Mathf.Min(maxChunkSize, str.Length - i));
    }

    public static void RotateTowards(this Transform trans, Vector3 target) {
        trans.rotation = trans.RotationTowards(target);
    }

    public static void LerpRotateTowards(this Transform trans, Vector3 target, float speed) {
        trans.rotation = trans.LerpRotationTowards(target, speed);
    }

    public static Quaternion RotationTowards(this Transform trans, Vector3 target) {
        var diff = (target - trans.position).normalized;

        var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z);
    }

    public static Quaternion LerpRotationTowards(this Transform trans, Vector3 target, float speed) {
        var rotation = trans.RotationTowards(target);
        return new Quaternion(
            w: Mathf.Lerp(trans.rotation.w, rotation.w, speed * Time.deltaTime),
            x: Mathf.Lerp(trans.rotation.x, rotation.x, speed * Time.deltaTime),
            y: Mathf.Lerp(trans.rotation.y, rotation.y, speed * Time.deltaTime),
            z: Mathf.Lerp(trans.rotation.z, rotation.z, speed * Time.deltaTime)
            );
    }

    public static void LerpMoveTowards(this Transform trans, Vector2 target, float speed) {
        var MoveInterpolation = Mathf.Min(Mathf.Max(Vector2.Distance(trans.position, target), 0.02f), 1) / 2;
        var move = Vector2.MoveTowards(trans.position, target, speed * Time.deltaTime);
        trans.position = new Vector2(
            Mathf.Lerp(trans.position.x, move.x, MoveInterpolation),
            Mathf.Lerp(trans.position.y, move.y, MoveInterpolation));
    }

    public static void MoveTowards(this Transform trans, Vector2 target, float speed) {
        var move = (target - (Vector2)trans.position).normalized * speed * Time.fixedDeltaTime;
        trans.Translate(move);
    }

    public static Vector2 MovementTowards(this Transform trans, Vector2 target, float speed) {
        return (target - (Vector2)trans.position).normalized * speed * Time.fixedDeltaTime;
    }

    public static Vector2 LerpMovementTowards(this Transform trans, Vector2 target, float speed) {
        var MoveInterpolation = Mathf.Min(Mathf.Max(Vector2.Distance(trans.position, target), 0.02f), 1) / 2;
        var move = trans.MovementTowards(target, speed);
        return new Vector2(
            Mathf.Lerp(trans.position.x, move.x, MoveInterpolation),
            Mathf.Lerp(trans.position.y, move.y, MoveInterpolation));
    }

    public static float SignedAngle(this Vector3 from, Vector3 to) {
        Vector3 n = Vector3.forward;

        float angle = Vector3.Angle(to, from);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(from, to)));

        //-179 to 180
        float signedAngle = angle * sign;

        //0 to 360
        float angle360 = (signedAngle + 180) % 360;

        return signedAngle;
    }

    public static Vector2 dir(this Vector2 from, Vector2 to) {
        return new Vector2(
            to.x - from.x,
            to.y - from.y
            );
    }

    public static Vector2 toVector2(this Vector3 v) {
        return new Vector2(v.x, v.y);
    }

    public static Vector2 RadianToVector2(this float radian) {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(this float degree) {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static Vector2 pointFrom(this Vector2 from, float angle, float distance) {
        return from + angle.DegreeToVector2() * distance;
    }

    public static Vector3 pointFrom(this Vector3 from, float angle, float distance) {
        return from + (Vector3)angle.DegreeToVector2() * distance;
    }

    public static List<int> Reversed(this List<int> list) {
        var rev = new List<int>();

        for (int i = list.Count - 1; i >= 0; i--) {
            rev.Add(list[i]);
        }

        return rev;
    }

    public static int[] Reversed(this int[] array) {
        var rev = new int[array.Length];

        for (int i = array.Length - 1; i >= 0; i--) {
            rev[array.Length - 1 - i] = array[i];
        }

        return rev;
    }

    public static Queue<GameObject> Reversed(this Queue<GameObject> q) {
        return new Queue<GameObject>(q.Reverse());
    }

    public static Queue<int> toQueue(this int[] array) {
        Queue<int> ints = new Queue<int>();

        foreach (var i in array) {
            ints.Enqueue(i);
        }

        return ints;
    }

    public static Vector2 ScreenToWorldPoint(this Vector2 v) {
        return Camera.main.ScreenToWorldPoint(v);
    }

    public static Vector2 WorldToScreenPoint(this Vector2 v) {
        return Camera.main.WorldToScreenPoint(v);
    }

    /// <summary>
    ///     returns a List of ints from the value this is called on (inclusive) to the value specified (exclusive)
    /// </summary>
    /// <param name="from">starting value (inclusive)</param>
    /// <param name="to">end value (exclusive)</param>
    /// <param name="interval"></param>
    /// <returns></returns>
    public static List<int> to(this int from, int to, int interval = 1) {
        List<int> ints = new List<int>();
        for (int i = from; i < to; i += interval) {
            ints.Add(i);
        }
        return ints;
    }

    //get all substrings of a string
    public static List<string> substrings(this string str) {

        List<string> subs = new List<string>();

        str.Length.times(i => {
            subs.Add(str.Substring(0, i + 1));
        });
        return subs;
    }
}
