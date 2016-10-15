using System.Collections;
using UniRx;
using UnityEngine;

public class Stats : MonoBehaviour {

    public Aging aging;
    public Organism organism;
    public Genetics genetics;

    //base stats
    public float maxHealth;
    public float maxEnergy;
    public float longevity;
    public float metabolicRate;
    public float moveSpeed;
    public float reproductionRate;
    public float size;
    public float healthRegen;

    //decay factors - represents the ratio between the maximum and final values based on age (organisms will get smaller and more frail in their old age)
    public float sizeDecayFactor;
    public float healthDecayFactor;
    public float energyDecayFactor;
    public float moveSpeedDecayFactor;
    public float healthRegenDecayFactor;

    //stats properties
    public float MaxHealth { get { return (maxHealth + genetics.maxHealth) * decayFactor; } }
    public float MaxEnergy { get { return (maxEnergy + genetics.maxEnergy) * decayFactor; } }
    public float Longevity { get { return longevity + genetics.longevity; } }
    public float EnergyReq { get { return metabolicRate + genetics.metabolicRate; } }
    public float MoveSpeed { get { return (moveSpeed + genetics.moveSpeed) * decayFactor; } }
    public float Size { get { return (size + genetics.size) * decayFactor; } }
    public float HealthRegen { get { return (healthRegen + genetics.healthRegen) * decayFactor; } }

    private float decayFactor {
        get {
            var factor = Mathf.Clamp(aging.decay, 0.8f, 1f);
            return factor;
        }
    }

    void Start() {
        var update = Observable
            .EveryUpdate();

        update
            .Subscribe(_ => {
                transform.localScale = new Vector2(Size, Size);
                applyHealthAndEnergy();
            });
        
    }

    void applyHealthAndEnergy() {
        if (organism.initializedHealthAndEnergy) {
            /*
             * MaxHealth is the variable that determines the organism's max health.
             * if MaxHealth drops from something e.g. decay, then it will bring down the ratio, 
             * which organism.maxHealth will be multiplied by,
             * which will bring the two back in line.
             * same for energy.
             */ 
            //scale health and energy with maxHealth and maxEnergy so that they maintain their ratio
            float healthRatio = MaxHealth / organism.maxHealth; 
            float energyRatio = MaxEnergy / organism.maxEnergy;
            organism.maxHealth *= healthRatio;
            organism.health *= healthRatio;
            organism.maxEnergy *= energyRatio;
            organism.energy *= energyRatio;
        } else if (MaxHealth != 0 && MaxEnergy != 0) {
            organism.maxHealth = MaxHealth;
            organism.maxEnergy = MaxEnergy;
            organism.health = organism.maxHealth;
            organism.energy = organism.maxEnergy;
            organism.initializedHealthAndEnergy = true;
        }
    }
}
