using UnityEngine;
using System.Collections;

public class Genetics : MonoBehaviour {

    public Stats stats;
    public Aging aging;
    public Unit unit;

    //genetic bonuses
    public float maxHealth;
    public float maxEnergy;
    public float longevity;
    public float metabolicRate;
    public float moveSpeed;
    public float size;
    public float healthRegen;

    //decay factors
    public float healthDecayFactor;
    public float sizeDecayFactor;
    public float energyDecayFactor;
    public float moveSpeedDecayFactor;
    public float healthRegenDecayFactor;

    // Use this for initialization
    void Start() {

    }

    //zero out genetic bonuses
    public void reset() {
        maxHealth = 0;
        maxEnergy = 0;
        longevity = 0;
        metabolicRate = 0;
        moveSpeed = 0;
        size = 0;
        healthRegen = 0;
    }
}
