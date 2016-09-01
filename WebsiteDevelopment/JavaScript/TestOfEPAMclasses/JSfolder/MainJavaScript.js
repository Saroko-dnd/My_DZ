

window.onload = TestOfMaxHeap;

function TestOfMaxHeap()
{
    document.getElementById('StartTestButton').onclick = StartTestFunction;

}

function StartTestFunction()
{
    const h = new MaxHeap();
    h.push(42, 15);
    h.push(15, 42);
    h.push(100, 100);
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
        if (this.root != null) {
            var CurrentDtaInsideRoot = this.root.data;
            var DetachedRoot = this.detachRoot();
            restoreRootFromLastInsertedNode(DetachedRoot);
            --this.CurrentSize;
            return CurrentDtaInsideRoot;
        }
    }

    detachRoot() {
        if (this.root != null) {
            if (this.parentNodes[0] == this.root) {
                this.parentNodes.shift();
            }
            var CopyOfRoot = this.root;
            this.root = null;
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
                this.root.left.parent = this.root;
            }
            if (detached.right == this.root) {
                this.root.right = null;
                LastInsertedNodeIsChildOfDetached = true;
            }
            else {
                this.root.right = detached.right;
                this.root.right.parent = this.root;
            }
            if (LastInsertedNodeIsChildOfDetached && this.CurrentSize > 1) {
                var BufferForParentNode = this.parentNodes[0];
                this.parentNodes[0] = this.parentNodes[1];
                this.parentNodes[1] = BufferForParentNode;
            }
            else {
                this.parentNodes.pop();
            }
        }
    }

    size() {
        return this.CurrentSize;
    }

    isEmpty() {
        if (this.CurrentSize == 0) {
            return true;
        }
        else {
            return false;
        }
    }

    clear() {
        this.root = null;
        this.parentNodes = [];
        this.CurrentSize = 0;
    }

    insertNode(node) {
        if (this.root == null) {
            this.root = node;
        }
        else {
            if (this.parentNodes[0].left == null) {
                this.parentNodes[0].left = node;
                this.parentNodes[0].left.parent = this.parentNodes[0];
            }
            else if (this.parentNodes[0].right == null) {
                this.parentNodes[0].right = node;
                this.parentNodes[0].right.parent = this.parentNodes[0];
            }
        }
        this.parentNodes.push(node);
        if (this.parentNodes[0].left != null && this.parentNodes[0].right != null) {
            this.parentNodes.shift();
        }
        ++this.CurrentSize;
    }

    shiftNodeUp(node) {
        if (node != this.root && node.parent != null) {
            var IndexOfNodeParent = -1;
            for (var Index = 0; Index < this.parentNodes.length; ++Index) {
                if (this.parentNodes[Index] == node) {
                    if (IndexOfNodeParent >= 0) {
                        var BufferForParentNode = this.parentNodes[IndexOfNodeParent];
                        this.parentNodes[IndexOfNodeParent] = this.parentNodes[Index];
                        this.parentNodes[Index] = BufferForParentNode;
                    }
                    else {
                        this.parentNodes[Index] = node.parent;
                        break;
                    }
                }
                else if (this.parentNodes[Index] == node.parent) {
                    IndexOfNodeParent = Index;
                }
            }
            if (node.left != this.root && node.right != this.root) {
                node.swapWithParent();
                this.shiftNodeUp(node);
            }
            else {
                this.root = node;
            }
        }
        else if (node.left == this.root || node.right == this.root) {
            this.root = node;
        }
    }

    shiftNodeDown(node) {
        if (node.left != null) {
            node.left.swapWithParent();
            var IndexOfFormerRoot = this.parentNodes.indexOf(node);
            var IndexOfFormerRootNewParent = this.parentNodes.indexOf(node.parent);
            if (IndexOfFormerRoot >= 0 && IndexOfFormerRootNewParent >= 0) {
                var BufferForNode = this.parentNodes[IndexOfFormerRoot];
                this.parentNodes[IndexOfFormerRoot] = this.parentNodes[IndexOfFormerRootNewParent];
                this.parentNodes[IndexOfFormerRootNewParent] = BufferForNode;
            }
            else if (IndexOfFormerRootNewParent >= 0) {
                this.parentNodes[IndexOfFormerRootNewParent] = node;
            }
            this.shiftNodeDown(node);
        }
        else {
            var BufferForParent = node;
            while (BufferForParent.parent != null) {
                BufferForParent = BufferForParent.parent;
            }
            this.root = BufferForParent;
        }
    }
}