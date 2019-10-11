#include <iostream>
#include "headers/BST.h"

using namespace std;

//BSTNode class incapsulation
BSTNode::BSTNode(int _value)
{
    this->value = _value;
    this->leftChild = NULL;
    this->rightChild = NULL;
    this->parent = NULL;
}

//BST class incapsulation
BST::BST()
{
    root = NULL;
}

void BST::Insert(int key)
{
    root = Insert(root, key);
}

/*
Returns new root if BST is empty
else returns old root and inserts node in the BST
*/
BSTNode* BST::Insert(BSTNode* node, int key)
{
    if (node == NULL)
    {
        BSTNode* node = new BSTNode(key);
        return node;
    }

}
