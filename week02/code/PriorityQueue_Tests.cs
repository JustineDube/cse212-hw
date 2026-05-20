using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities and dequeue them all.
    // Expected Result: Items should come out in order of highest to lowest priority:
    //   "high"(5), "medium"(3), "low"(1)
    // Defect(s) Found: DEFECT 1 - The loop used `_queue.Count - 1` as the upper bound, which
    // skips the last element entirely. So the last item in the list was never considered as
    // a candidate for highest priority. Fix: changed to `_queue.Count`.
    // DEFECT 2 - The item was never removed from the list (`_queue.RemoveAt` was missing),
    // so Dequeue returned the value but left the item in the queue, causing Length to never
    // decrease and repeated Dequeues to return stale results. Fix: added _queue.RemoveAt(highPriorityIndex).
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("medium", 3);
        priorityQueue.Enqueue("high", 5);

        Assert.AreEqual("high", priorityQueue.Dequeue());
        Assert.AreEqual("medium", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue three items with the same priority and dequeue them all.
    // Expected Result: Items should come out in the order they were enqueued (FIFO tiebreak):
    //   "first", "second", "third"
    // Defect(s) Found: DEFECT 3 - The loop used `>=` when updating highPriorityIndex, which
    // meant every equal-priority item updated the index, so the LAST equal-priority item was
    // always selected instead of the FIRST. This broke the FIFO tiebreak requirement.
    // Fix: changed `>=` to strict `>` so the first (front-most) item wins on ties.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 5);
        priorityQueue.Enqueue("second", 5);
        priorityQueue.Enqueue("third", 5);

        Assert.AreEqual("first", priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
        Assert.AreEqual("third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items in ascending priority order (so the highest-priority item is last).
    // Expected Result: Dequeue should still return the highest priority first regardless of
    // insertion order: "five"(5), "four"(4), "three"(3), "two"(2), "one"(1)
    // Defect(s) Found: DEFECT 1 (loop stopping one short) - When the highest-priority item is
    // the last element in the list, the original loop (Count - 1) never reaches it, so a
    // lower-priority item is incorrectly returned instead. Fix: loop to Count.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("one", 1);
        priorityQueue.Enqueue("two", 2);
        priorityQueue.Enqueue("three", 3);
        priorityQueue.Enqueue("four", 4);
        priorityQueue.Enqueue("five", 5);

        Assert.AreEqual("five", priorityQueue.Dequeue());
        Assert.AreEqual("four", priorityQueue.Dequeue());
        Assert.AreEqual("three", priorityQueue.Dequeue());
        Assert.AreEqual("two", priorityQueue.Dequeue());
        Assert.AreEqual("one", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty." is thrown.
    // Defect(s) Found: No defect - the empty check was present and correct in the original code.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                               e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue a mix of priorities where a tiebreak is embedded in the middle.
    //   Enqueue: A(10), B(10), C(1)
    // Expected Result: A comes before B (both priority 10, A enqueued first), then C.
    //   "A", "B", "C"
    // Defect(s) Found: DEFECT 3 (>= tiebreak bug) - B would have been returned before A
    // because the >= comparison kept updating the index to the later equal-priority item.
    // Fix: strict > preserves A as the winner since it was found first.
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 10);
        priorityQueue.Enqueue("B", 10);
        priorityQueue.Enqueue("C", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }
}