#genetic approach of solving non-linear equations of 5 variables
#1 eq: u2*w2*x*y2*z2 + w*x*z + w2*x*y + z + u*w*x2 = -50
#2 eq: u*y*z2 + x*y*z2 + z + u*w*x*y2*z2 + w*x2*y*z2 = -50
#3 eq: u*x2*y*z + x*y2*z + u + u2*w*x2*y2*z2 + u*w*x2 = 4
#form of any solution of eq: (U,W,X,Y,Z)

import random
import math
from population import Population, Chromosome

def kth_partite_tournament_selection(k, pop):
    """
    Return len(pop) of type Chromosome fittable for crossover.
    Each of them is a winner of local k-participants tournament.
    Each chromosome MUST have a defined fitness 
    """
    N = pop.__len__()
    selected = []
    for i in range(N):
        candidates = [] #chromosomes randomly selected for tournament
        for j in range(k): #select k chromosomes for each tournament
            randIndex = random.randint(0, N-1)
            candidates.append(pop.get_chromosome(randIndex))
            # candidates[key] = pop.get_chromosomes()[randIndex]
        #sort candidates in decreasing order of their fitness and select [0] as a winner
        #keys = list(candidates.keys())
        #keys.sort() #sort candidates in increasing order of key=fitness
        candidates.sort(key=lambda ch : ch.get_fitness())
        selected.append(candidates[0]) #select winning candidate
    
    return selected


def crossover_chromosomes(mpl, chromosomes):
    """ 
    Population is a list of chromosomes selected for crossover from func above.
    
    Since it's necessary to produce more NEW chromosomes than len(population)
    and pair_crossover() produces 1 child of any 2 parents, 
    method will loop for k*N times, where k - is crossover_multiplier, or @mpl
    """
    N = len(chromosomes)
    new_population = []
    for i in range(mpl*N):
        parents = random.sample(range(N),2) #select unique random numbers
        #find out if parents are the same TODO: right comparing of diversity of genes
        parent1, parent2 = [chromosomes[parents[i]] for i in range(2)]
        sum_diff = sum([parent1.get_gene(i)-parent2.get_gene(i) for i in range(parent1.__len__())])
        if sum_diff != 0:
            new_population.append(
                pair_crossover(
                    parent1=chromosomes[parents[0]], parent2=chromosomes[parents[1]])
            )
    return new_population

def pair_crossover(parent1, parent2):
    """ 
    Parents are of type Chromosome
    Returns Chromosome

    This method is invoked on 2 randomly selected solutions of previous generation
    to produce new solution for next generation
    Uniform Probability - 0.5/0.5 for a gene of parent to be selected
    parent = [u,w,x,y,z]
    """   
    child_genes = []
    for i in range(parent1.__len__()):
        if (random.random() <= 0.5):
            child_genes.append(parent1.get_gene(gene_idx= i))
        else:
            child_genes.append(parent2.get_gene(gene_idx= i))
    return Chromosome(child_genes)

def mutate(mut_probab, chromosomes):
    """
    Chromosomes is a list of produced descendants in crossover
    Method produces mutation with certain likelihood over new chromosomes created by crossover 
    """  
    for chromosome in chromosomes:
        for i in range(chromosome.__len__()):
            val = random.random()
            if(val <= mut_probab):
                chromosome.update_gene(gene_idx= i)

    return chromosomes
            

def evolution(population, mutation_probab, crossover_multiplier, tournament_participants):
    """
    Returns population of N best-fit offsprings of previous population

    Process of evolution of population includes the following steps
    1)selection for crossover (several pairs are selected)
    2)each pair produces new offspring(s)
    3)each offspring mutate with a certain probability(change of genes in genome)
    4)next population is formed from offsprings, selecting best N elements among mutated
    """
    N = population.__len__()
    offsprings = mutate(
                mut_probab=mutation_probab, 
                chromosomes=crossover_chromosomes(
                    mpl=crossover_multiplier,
                    chromosomes=kth_partite_tournament_selection(
                        k=tournament_participants, pop=population
                    )
                )
            )
    for ch in offsprings:
        ch.set_fitness()
    offsprings.sort(key=lambda ch: ch.get_fitness())
    nextPopulation = Population().populate(offsprings[:N])
    #a slice of N best fit chromosomes which form next generation population

    return nextPopulation #returns a slice of N best fit chromosomes