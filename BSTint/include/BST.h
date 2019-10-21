///#ifndef BST_H
//#define BST_H

#include <iostream>
#include <vector>
using namespace std;

class BSTNode
{
private:
    int value;
    BSTNode* left;
    BSTNode* right;
public:
    BSTNode(int _value, BSTNode* _left=NULL, BSTNode* _right=NULL);

    int getValue()
    {
        return value;
    }
    BSTNode* getLeftChild()
    {
        return left;
    }
    void setLeftChild(BSTNode* _node)
    {
        left = _node;
    }
    BSTNode* getRightChild()
    {
        return right;
    }
    void setRightChild(BSTNode* _node)
    {
        right = _node;
    }
    
    ~BSTNode();
};

class BST
{
private:
    BSTNode* root;

public:
    BST();
    BSTNode* getRoot()
    {
        return root;
    }
    
    void Insert(int key);
    void PrintTreeFromNode(int key, bool ascendingOrder=true);
    void PrintInOrder(BSTNode* root);
    void PrintPreOrder(BSTNode* root);

    BSTNode* Search(int key);
    BSTNode* kth_Smallest(int key);
    
    BSTNode* findParent(BSTNode* child);
    BSTNode* RotateRight(int key);
    BSTNode* RotateLeft(int key);
    void PutInRoot(int key);
    
    int getSize();
    int getSizeofLeftSubtree(BSTNode* node);

    BSTNode* BalanceTree();

    ~BST();

private:
    BSTNode* Insert(BSTNode* node, int key);
    void PrintTreeASC(BSTNode* node);
    void PrintTreeDESC(BSTNode* node);
    BSTNode* Search(BSTNode* node, int key);
    BSTNode* kth_Smallest(BSTNode* node, int key);
    
    BSTNode* findParent(BSTNode* node, BSTNode* child);
    BSTNode* RotateRight(BSTNode* node);
    BSTNode* RotateLeft(BSTNode* node);
    BSTNode* PutInRoot(BSTNode* node);
    void StoreBSTNodes(BSTNode* node, vector<BSTNode*> &nodes);
    BSTNode* BuildBalancedBST(vector<BSTNode*> &nodes, int left, int right);

    int CountAllSuccessors(BSTNode* node);
};

//#endif