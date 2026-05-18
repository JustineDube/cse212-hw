public static class Arrays {
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Plan:
        // Step 1: Create a new array of doubles with the given 'length' to hold the multiples.
        // Step 2: Loop from index 0 up to (but not including) 'length'.
        // Step 3: For each index i, calculate the multiple: number * (i + 1).
        //         - At i=0: number * 1 = first multiple
        //         - At i=1: number * 2 = second multiple
        //         - ...and so on
        // Step 4: Store the calculated multiple in the array at position i.
        // Step 5: After the loop ends, return the completed array.

        // Step 1: Create the result array sized to 'length'
        double[] result = new double[length];

        // Step 2-4: Iterate through each index and compute the multiple
        for (int i = 0; i < length; i++)
        {
            // Each element is number multiplied by its 1-based position (i + 1)
            result[i] = number * (i + 1);
        }

        // Step 5: Return the populated array
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Plan:
        // Step 1: Determine the split index. A "rotate right by amount" means the last
        //         'amount' elements move to the front. The split index is (data.Count - amount).
        //         Example: data = {1,2,3,4,5,6,7,8,9}, amount = 3
        //           -> splitIndex = 9 - 3 = 6
        //           -> Elements from index 6 onward: {7, 8, 9}  (these go to the front)
        //           -> Elements from index 0 to 5:   {1, 2, 3, 4, 5, 6} (these follow)
        //
        // Step 2: Use GetRange to extract the two slices:
        //         - 'tail': GetRange(splitIndex, amount) -> the last 'amount' elements
        //         - 'head': GetRange(0, splitIndex)      -> all elements before the split
        //
        // Step 3: Clear the original list.
        //
        // Step 4: Add the 'tail' slice back first (it becomes the new beginning).
        //
        // Step 5: Add the 'head' slice after it (it becomes the new end).
        //         The result is a list rotated right by 'amount'.

        // Step 1: Calculate where to split the list
        int splitIndex = data.Count - amount;

        // Step 2: Extract both halves
        List<int> tail = data.GetRange(splitIndex, amount);   // last 'amount' elements
        List<int> head = data.GetRange(0, splitIndex);        // all elements before the split

        // Step 3: Clear the original list
        data.Clear();

        // Step 4: Put the tail (formerly last elements) at the front
        data.AddRange(tail);

        // Step 5: Append the head (formerly first elements) at the end
        data.AddRange(head);
    }
}
