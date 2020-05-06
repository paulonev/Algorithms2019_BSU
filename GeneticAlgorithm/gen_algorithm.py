import sys
import time
import gen_operators as GA
from population import Population, Chromosome
import constants as c

######## Main Block ########

def genetic_algorithm_timeout(pop, timeout):
    elapsed_time = 0.0
    start = time.monotonic()
    generations = []
    # while(pop.get_chromosome(0).get_fitness() > 0):
    while(elapsed_time <= timeout):
        generations.append(pop)
        pop = GA.evolution(
            population= pop, 
            mutation_probab= c.MP,
            crossover_multiplier= c.CM,
            tournament_participants=c.TP
        )
        elapsed_time = time.monotonic() - start #time from the start

    i = 1
    for pop in generations:
        print("-----------------------------------------")
        print(f"Generation {i}")
        print("-----------------------------------------")
        print (pop)
        i+=1

    print(f"Solutions were evolving for {elapsed_time:0.4f} sec - using genetic algorithm approach")


def genetic_algorithm_deviation(pop, avg_dev_max):
    generations = []
    pop_count = 1
    while(pop.get_avg_dev() > avg_dev_max):
        generations.append(pop)
        pop = GA.evolution(
            population= pop, 
            mutation_probab= c.MP,
            crossover_multiplier= c.CM,
            tournament_participants=c.TP
        )
        pop_count += 1

    print("Last generation produced: ")
    print(pop)
    
    print(f"It took {pop_count} generations to approach average deviation less than {avg_dev_max}")


if __name__ == "__main__":
    POPULATION_SIZE = int(sys.argv[1])
    pop = Population(POPULATION_SIZE).populate()
    print(f"Inhabit initial GENERATION with {POPULATION_SIZE} chromosomes")
    time.sleep(2)
    # genetic_algorithm_timeout(pop, timeout=5)
    genetic_algorithm_deviation(pop, avg_dev_max=0)