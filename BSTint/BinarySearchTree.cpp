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
    tree->PrintTreeFromNode(nodeVal);

    //cout << tree->Search(8)->getSizeofLeftSubtree() << endl;
    
    int k = 3;
    cout << k << "-th smallest element of tree=" << tree->kth_Smallest(k)->getValue() << endl;
    
    cout << "Root of the tree: " << tree->getRoot()->getValue() << endl;
    tree->PrintTreeFromNode(10);

    ////tree->RotateLeft(9);
    //tree->RotateLeft(22);
    //tree->PrintTree();

    //cout << "old root: " << tree->getRoot()->getValue() << endl;
    //tree->PutInRoot(3);
    //cout << "new root: " << tree->getRoot()->getValue() << endl;

    //tree->PrintTree();
    return 0;
}

/*int key = 3;
    cout << "Find node "<< key <<"\n";
    BSTNode* result = tree->Search(key);
    if(result){
        result->getLeftChild() == NULL ? 
            cout << "Left child of this node is NULL\n" :
            cout << "Left child of this node is " << result->getLeftChild()->getValue() << endl;

        result->getRightChild() == NULL ?
            cout << "Right child of this node is NULL\n" :
            cout << "Right child of this node is " << result->getRightChild()->getValue() << endl;
        
    }else cout << "Node with key=" + to_string(key) + " wasn't found in" << endl;
    */