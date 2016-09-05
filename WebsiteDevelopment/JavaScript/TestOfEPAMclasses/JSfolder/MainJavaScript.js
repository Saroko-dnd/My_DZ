

window.onload = TestOfMaxHeap;

function TestOfMaxHeap()
{
    document.getElementById('StartTestButton').onclick = StartTestFunction;

}

function StartTestFunction()
{
    /*q = new PriorityQueue();
    const nodes = [
        { priority: 10, data: 1 },
        { priority: 20, data: 2 },
        { priority:  5, data: 3 },
        { priority:  0, data: 4 },
        { priority:  8, data: 5 },
        { priority: 12, data: 6 },
        { priority: 17, data: 7 },
        { priority: 15, data: 8 },
    ];

    nodes.forEach(node => q.push(node.data, node.priority));
    
    alert(q.shift());
    alert(q.shift());
    alert(q.shift());
    alert(q.shift());
    alert(q.shift());
    alert(q.shift());
    alert(q.shift());
    alert(q.shift());*/

    const h = new MaxHeap();
    h.push(42, 15);
    h.push(15, 14);
    h.push(0, 16);
    h.push(100, 100);

    alert(h.pop());
    alert(h.pop());
    alert(h.pop());
    alert(h.pop());

    var ggg = 99;
}

class Node {
    constructor(data, priority) {
        this.data = data;
        this.priority = priority;
        this.parent = null;
        this.left = null;
        this.right = null;
    }

    appendChild(node) {
        if (this.left == null) {
            node.parent = this;
            this.left = node;
        }
        else if (this.right == null) {
            node.parent = this;
            this.right = node;
        }
    }

    removeChild(node) {
        if (this.left == node) {
            this.left.parent = null;
            this.left = null;
        }
        else if (this.right == node) {
            this.right.parent = null;
            this.right = null;
        }
        else {
            throw 'Error from RemoveChild(): argument is not a child of current node!';
        }
    }

    remove() {
        if (this.parent != null) {
            this.parent.removeChild(this);
        }
    }

    swapWithParent() {
        if (this.parent != null) {
            var CopyOfLeftChild = this.left;
            var CopyOfRightChild = this.right;
            if (this == this.parent.left) {
                if (this.parent.right != null) {
                    this.parent.right.parent = this;
                }
                this.right = this.parent.right;
                this.left = this.parent;
            }
            else {
                if (this.parent.left != null) {
                    this.parent.left.parent = this;
                }
                this.left = this.parent.left;
                this.right = this.parent;
            }
            if (CopyOfLeftChild != null)
            {
                CopyOfLeftChild.parent = this.parent;
            }
            if (CopyOfRightChild != null)
            {
                CopyOfRightChild.parent = this.parent;
            }
            this.parent.left = CopyOfLeftChild;
            this.parent.right = CopyOfRightChild;
            if (this.parent.parent != null) {
                if (this.parent == this.parent.parent.left) {
                    this.parent.parent.left = this;
                }
                else {
                    this.parent.parent.right = this;
                }
            }
            var CopyOfParentParent = this.parent.parent;
            this.parent.parent = this;
            this.parent = CopyOfParentParent;
        }
    }
}

class MaxHeap {
    constructor() {
        this.root = null;
        this.parentNodes = [];
        this.CurrentSize = 0;
    }

    push(data, priority) {
        var NewNodeForHeap = new Node(data, priority);
        this.insertNode(NewNodeForHeap);
        this.shiftNodeUp(NewNodeForHeap);
    }

    pop() {
        if (this.root != null)
        {
            var CurrentDtaInsideRoot = this.root.data;
            var DetachedRoot = this.detachRoot();
            this.restoreRootFromLastInsertedNode(DetachedRoot);
            this.shiftNodeDown(this.root);
            return CurrentDtaInsideRoot;
        }
    }

    detachRoot() {
        if (this.root != null)
        {
            if (this.parentNodes[0] == this.root)
            {
                this.parentNodes.shift();
            }
            var CopyOfRoot = this.root;
            this.root = null;
            --this.CurrentSize;
            return CopyOfRoot;
        }
    }

    restoreRootFromLastInsertedNode(detached) {
        if (this.CurrentSize > 0) {
            var LastInsertedNodeIsChildOfDetached = false;
            this.root = this.parentNodes[this.parentNodes.length - 1];

            if (detached.left == this.root) {
                this.root.left = null;
                LastInsertedNodeIsChildOfDetached = true;
            }
            else {
                this.root.left = detached.left;
                if (this.root.left != null) {
                    this.root.left.parent = this.root;
                }
            }
            if (detached.right == this.root) {
                this.root.right = null;
                LastInsertedNodeIsChildOfDetached = true;
            }
            else {
                this.root.right = detached.right;
                if (this.root.right != null) {
                    this.root.right.parent = this.root;
                }
            }
            
            if (LastInsertedNodeIsChildOfDetached && this.CurrentSize > 1) {
                var BufferForParentNode = this.parentNodes[0];
                this.parentNodes[0] = this.parentNodes[1];
                this.parentNodes[1] = BufferForParentNode;
            }
            else if (this.root.parent != null)
            {        	
                this.parentNodes.reverse();
                this.parentNodes.push(this.root.parent);
                this.parentNodes.shift();
                this.parentNodes.reverse();
            }

            if (this.root.parent != null) {
                if (this.root == this.root.parent.left) {
                    this.root.parent.left = null;
                }
                else if (this.root == this.root.parent.right) {
                    this.root.parent.right = null;
                }
            }

            this.root.parent = null;
        }
    }

    size() {
        return this.CurrentSize;
    }

    isEmpty() {
        if (this.CurrentSize == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    clear() {
        this.root = null;
        this.parentNodes = [];
        this.CurrentSize = 0;
    }

    insertNode(node) {
        if (this.root == null)
        {
            this.root = node;
        }
        else
        {
            if (this.parentNodes[0].left == null)
            {
                this.parentNodes[0].left = node;
                this.parentNodes[0].left.parent = this.parentNodes[0];
            }
            else if (this.parentNodes[0].right == null)
            {
                this.parentNodes[0].right = node;
                this.parentNodes[0].right.parent = this.parentNodes[0];
            }
        }
        this.parentNodes.push(node);
        if (this.parentNodes[0].left != null && this.parentNodes[0].right != null)
        {
            this.parentNodes.shift();
        }
        ++this.CurrentSize;
    }

    shiftNodeUp(node) {
        if (node != this.root && node.parent != null && node.priority > node.parent.priority)
        {	
            var IndexOfNodeParent = -1;
            for (var Index = 0; Index < this.parentNodes.length; ++Index)
            {
                if (this.parentNodes[Index] == node)
                {
                    if (IndexOfNodeParent >= 0)
                    {
                        var BufferForParentNode = this.parentNodes[IndexOfNodeParent];
                        this.parentNodes[IndexOfNodeParent] = this.parentNodes[Index];
                        this.parentNodes[Index] = BufferForParentNode;								
                    }
                    else
                    {
                        this.parentNodes[Index] = node.parent;
                        break;
                    }
                }
                else if (this.parentNodes[Index] == node.parent)
                {
                    IndexOfNodeParent = Index;
                }
            }
            node.swapWithParent();
            this.shiftNodeUp(node);
        }
        else if (node.parent == null)
        {
            this.root = node;
        }
    }

    shiftNodeDown(node) {
        if (node != null)
        {
            var SwapWithLeftNode = true;
            var SwapNeeded = true;
            if (node.left != null && node.right != null)
            {
                if ((node.left.priority > node.right.priority) && node.priority < node.left.priority)
                {
                    SwapWithLeftNode = true;
                    SwapNeeded = true;
                }
                else if ((node.right.priority > node.left.priority) && node.priority < node.right.priority)
                {
                    SwapWithLeftNode = false;
                    SwapNeeded = true;
                }
            }
            else if (node.left != null && node.priority < node.left.priority)
            {
                SwapWithLeftNode = true;
                SwapNeeded = true;
            }
            else if (node.right != null && node.priority < node.right.priority)
            {
                SwapWithLeftNode = false;
                SwapNeeded = true;
            }
            else
            {
                SwapNeeded = false;
            }
            if (SwapNeeded)
            {
                var ThisNodeIsRoot = false;
                if (node == this.root)
                {
                    ThisNodeIsRoot = true;
                }
                if (SwapWithLeftNode)
                {
                    node.left.swapWithParent();				
                }
                else
                {
                    node.right.swapWithParent();
                }
                if (ThisNodeIsRoot)
                {
                    this.root = node.parent;
                }
                var IndexOfNode = this.parentNodes.indexOf(node);
                var IndexOfNodeParent = this.parentNodes.indexOf(node.parent);
                if (IndexOfNode >=0 && IndexOfNodeParent >= 0)
                {
                    var BufferForNode = this.parentNodes[IndexOfNode];
                    this.parentNodes[IndexOfNode] = this.parentNodes[IndexOfNodeParent];
                    this.parentNodes[IndexOfNodeParent] = BufferForNode;
                }
                else if (IndexOfNodeParent >= 0)
                {
                    this.parentNodes[IndexOfNodeParent] = node;
                }
                this.shiftNodeDown(node);
            }
        }
    }
}

class PriorityQueue {
    constructor(maxSize = 30) {
        this.maxSize = maxSize;
        this.heap = new MaxHeap();
        this.CurrentSizeOfQueue = 0;
    }

        push(data, priority) {
            if (this.CurrentSizeOfQueue < this.maxSize)
            {
                this.heap.push(data, priority);
                ++this.CurrentSizeOfQueue;

            }
            else
            {
                throw "PriorityQueue already has its maximum size";
            }
        }

        shift() {
            if (this.CurrentSizeOfQueue == 0)
            {
                throw 'PriorityQueue is empty';
            }
            else
            {

                --this.CurrentSizeOfQueue;
                return this.heap.pop();
            }
        }

        size() {

        }

        isEmpty() {
		
        }
    }
