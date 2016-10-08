using UnityEngine;
using System.Collections.Generic;
using System;


public abstract class Gene {
    public string word;
    public abstract void apply();
}

/*
 * To add a new gene, extend this class, 
 * add a new word in Chromosome.words,
 * add a case for it in Chromosome.getGeneFromWord
 */ 


