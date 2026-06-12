public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1
        // Plan:
        // 1. If value < current node's Data, go left:
        //    - If Left is null, create a new Node there.
        //    - Otherwise recurse into Left.Insert(value).
        // 2. If value >= current node's Data, go right:
        //    - If Right is null, create a new Node there.
        //    - Otherwise recurse into Right.Insert(value).
        // Note: duplicate values are inserted to the right.

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2
        // Plan:
        // 1. Base case: if value equals this node's Data, return true.
        // 2. If value < Data, search the left subtree.
        //    - If Left is null, value is not in the tree — return false.
        //    - Otherwise return Left.Contains(value).
        // 3. If value > Data, search the right subtree.
        //    - If Right is null, value is not in the tree — return false.
        //    - Otherwise return Right.Contains(value).

        if (value == Data)
            return true;

        if (value < Data)
            return Left != null && Left.Contains(value);

        return Right != null && Right.Contains(value);
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        // Plan:
        // 1. Base case: a leaf node (no children) has height 1.
        // 2. Recursive case: height = 1 + max(left subtree height, right subtree height).
        //    - If only Left exists:  1 + Left.GetHeight()
        //    - If only Right exists: 1 + Right.GetHeight()
        //    - If both exist:        1 + Math.Max(Left.GetHeight(), Right.GetHeight())

        if (Left is null && Right is null)
            return 1;

        if (Left is null)
            return 1 + Right!.GetHeight();

        if (Right is null)
            return 1 + Left.GetHeight();

        return 1 + Math.Max(Left.GetHeight(), Right.GetHeight());
    }
}