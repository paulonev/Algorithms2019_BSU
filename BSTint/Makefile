#My first Makefile
#It's script for compiling my project files
#[all] is a special target which depends on [BST.o] and [BinarySearchTree.o]
#and has the command to make GCC link the two object files into the final executable binary [bst]
#[BST.o] is a filename target that depends on [src/BST.cpp] and [headers/BST.h],
#and has the command to compile BST.cpp to produce BST.o.
#[B..Tree.o] is a filename target that depends on [B..Tree.cpp], 
#it calls GCC to compile the [B..Tree.cpp] and produce [B..Tree.o]
#[clean] is a special target that has no dependencies, but specifies the commands 
#to clean the compilation outputs i.e object and binary files from the project directories.
#-o <file> produce output to file
#-c <file> compile source file into object
#-I is used to include the current directory (./) where header file located. 

all: BST.o BinarySearchTree.o
	g++ -g BST.o BinarySearchTree.o -o bst 

BST.o: src/BST.cpp
	g++ -g -c src/BST.cpp -I ./

BinarySearchTree.o: BinarySearchTree.cpp
	g++ -g -c BinarySearchTree.cpp

clean:
	rm -rf *.o
	rm -rf bst

