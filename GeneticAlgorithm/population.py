import random
import functions as funlib

UPPER_BOUND = 300
LOWER_BOUND = -300
OPTIMAL_SOLUTION = -50 #-50

class Chromosome:
    def __init__(self, genes=[]):
        self.genes = genes
        self.fitness = 0 if len(genes) == 0 else fitness_compute(self.genes)
        
    def update_gene(self, gene_idx):
        if(gene_idx < len(self.genes)): 
            self.genes[gene_idx] = random.randint(LOWER_BOUND, UPPER_BOUND)
        else:
            print("update_gene() -> index of gene is out of range")
            
    def get_genes(self):
        return self.genes
    def get_gene(self, gene_idx):
        if(gene_idx < len(self.genes)):
            return self.genes[gene_idx]
        else:
            print("get_gene() -> index of gene is out of range")
    def set_fitness(self):
        self.fitness = fitness_compute(self.genes)
    def get_fitness(self):
        return self.fitness
    def __str__(self):
        return str(self.genes)
    def __len__(self):
        return len(self.genes)


class Population:       
    def __init__(self, size=0):
        self.size = size
        self.chromosomes = []

    def populate(self, population=[]):    
        """
        population contains elems of type Chromosome

        Method adds chromosomes to population, computes fitness and creates a dictionary
        {fitness : chromosome}
        """
        if(len(population) == 0):
            population = generate_population(self.size) #generate [[x1],[x2],[x3],...]
            for x in population:
                self.chromosomes.append(Chromosome(x))
        else:
            self.chromosomes = population
        
        return self

    # def fit_population(self):
    #     """
    #     This method invokes calculation of fitness of each chromosome in population
    #     """
    #     for chromosome in self.chromosomes:
    #         chromosome.set_fitness()

    # def update_gene(self, chr_idx, gene_idx):
    #     new_val = myrand.randint(LOWER_BOUND, UPPER_BOUND+1)
    #     self.chromosomes[chr_idx][gene_idx] = new_val

    def get_chromosomes(self) : return self.chromosomes
    
    def get_chromosome(self, chrom_idx):
        if(chrom_idx < len(self.chromosomes)): return self.chromosomes[chrom_idx]
        else: print("get_chromosome() -> chromosome index is out of range")

    def __str__(self):
        return self.print_residual()
    
    def __len__(self):
        return len(self.chromosomes)
    
    def print_residual(self):
        """
        Utility function for printing a residual vector - deviations of each chromosome from OPTIMAL and average deviation of population
        """
        s = ""
        i = 1
        for x in self.chromosomes:
            s += str.format(f"Chromosome {i}: {x} | Deviation: {x.get_fitness()}\n")
            i+=1
        return s
        # print("Average deviation of population: ", avg_dev/ self.__len__())
    
    def get_avg_dev(self):
        sum = 0.0
        for ch in self.chromosomes:
            sum += ch.get_fitness()
        return sum / self.__len__()


# @function for generating an initial population
# @population is a list of lists, where lists are possible solutions
def generate_population(N):
    return [[random.randint(LOWER_BOUND, UPPER_BOUND) for j in range(5)] for i in range(N)]

def fitness_compute(chromosome):
    return abs(funlib.diaf1(chromosome) - OPTIMAL_SOLUTION)