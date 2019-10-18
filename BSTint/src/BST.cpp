#include <iostream>
#include "include/BST.h"

using namespace std;

//BSTNode class incapsulation
BSTNode::BSTNode(int _value, BSTNode* _left, BSTNode* _right)
{
    this->value = _value;
    this->left = _left;
    this->right = _right;
    
}

//BSTNode destructor
BSTNode::~BSTNode()
{
    delete left;
    delete right;
    
}

BST::BST()
{
    root = NULL;
}

BST::~BST()
{
    delete root;
}

//Inserts node by it's key
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

    //if the given key is greater than the
    //node's key then go to right subtree from node
    if (node->getValue() < key)
    {
        node->setRightChild(Insert(node->getRightChild(), key));
    }
    
    //if the given key is smaller than the
    //node's key then go to left subtree from node
    else if (node->getValue() > key)
    {
        node->setLeftChild(Insert(node->getLeftChild(), key));
    }
}

//Prints tree from low to high if order not specified
void BST::PrintTree(bool ascendingOrder)
{
    if(ascendingOrder) PrintTreeASC(root);
    else PrintTreeDESC(root);
    cout << endl;
}

void BST::PrintTreeASC(BSTNode* node)
{
    if(node == NULL)
        return;
    
    PrintTreeASC(node->getLeftChild());

    cout << node->getValue() << "(";
    node->getLeftChild() == NULL ? 
            cout << "null," :
            cout << node->getLeftChild()->getValue() << ",";
    node->getRightChild() == NULL ? 
            cout << "null) " :
            cout << node->getRightChild()->getValue() <<") ";

    PrintTreeASC(node->getRightChild());
}

void BST::PrintTreeDESC(BSTNode* node)
{
    if(node == NULL)
        return;
    
    PrintTreeDESC(node->getRightChild());

    cout << node->getValue() << "(";
    node->getLeftChild() == NULL ? 
            cout << "null," :
            cout << node->getLeftChild()->getValue() << ",";
    node->getRightChild() == NULL ? 
            cout << "null) " :
            cout << node->getRightChild()->getValue() <<") ";

    PrintTreeDESC(node->getLeftChild());
}

//Returns NULL if node with KEY not found
//Returns pointer to node otherwise
/*BSTNode* BST::Search(int key)
{
    BSTNode* result = Search(root,key);

    result == NULL ? NULL : result;
}

//Returns NULL if node with KEY not found
//Returns pointer to node otherwise
BSTNode* BST::Search(BSTNode* node, int key)
{
    if (node == NULL) 
        return NULL;
    
    if(node->getValue() > key)
    {
        return Search(node->getLeftChild(), key);
    }
    else if(node->getValue() < key)
    {
        return Search(node->getRightChild(), key);
    }
    else return node;
}

/*
Returns size of left subtree from current node
Returns -1 if current node is null
*/
int BSTNode::getSizeofLeftSubtree()
{
    int count = 0;
    if(this == NULL) return -1;
    if(this->left)
    {
        //find all successors of this->left
        count = count + 1 + getAllSuccessors(this->left);
    }

    return count;
}

int BSTNode::getAllSuccessors(BSTNode* node)
{
    int count=0;
    if(node->getLeftChild())
    {
        count += 1+ getAllSuccessors(node->getLeftChild());
    }
    if(node->getRightChild())
    {
        count += 1+ getAllSuccessors(node->getRightChild());
    }
    
    return count;
}

BSTNode* BST::kth_Smallest(int key) 
{ 
    return kth_Smallest(root, key);
}

BSTNode* BST::kth_Smallest(BSTNode* node, int key)
{
    if(key == node->getSizeofLeftSubtree() + 1)
    {
        return node;
    }
    else if (key > node->getSizeofLeftSubtree())
    {
        key -= node->getSizeofLeftSubtree()+1;
        return kth_Smallest(node->getRightChild(), key);
    }
    else
    {
        return kth_Smallest(node->getLeftChild(), key);
    }
    
}

/*
Returns NULL if node wasn't found
Returns node with node.value = key
*/
/*BSTNode* BST::RotateRight(int key)
{
    BSTNode* node = this->Search(key);
    if(node)
    {
        return RotateRight(node);
    }
    else return NULL;
}*/

/*
Changes roles of node & node's parent vice a verse
Where node.value < node->parent.value
Also called Clock-wise rotation
*/
/*BSTNode* BST::RotateRight(BSTNode* node)
{
    if(node == NULL) return NULL;
    else
    {
        node->getParent()->setleft(node->getRightChild());
        //set new right for node
        node->setright(node->getParent());
        if(node->getParent()->getParent() == NULL)
        {
            node->getParent()->setParent(node);
            setAsRoot(node);
        }
        else 
        {
            //set new parent for node
            node->setParent(node->getParent()->getParent());
            //set new left for node's parent, if parent not null
            node->getParent()->setleft(node);
            node->getRightChild
()->setParent(node);
        }
        return node;
    }  
}

/*
Returns NULL if node wasn't found
Returns node with node.value = key
*/
/*BSTNode* BST::RotateLeft(int key)
{
    BSTNode* node = this->Search(key);
    if(node)
    {
        return RotateLeft(node);
    }
    else return NULL;
}*/

/*
Changes roles of node & node->parent vice a verse
Where node.value > node->parent.value
Also called Counter-Clockwise rotation
*/
/*BSTNode* BST::RotateLeft(BSTNode* node)
{
    if(node == NULL) return NULL;
    else
    {
        BSTNode* temp = node->getParent()->getParent();

        node->getParent()->setright(node->getLeftChild());
        node->getLeftChild()->setParent(node->getParent());
        node->getParent()->getParent()->setleft(node);
        node->getParent()->setParent(node);
        node->setleft(node->getParent());
        
        node->setParent(temp);        
        return node;
    }
    
}

BSTNode* BST::PutInRoot(int key)
{
    BSTNode* node = this->Search(key);
    if(node)
    {
        return PutInRoot(node);
    }
    else return NULL;
}

//Returns new root of the BST
//TODO: find mistakes 
BSTNode* BST::PutInRoot(BSTNode* node)
{
    while(node->getParent() != NULL)
    {
        if(node->getValue() > node->getParent()->getValue())
        {
            node = RotateLeft(node);
        }
        else if(node->getValue() < node->getParent()->getValue())
        {
            node = RotateRight(node);
        }
    }
    return this->root;
}*/