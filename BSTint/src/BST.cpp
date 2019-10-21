#include <iostream>
#include <vector>
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

//Return size of the BST
int BST::getSize()
{
    return this->CountAllSuccessors(this->root) + 1;
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

//Print BST in inorder sequence, like left->root->right
//root  --->  starting node
void BST::PrintInOrder(BSTNode* root)
{
    if(root == NULL)
        return;
    PrintInOrder(root->getLeftChild());

    cout << root->getValue() << " ";

    PrintInOrder(root->getRightChild());
}

//Print BST in preorder sequence, like root->left->right
//root  --->  starting node
void BST::PrintPreOrder(BSTNode* root)
{
    if(root == NULL)
        return;
    cout << root->getValue() << " ";

    PrintInOrder(root->getLeftChild());
    PrintInOrder(root->getRightChild());
}

//Prints tree from the specified NODE given by KEY
//from low to high if ORDER not specified
void BST::PrintTreeFromNode(int key, bool ascendingOrder)
{
    BSTNode* node = this->Search(key);
    //if node == root - prints the whole tree
    if(node)
    {
        if(ascendingOrder) PrintTreeASC(node);
        else PrintTreeDESC(node);
        cout << endl;
    }
    else cout << "Node with key=" << key << "wasn't found\n";
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

//
/*int* BST::CreateArray()
{
    int size = this->getSize();
    int* values = new int[size];
    return CreateArray(this->root, values, -1);
}

//Store nodes values in inorder sequence
int* BST::CreateArray(BSTNode* node, int* arr, int iter)
{
    //iter++;
    if(node == NULL)
        return arr;
    CreateArray(node->getLeftChild(), arr, iter);

    arr[++iter] = node->getValue();

    CreateArray(node->getRightChild(), arr, iter);
    
    return arr;
}*/

//Returns NULL if node with KEY not found
//Returns pointer to node otherwise
BSTNode* BST::Search(int key)
{
    BSTNode* result = Search(root,key);

    result == NULL ? NULL : result;
}

//Returns NULL if node with KEY not found
//Returns pointer to node otherwise
BSTNode* BST::Search(BSTNode* node, int key)
{
    if(node == NULL) return NULL;
    
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
int BST::getSizeofLeftSubtree(BSTNode* node)
{
    int count = 0;
    if(node == NULL) return -1;
    if(node->getLeftChild())
    {
        //find all successors of this->left
        count += 1 + CountAllSuccessors(node->getLeftChild());
    }

    return count;
}

/*
Returns amount of direct and indirect
successors of NODE
*/
int BST::CountAllSuccessors(BSTNode* node)
{
    int count=0;
    if(node->getLeftChild())
    {
        count += 1+ CountAllSuccessors(node->getLeftChild());
    }
    if(node->getRightChild())
    {
        count += 1+ CountAllSuccessors(node->getRightChild());
    }
    
    return count;
}

BSTNode* BST::kth_Smallest(int key) 
{ 
    return kth_Smallest(root, key);
}

//Introduce changes to getSizeofLeftSubtree function
//kth_Smallest may get broken
BSTNode* BST::kth_Smallest(BSTNode* node, int key)
{
    if(key == getSizeofLeftSubtree(node) + 1)
    {
        return node;
    }
    else if (key > getSizeofLeftSubtree(node))
    {
        key -= getSizeofLeftSubtree(node)+1;
        return kth_Smallest(node->getRightChild(), key);
    }
    else
    {
        return kth_Smallest(node->getLeftChild(), key);
    }
    
}

//Returns parent fpr a CHILD node or NULL if it's root
BSTNode* BST::findParent(BSTNode* child)
{
    if(child == root){
        return NULL;
    }
    else
        return findParent(root,child);
}

BSTNode* BST::findParent(BSTNode* parent, BSTNode* child)
{
    if(parent->getLeftChild() == child ||
       parent->getRightChild() == child)
       return parent;
    else if(parent->getValue() > child->getValue())
        findParent(parent->getLeftChild(), child);
    else findParent(parent->getRightChild(), child);
}

/*
Change roles of parent(given by key) and it's left child
Return pointer to exChild of parent
Return NULL if node wasn't found
key - reference to parent
*/
BSTNode* BST::RotateRight(int key)
{
    BSTNode* node = this->Search(key);
    if(node)
    {
        return RotateRight(node);
    }
    else return NULL;
}

/*
Changes roles of PARENT & LEFT CHILD
*/
BSTNode* BST::RotateRight(BSTNode* parent)
{
    if(parent == NULL) return NULL;
    else
    {
        //get child
        BSTNode* child = parent->getLeftChild();
        //find parent for parent
        BSTNode* parentParent = findParent(parent);
                
        parent->setLeftChild(child->getRightChild());
        //changing roles
        child->setRightChild(parent);
        //give parent of parent new child
        if(parentParent)
        {
            if(parent->getValue() < parentParent->getValue())
            {
                parentParent->setLeftChild(child);
            }
            else parentParent->setRightChild(child);
        }
        else
            this->root = child;

        return child; 
    } 
}

/*
Change roles of parent(given by key) and it's right child
Return pointer to exChild of parent
Return NULL if node wasn't found
key - reference to parent
*/
BSTNode* BST::RotateLeft(int key)
{
    BSTNode* node = this->Search(key);
    if(node)
    {
        return RotateLeft(node);
    }
    else return NULL;
}

/*
Change roles of PARENT & RIGHT CHILD
*/
BSTNode* BST::RotateLeft(BSTNode* parent)
{
    if(parent == NULL) return NULL;
    else
    {
        //get child
        BSTNode* child = parent->getRightChild();
        //find parent for parent
        BSTNode* parentParent = findParent(parent);
                
        parent->setRightChild(child->getLeftChild());
        //changing roles
        child->setLeftChild(parent);
        //give parent of parent new child
        if(parentParent)
        {
            if(parent->getValue()<parentParent->getValue())
            {
                parentParent->setLeftChild(child);    
            }
            else parentParent->setRightChild(child);
        }
        else this->root = child;
        // BSTNode* temp = node->getParent()->getParent();

        // node->getParent()->setright(node->getLeftChild());
        // node->getLeftChild()->setParent(node->getParent());
        // node->getParent()->getParent()->setleft(node);
        // node->getParent()->setParent(node);
        // node->setleft(node->getParent());
        
        // node->setParent(temp);        
        return child;
    }
    
}

void BST::PutInRoot(int key)
{
    BSTNode* node = this->Search(key);
    if(node)
    {
        root = PutInRoot(node);
    }
    else cout << "Node with key=" << key << " wasn't found\n";
}

//Returns new root of the BST
//TODO: find mistakes 
BSTNode* BST::PutInRoot(BSTNode* node)
{
    BSTNode* parent = findParent(node);
    while(parent != NULL)
    {
        if(node->getValue() < parent->getValue())
        {
            node = RotateRight(parent);
            parent = findParent(node);
        }
        else
        {
            node = RotateLeft(parent);
            parent = findParent(node);
        } 
    }
    return node;
}

//Return root of new balanced BST
BSTNode* BST::BalanceTree()
{
    //1. make a storage of tree nodes in sorted order
    vector<BSTNode*> nodes;
    StoreBSTNodes(this->root, nodes);
    //2. use time complexity O(n) solution to build new bst
    int N = nodes.size();
    //3. return root of new balanced BST
    return BuildBalancedBST(nodes, 0, N);
}

void BST::StoreBSTNodes(BSTNode* node, vector<BSTNode*> &nodes)
{
    if(node == NULL)
        return;

    StoreBSTNodes(node->getLeftChild(), nodes);
    
    nodes.push_back(node);

    StoreBSTNodes(node->getRightChild(), nodes);
}

//Build balanced BST and Return it's root
BSTNode* BST::BuildBalancedBST(vector<BSTNode*> &nodes, int left, int right)
{
    //Get middle element of array and make it root
    //Recursively do the same for left and right halves
        //Get the middle of left half and make if [left child]
        //of the root  
        //Get the middle of right half and make if [right child]
        //of the root  
    if(left > right) 
        return NULL;

    int mid = left + (right - left) / 2;
    BSTNode* root = nodes[mid];

    root->setLeftChild(BuildBalancedBST(nodes, left, mid-1));
    root->setRightChild(BuildBalancedBST(nodes, mid+1, right));

    return root;
}