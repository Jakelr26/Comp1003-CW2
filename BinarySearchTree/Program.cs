
using System;   // Don't use anything else than System and only use C-core functionality; read the specs!
using System.ComponentModel.Design.Serialization;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

/// <summary>
/// Implement a binary search tree 
/// 
/// Notes
/// 1) Don't rename any of the method names in this file or change their arguments or return types or their order in the file.
/// 2) If you want to add methods do this in the space indicated at the top of the Program.
/// 3) You can add fields to the structures Tree, Node, DataEntry, if you find this necessary or useful.
/// 4) Some of the method stubs have return statements that you may need to change (the code wouldn't run without return statements).
/// 
///    You can ignore most warnings - many of them result from requirements of Object-Orientated Programming or other constraints
///    unimportant for COMP1003.
///    
/// </summary>



/// <summary>
/// Declare what sort of data we store in the tree.
/// 
/// We use simple integers for convenience, but this could be anything sortable in general.
/// </summary>
class DataEntry
{
    public int data;

}


/// <summary>
/// A single node in the tree;
/// </summary>
class Node
{
    public DataEntry data;
    public Node right;
    public Node left;
}


/// <summary>
/// The top-level tree structure
/// </summary>
class Tree
{
    public Node root;
}



class Program
{

    /// THIS LINE: If you want to add methods add them between THIS LINE and THAT LINE
    


    /// Your methods go here  .... (and nowhere else)
    /// 
    
    
    /// <summary>
    /// Deletes parts of the tree
    /// checks for childrem and acts accordingly
    /// replaces children in correct order
    /// </summary>
    /// <param name="item">to delete</param>
    /// <param name="root">root of tree</param>
    static Node DeleteItemPart(Node root, Node item)
    {
        if (root != null)
        {
            if (item.data.data < root.data.data)
            {   
                root.left = DeleteItemPart(root.left, item);

            }else if (item.data.data > root.data.data)
            {   
                root.right = DeleteItemPart(root.right, item);

            }else
            {
                // 1 or none kid
                if (root.left == null)
                {
                    return root.right;

                }else if (root.right == null)
                {
                    return root.left;
                }
                Node TwoChildReplace = root.right;
                while (TwoChildReplace.left != null)
                {
                    TwoChildReplace = TwoChildReplace.left;
                }
                root.data = TwoChildReplace.data;
                root.right = DeleteItemPart(root.right, TwoChildReplace);

            }
            return root;
        }
        return null;



    }
    /// <summary>
    /// merges two trees, results in one with no duplicates
    /// uses recursion to pass tree
    /// </summary>
    /// <param name="Union">where the compiled list is stored</param>
    /// <param name="InTree">ofiginal tree</param>
    static void treeMerger(Tree Union, Node InTree)
    {
        
        if (InTree == null)
        {
            return;
        }
        else
        {
            Node ValueIn = new Node();
            ValueIn.data = InTree.data;
            InsertItem(ref Union.root, ValueIn);

            if (InTree.left != null)
            {
                treeMerger(Union, InTree.left);
            }
            if (InTree.right != null)
            {
                treeMerger(Union, InTree.right);
            }
        }


    }
    /// <summary>
    /// finds the intersection of the of two trees (sets)
    /// recurses through to find all the data
    /// </summary>
    /// <param name="direcion"></param>
    /// <param name="tree"></param>
    /// <param name="treeUnion"></param>
    static void Intersect(Node direcionallroot, Node root , Tree treeUnion, Tree results)
    {
        if (direcionallroot == null)
        {
            return;
        }
        else
        {
            if (SearchTree(root, direcionallroot.data))
            {
                Node partofIntersect = new Node();
                partofIntersect.data = direcionallroot.data ;
                InsertTree(results, partofIntersect);
            }
            if (direcionallroot.left != null)
            {
                Intersect(direcionallroot.left, root, treeUnion, results);
            }
            if (direcionallroot.right != null)
            {
                Intersect(direcionallroot.right, root, treeUnion, results);
            }
            
        }
    }
    /// <summary>
    /// Find the symetric difference of two trees that represent sets. 
    /// uses intersection and the union
    /// Search tree is used to find the specific nodes in the tree, and recursion is used to change the Value of the part being searched for
    /// </summary>
    /// <param name="direcionallroot"></param>
    /// <param name="intersect"></param>
    /// <param name="results"></param>
    static void TreeDiff(Node direcionallroot, Node intersect, Tree results)
    {
        if (direcionallroot != null && intersect != null)
        {
            
            if (SearchTree(direcionallroot, intersect.data))
            {
                Node partofIntersect = new Node();
                direcionallroot = DeleteItemPart(direcionallroot, intersect);

            }
            if (intersect.left != null)
            {
                TreeDiff(direcionallroot, intersect.left, results);
            }
            if (intersect.right != null)
            {
                TreeDiff(direcionallroot, intersect.right, results);
            }
        }
        
    }
    /// <summary>
    /// Finds the asymetricall difference of two trees
    /// searches for data in tree2 and if it finds it it doesnt add to the result tree,
    /// but if doesnt, it measn they dont match and it gets added to the tree
    /// </summary>
    /// <param name="root1"></param>
    /// <param name="tree2"></param>
    /// <param name="resultTree"></param>
    static void NormalDifference(Node root1, Node tree2, Tree resultTree)
    {
        if (root1 == null)
        {
            return;
        }
      
        if (!SearchTree(tree2, root1.data))
        {
            Node newNode = new Node();
            newNode.data = root1.data;
            InsertTree(resultTree, newNode);
        }
        NormalDifference(root1.left, tree2, resultTree);
        NormalDifference(root1.right, tree2, resultTree);
    }


    /// THAT LINE: If you want to add methods add them between THIS LINE and THAT LINE



    /// <summary>
    /// Recursively traverse a tree depth-first printing data in in-fix order.
    /// 
    /// Note that we expect the root Node as argument, not a Tree structure.
    /// Otherwise we would need an auxiliary function as we do recursion over Nodes.
    /// 
    /// In fact, the method below can print any non-empty sub-tree.
    /// 
    /// </summary>
    /// <param name="subtree">The *root node* of the tree to traverse and print</param>
    static void PrintTree(Node tree)
    {
        if (tree.left != null)
            PrintTree(tree.left);

        Console.Write(tree.data.data + "  ");

        if (tree.right != null)
            PrintTree(tree.right);
    }


    /// <summary>
    /// Compare whether the data in one Node is smaller than data in another Node. 
    /// 
    /// The data held in Nodes could be different from integers, but it must be sortable.
    /// This function/method defines when the data in Node item1 is smaller than in item2.
    /// As we assume Integers for convenience, the comparison is just the usual "smaller than".
    /// </summary>
    /// <param name="item1">First Node</param>
    /// <param name="item2">Second Node</param>
    /// <returns>True if the data in item1 is smaller than the data in item2, and false otherwise.</returns>
    static bool IsSmaller(Node item1, Node item2)
    {
        return item1.data.data < item2.data.data;
    }


    /// <summary>
    /// Predicate that checks if two Nodes hold the same value. 
    /// 
    /// As we assume Integers for convenience, the comparison is just the usual "equality" on integers.
    /// Equality could be more complicated for other sorts of data.
    /// </summary>
    /// <param name="item1">First Node</param>
    /// <param name="item2">Second Node</param>
    /// <returns>True if two Nodes have the same value, false otherwise.</returns>
    static bool IsEqual(Node item1, Node item2)
    {
        //  Fill in proper code in this method stub
        if (item1.data == null && item2.data == null)
        {
            return true;
        }
        else if (item2.data == null || item1.data == null)
        {
            return false;
        }

        if (item2.data == item1.data)
        {
            return true;
        }
        else 
        { 
            return false;
        }


    }


    /// <summary>
    /// Insert a Node into a Tree
    /// 
    /// Note that the root node has to be provided, not the Tree reference, because we do recursion over the Nodes.
    /// The function makes use of IsSmaller and would work for other sorts of data than Integers.
    /// </summary>
    /// <param name="tree">The *root node* of the tree</param>
    /// <param name="item">The item to insert</param>
    static void InsertItem(ref Node tree, Node item)
    {
        if (tree == null)                           // if tree Node is empty, make item the tree's Node
        {
            tree = item;
            return;
        }

        if (IsSmaller(item, tree))                  // if item data is smaller than tree's data
        {
            InsertItem(ref tree.left, item);        //     recursively insert into the left subtree
        }
        else if (IsSmaller(tree, item))             // if item data is larger than tree's data
        {
            InsertItem(ref tree.right, item);       //     recursively insert into the right subtree
        }

        // otherwise the item data is already in the tree and we discard it 
    }


    /// <summary>
    /// Insert a Node into a Tree
    /// 
    /// This is an auxiliary function that expects a Tree structure, in contrast to the previous method. 
    /// It always inserts on the toplevel and is not recursive. It's just a wrapper.
    /// </summary>
    /// <param name="tree">The Tree (not a Node as in InsertItem())</param>
    /// <param name="item">The Node to insert</param>
    static void InsertTree(Tree tree, Node item)
    {
        InsertItem(ref tree.root, item); // using insert item, but with different parameters.

    }


    /// <summary>
    /// Find a value in a tree.
    /// 
    /// This requires the IsEqual() predicate defined above for general data.
    /// </summary>
    /// <param name="tree">The root node of the Tree.</param>
    /// <param name="value">The Data to find</param>
    /// <returns>True if the value is found and false otherwise.</returns>


    static bool SearchTree(Node tree, DataEntry value)
    {
        //  Fill in proper code

        Node valueNode = new Node(); //makes node 
        valueNode.data = value;

        if (IsEqual(tree, valueNode)) // checks if tree (item being searched for) is in the tree, if yes return true
        {
            return true;
        }


        if (IsSmaller(tree, valueNode)) //finds if the node is too the right, if yes recurses to the right
        {
            if (tree.right == null)
            {
                return false;
            }
            else 
            {
                return SearchTree(tree.right, value);
            }

        }

        if (IsSmaller(valueNode, tree)) //finds if the node is too the left, if yes recurses to the left
        {

            if (tree.left == null)
            {
                return false;
            }
            else 
            {
                return SearchTree(tree.left, value);

            }


        }

        return true; 
    }
    




    /// <summary>
    /// Find a Node in a tree
    /// 
    /// This compares Node references not data values.
    /// </summary>
    /// <param name="tree">The root node of the tree.</param>
    /// <param name="item">The Node (reference) to be found.</param>
    /// <returns>True if the Node is found, false otherwise.</returns>
    static bool SearchTreeItem(Node tree, Node item)
    {

        if (tree == null || item == null) // checks that their is data in the perameteres 
        {
            return false;
        }

        if (IsEqual(tree, item))  // uses isEqual function to see if the item is in the tree
        {
            return true;
        }else
        {
            if (IsSmaller(tree, item))  // searches to see if the item is to the right and then will recurse to the right
            {
                if (tree.right == null)
                {
                    return false;
                }
                else
                {
                    return SearchTreeItem(tree.right, item);
                }
            }
            if (!IsSmaller(tree, item)) // checks to see if the item is too left and then will recurse to the left 
            {
                if (tree.left == null)
                {
                    return false;
                }
                else
                {
                    return SearchTreeItem(tree.left, item);

                }
            }
        }

        return false; // returns false if after all recursion, bnothing is found

    }


    /// <summary>
    /// Delete a Node from a tree
    /// </summary>
    /// <param name="tree">The root of the tree</param>
    /// <param name="item">The Node to remove</param>
    static void DeleteItem(Tree tree, Node item)
    {
        tree.root = DeleteItemPart(tree.root, item); // calls my delete part function/ method 
    }
    

    /// <summary>
    /// Returns how many elements are in a Tree
    /// </summary>
    /// <param name="tree">The Tree.</param>
    /// <returns>The number of items in the tree.</returns>
    static int Size(Tree tree)
    {

        if (tree.root == null) // if no tree, theres no nodes
        { return 0; }

        Tree Left = new Tree();  // making a left tree
        Left.root = tree.root.left;  //  sets root to left read for recusrsion 

        Tree Right = new Tree(); //making a right tree
        Right.root = tree.root.right;  // sets root to right ready for recursion

        if (tree != null) // if the tree exists, will recurse+ add 1 for the top node
        { return 1 + Size(Left) + Size(Right); }
        else
        { return 0; }


    }

        /// <summary>
        /// Returns the depth of a tree with root "tree"
        /// 
        /// Note that this function should work for any non-empty subtree
        /// </summary>
        /// <param name="tree">The root of the tree</param>
        /// <returns>The depth of the tree.</returns>
        static int Depth(Node tree)
        {

            if (tree != null) // will only run if a tree has been parsed in 
            {
         
                int leftDepth = Depth(tree.left); //recurses through to the left 

                int rightDepth = Depth(tree.right); // recurses through to the right
            
                if (leftDepth > rightDepth) // checks which one is deepest, then will adds the root node + the depth and return it
            {
                    return 1 + leftDepth;  // 
                } 
                else
                {
                    return 1 + rightDepth;
                }


            } 
            else { return 0; } // if no tree, no depth

        

       
        }


    /// <summary>
    /// Find the parent of Node node in Tree tree.
    /// </summary>
    /// <param name="tree">The Tree</param>
    /// <param name="node">The Node</param>
    /// <returns>The parent of node in the tree, or null if node has no parent.</returns>
    static Node Parent(Tree tree, Node node)
    {
        
        if (tree.root == null || node == null) // makes sure params exist
        { return null; }

        Tree Left = new Tree(); //making trees for left and right, and sets them up ready for the recursion
        Left.root = tree.root.left;
        Tree Right = new Tree();
        Right.root = tree.root.right;


        if (Left.root == null && Right.root == null) // checks for their existence,
        {
            return null;
        }
        if (Right.root != null) //  then the next two if statements will check if the next node is the child and if so, they will return the parent
        {
            if (Right.root.data.data == node.data.data)
            {
                return tree.root;
            }
        }
        if (Left.root != null)
        {
            if (Left.root.data.data == node.data.data)
            {
                return tree.root;
            }
        }
        if (tree != null)  // recursive functions dependent on if the parent is to the left or the right, and will uses the Trees at the top as params
        {
            if (node.data.data > tree.root.data.data)
            {
                return Parent(Right, node);

            }
            if (tree.root.data.data > node.data.data)
            {
                return Parent(Left, node);
            }
        }


        return null; // no trees, then no parents
    }


    /// <summary>
    /// Find the Node with maximum value in a (sub-)tree, given the IsSmaller predicate.
    /// </summary>
    /// <param name="tree">The root node of the tree to search.</param>
    /// <returns>The Node that contains the largest value in the sub-tree provided.</returns>
    static Node FindMax(Node tree)
    {
        if (tree!= null) // checks if there is a tree
        {

            while (tree.right != null) // will set the root of the tree to the right untill it finds the rightmost value which is hence the largest 
            {
                tree = tree.right;
            }
            return tree;
            
        } else
        {
            return null; // if their is no tree, then theres no max node
        }
                    
    }


    /// <summary>
    /// Delete the Node with the smallest value from the Tree. 
    /// </summary>
    /// <param name="tree">The Tree to process.</param>
    static void DeleteMin(Tree tree)
    {
        if (tree != null) // checks if their is a tree
        {
            Node current = tree.root; // sets up a Node based on the Tree, to get around the parameter being a tree

            if (current.left != null) 
            {
                while (current.left != null) // loops untill the left most (smallest) value  is foud
                {
                    current = current.left;
                }
            }

            if (current.left == null) 
            {
                Console.WriteLine(); // function is a void, and wanted to use data to show what is beind deleted, and so it made sense to write them in the function
                Console.WriteLine("****************DeleteMinimumValue*****************");
                Console.WriteLine("Deleting minumum value, of number: " + current.data.data);

                tree.root = DeleteItemPart(tree.root, current); // goes to an exterior function
            }
        }
    }


    /// SET FUNCTIONS 


    /// <summary>
    /// Merge the items of one tree with another one.
    /// Note that duplicate data entries are prohibited.
    /// </summary>
    /// <param name="tree1">A tree</param>
    /// <param name="tree2">Another tree</param>
    /// <returns>A new tree with all the values from tree1 and tree2.</returns>
    static Tree Union(Tree tree1, Tree tree2)
    {
        if ((tree1 != null) && (tree2 != null)) // checks  that there are actully two trees 
        {
            Tree treeUnion = new Tree(); // makes a tree for the union to be put in 
            treeMerger(treeUnion, tree2.root);// two links to an external function that will merge the trees together and store in treeunion
            treeMerger(treeUnion, tree1.root);
            return treeUnion; //returns the tree of merged trees
        }

        return null;
    }


    /// <summary>
    /// Find all values that are in tree1 AND in tree2 and copy them into a new Tree.
    /// </summary>
    /// <param name="tree1">The first Tree</param>
    /// <param name="tree2">The second Tree</param>
    /// <returns>A new Tree with all values in tree1 and tree2.</returns>
    static Tree Intersection(Tree tree1, Tree tree2)
    {
        if ((tree1 == null) || (tree2 == null)) // if trees dont exit, fucntion returns null 
        {
            return null;

        }
        else
        {
            Tree treeUnion = new Tree(); // two new trees, to store the union on that will be subracted uppon, and the results which is the tree to be returned 
            Tree results = new Tree();
            treeMerger(treeUnion, tree2.root); 
            treeMerger(treeUnion, tree1.root);


            Intersect(tree1.root,tree2.root, treeUnion, results);// links to a function that uses the data to recurse and fidn the intersect. 
            return results;// sends backl the result tree
        }
    }


    /// <summary>
    /// Compute the set difference of the values of two Trees (interpreted as Sets).
    /// </summary>
    /// <param name="tree1">Tree one</param>
    /// <param name="tree2">Tree two</param>
    /// <returns>The values of the set difference tree1/tree2 in a new Tree.</returns>
    static Tree Difference(Tree tree1, Node tree2) 
    {
        if (tree1 != null && tree2 != null) //only runs if params exist 
        {
            Tree resultTree = new Tree(); // makes tree
            NormalDifference(tree1.root, tree2, resultTree);// uses tree and the perameteres to find the difference
            return resultTree;// sends back to initial call 
        }
        else { return null; } // no tree, no difference 
        
    }


    /// <summary>
    /// Compute the symmetric difference of the values of two Trees (interpreted as Sets).
    /// </summary>
    /// <param name="tree1">Tree one</param>
    /// <param name="tree2">Tree two</param>
    /// <returns>The values of the symmetric difference tree1/tree2 in a new Tree.</returns>
    static Tree SymmetricDifference(Node tree1, Tree tree2)
    {
        if (tree2 == null || tree1 == null) // if no trees, no run
        {
            return null;
        }
        Tree treeUnion = new Tree(); //making some trees to transfer data into functions 
        Tree t2 = new Tree();
        t2.root = tree1;
        
        if ((tree2 != null) && (tree1 != null))
        {
            treeUnion = Union(tree2, t2); // find the union as vital for the maths
         
        }
        
        Tree inter = Intersection(tree2, t2); // find the intersection 

        Tree results = new Tree(); // to store results

        TreeDiff(treeUnion.root, inter.root, results); // function to do the maths 

        return treeUnion; // sends back to the original call 
    }



    /*  
     *  TESTING 
     */


    /// <summary>
    /// Testing of the Tree methods that does some reasonable checks.
    /// It does not have to be exhaustive but sufficient to suggest the code is correct.
    /// </summary>
    static void TreeTests()  // some tests
    {
        Tree tree = new Tree();
        Random r = new Random();
        DataEntry data;


        // Build a tree inserting 10 random values as data

        Console.WriteLine("Build a tree inserting 10 random values as data");

        for (int i = 1; i <= 10; i++)
        {
            data = new DataEntry();
            data.data = r.Next(10);

            Node current = new Node();
            current.left = null;
            current.right = null;
            current.data = data;

            InsertItem(ref tree.root, current);
            // InsertTree(tree, current);

        }

        // print out the (ordered!) tree

        Console.WriteLine("Print out the (ordered!) tree");
        PrintTree(tree.root);
        Node hold = tree.root;
        Console.WriteLine();


        // test SearchTree

        Console.WriteLine("Search for 10 random values");
        Console.WriteLine("");
        Console.WriteLine("*******************FindingElement*******************");
        data = new DataEntry();
        for (int i = 0; i < 10; i++)
        {
            data.data = r.Next(10);       // vvvv this is ugly ... improve it! vvvvv 
            Console.WriteLine(data.data + " was" + (!SearchTree(tree.root, data) ? " NOT" : "") + " found");
        }



        //  Add more tree testing here .... 
        
        // formatting is used on the text to sepparate out the different functions and make the output more readable 
        // most of this is calling the function and printing it
        Console.WriteLine("");
        Console.WriteLine("**********************Size*************************");
        Console.WriteLine("Size of/ Number of elements in Tree: " + (Size(tree)));
        
        
        Console.WriteLine("");
        Console.WriteLine("**********************Depth************************");
        Console.WriteLine("depth of the tree is: " + Depth(tree.root));
        
        
        Console.WriteLine("");
        Console.WriteLine("*******************ParentNodes*********************");
        Node parentNode = new Node();
        parentNode.data = data;

        Console.WriteLine("Top of tree = " + tree.root.data.data); // saying what the root of the tree is 
        DataEntry DoesntExist = new DataEntry();
        
        for (int i = 0; i < 10; i++) // loops through one to ten, looking for if a node exists, and if it does, it will print its value and its parent
        {
            parentNode.data.data = i;
            if ((Parent(tree, parentNode) != null))
            {
                Console.WriteLine("the parent of node " + parentNode.data.data + " is: " + (Parent(tree, parentNode).data.data));
                Console.WriteLine("");
            }
        }


        Console.WriteLine("");
        Console.WriteLine("*******************LargestNode*********************");
        Node maxNode = FindMax(tree.root);
        Console.WriteLine("The largest node is " + maxNode.data.data);

        Console.WriteLine("");
        Console.WriteLine("****************DeleteRandomValue******************");
        Node nodeDelete = new Node();
        nodeDelete.data = hold.data;
        Console.WriteLine("deleting node with value: " + nodeDelete.data.data);
        DeleteItem(tree, nodeDelete);
        Console.WriteLine("Print out the (new) tree");
        PrintTree(tree.root);
        Console.WriteLine();

        // Deleting the minimum value
        DeleteMin(tree);
        Console.WriteLine("Print out the (new) tree");
        PrintTree(tree.root);
        Console.WriteLine();
        
    }


    /// <summary>
    /// Testing of the Set methods that does some reasonable checks.
    /// It does not have to be exhaustive but sufficient to suggest the code is correct.
    /// </summary>
    static void SetTests()
    {
        Console.WriteLine();
        Console.WriteLine("****************************************************");
        Console.WriteLine("*************         SetTests         *************");
        Console.WriteLine("****************************************************");

        //Union();
        Tree tree1 = new Tree();
        Tree tree2 = new Tree();
        Random r = new Random();
        DataEntry data;


        // Build a tree inserting 10 random values as data

        Console.WriteLine("*Build two trees inserting 10 random values as data*");

        Console.WriteLine();

        //make tree1
        for (int i = 1; i <= 10; i++)
        {
            data = new DataEntry();
            data.data = r.Next(10);
            Node current = new Node();
            current.left = null;
            current.right = null;
            current.data = data;
            InsertItem(ref tree1.root, current);
        }
        //make tree2
        for (int i = 1; i <= 10; i++)
        {
            data = new DataEntry();
            data.data = r.Next(10);
            Node current = new Node();
            current.left = null;
            current.right = null;
            current.data = data;
            InsertItem(ref tree2.root, current);
        }
        Console.WriteLine("Tree1:"); // printing out the two trees for this section 
        PrintTree(tree1.root);
        Console.WriteLine();
        Console.WriteLine("Tree2:");
        PrintTree(tree2.root);

        Console.WriteLine();

        Console.WriteLine();
        Console.WriteLine("****************UnionOfTheTwoTrees*****************");
        Tree uniontree = new Tree();
        uniontree = Union(tree1, tree2);
        PrintTree(uniontree.root);
        Console.WriteLine();


        Console.WriteLine();
        Console.WriteLine("*************IntersectionOfTheTwoTrees*************");
        Tree intersectionTree = new Tree(); 
        intersectionTree = Intersection(tree1, tree2);
        PrintTree(intersectionTree.root);
        Console.WriteLine();


        Console.WriteLine();
        Console.WriteLine("*************SetDifferenceOfTheTwoTrees************");
        Tree difftree = new Tree();
        difftree = Difference(tree1, tree2.root);
        PrintTree(difftree.root);
        Console.WriteLine();


        Console.WriteLine();
        Console.WriteLine("***********SymetricDifferenceOfTheTwoTrees*********");
        Tree sdifftree = new Tree();
        sdifftree = SymmetricDifference(tree2.root, tree1);
        PrintTree(sdifftree.root);
        Console.WriteLine();


    }


    /// <summary>
    /// The Main entry point into the code. Don't change anythhing here. 
    /// </summary>
    static void Main()
    {
        TreeTests();
        SetTests();
    }

}

