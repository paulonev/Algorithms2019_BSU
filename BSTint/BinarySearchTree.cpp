#include <iostream>
#include "include/BST.h"
using namespace std;

int main(void)
{
    //Here is main entry of program
    //Let's create a list of keys, then add them to the tree
    //in instances of nodes and print tree 

    int keys[] = {10,5,9,3,-5,22};
    BST* tree = new BST();

    for(const int& key : keys)
    {
        tree->Insert(key);
    }

    tree->Insert(8);

    int nodeVal = 5;
    cout << "SubTree from root=" << nodeVal << ": ";
    BSTNode* nodet = tree->Search(nodeVal);
    tree->PrintInOrder(nodet);
    cout << endl;

    cout << "Tree:\n";
    tree->PrintTreeFromNode(tree->getRoot());
    cout << "Root - " << tree->getRoot()->getValue() << endl;

    BST* newTree = tree->BalanceTree();
    cout << "Balanced tree:\n";
    newTree->PrintTreeFromNode(newTree->getRoot());
    cout << "Root - " << newTree->getRoot()->getValue() << endl;

    return 0;
}

