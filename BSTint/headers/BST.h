#include <iostream>
using namespace std;

class BSTNode
{
private:
    int value;
    BSTNode* leftChild;
    BSTNode* rightChild;
    BSTNode* parent;
public:
    BSTNode(int _value);
};

class BST
{
private:
    BSTNode* root;

public:
    BST();
    void Insert(int key);
    void PrintTree();

private:
    BSTNode* Insert(BSTNode* node, int key);
};