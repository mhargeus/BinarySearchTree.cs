// See https://aka.ms/new-console-template for more information

//Mitt träd är inte speciellt effektivt eftersom det saknar balansen som krävs. Beroende på vilket värde som implementeras först kan "Root" bli ett
//	extremfall som inte är mer effektiv än en vanlig lista, då hela vägen till sista noden blir en vandring åt höger t.ex. Man tappar alltså poängen med Divide and Conquer sökning.
//	Worst case är en lista 
//
//
//
//
//
//


BinarySearchTree<int> testList = new BinarySearchTree<int>();
testList.Insert(1);
testList.Insert(2);
testList.Insert(3);
testList.Insert(5);
testList.Insert(19);
testList.Insert(0);
testList.Insert(6);
testList.Insert(22);
testList.Insert(13);
testList.Insert(4);
testList.Insert(32);
testList.Insert(33);

Console.WriteLine(testList.Count());
Console.WriteLine(testList.Exists(2));
Console.WriteLine(testList.Exists(1));
Console.WriteLine(testList.Exists(3));
Console.WriteLine(testList.Exists(4));
Console.WriteLine(testList.Exists(5));
Console.WriteLine(testList.Exists(0));

testList.Print();


public interface BST_G<T> where T : IComparable<T>
{
	// Remember: the most efficient tree is a balanced tree. A balanced tree has the same (or as close as possible to) amount of nodes on the left as on the right.

	// Inserts a new value to the tree
	public void Insert(T value);

	// Returns true if an object that is equal to value exists in the tree
	// Uses the IComparable<T> interface. x.CompareTo(y) == 0
	public bool Exists(T value);

	// Returns the number of objects currently in the tree
	public int Count();
}
public class BinarySearchTree<T> : BST_G<T> where T : IComparable<T>
{
	private Node<T>? Root = null;

	int counter = 0;

	public void Insert(T value)
	{
		var newNode = new Node<T>(value);


		if (Exists(value)) return; //Dubblettkontroll
		if (Root == null) { Root = newNode; counter++; return; } //Skapar Root om träd ej påbörjat

		var currentNode = Root;

		while (true)
		{
			if (currentNode.Data.CompareTo(value) > 0)  //Jämför med root, om den är lägre gå vänster, högre gå höger.
			{
				if (currentNode.LeftChild == null) { currentNode.LeftChild = newNode; counter++; return; } //om nod är tom, skapa ny
				{ currentNode = currentNode.LeftChild; }; // om nod ej är tom, gå ett steg till
			}
			else
			{
				if (currentNode.RightChild == null) { currentNode.RightChild = newNode; counter++; return; }//om nod är tom, skapa ny
				{ currentNode = currentNode.RightChild; }; // om nod ej är tom, gå ett steg till
			}
		}
	}
	public int Count()
    {
		return counter;  //returnerar counter som registrerat varje gång något lagts till i trädet.
    }

    public bool Exists(T value)
    {
		var node = Root;
		if (Root == null){return false;}
        while (node != null)
        {
			if (node.Data.CompareTo(value) == 0) return true; //Returnerar om trädets root är match
			else if (node.Data.CompareTo(value) > 0) node = node.LeftChild; // returnerar match på trädets vänstra sida
			else node = node.RightChild; //returnerar om match på trädets högra sida.
        }
		return false;

    }

	public void Print()
	{
		Queue<Node<T>?> nodes = new Queue<Node<T>?>();
		Queue<Node<T>?> newNodes = new Queue<Node<T>?>();
		nodes.Enqueue(Root);
		int depth = 0;

		bool exitCondition = false;
		while (nodes.Count > 0 && !exitCondition)
		{
			depth++;
			newNodes = new Queue<Node<T>?>();

			string xs = "[";
			foreach (var maybeNode in nodes)
			{
				string data = maybeNode == null ? " " : "" + maybeNode.Data;
				if (maybeNode == null)
				{
					xs += "_, ";
					newNodes.Enqueue(null);
					newNodes.Enqueue(null);
				}
				else
				{
					Node<T> node = maybeNode;
					string s = node.Data.ToString();
					xs += s.Substring(0, Math.Min(4, s.Length)) + ", ";
					if (node.LeftChild != null) newNodes.Enqueue(node.LeftChild);
					else newNodes.Enqueue(null);
					if (node.RightChild != null) newNodes.Enqueue(node.RightChild);
					else newNodes.Enqueue(null);
				}
			}
			xs = xs.Substring(0, xs.Length - 2) + "]";

			Console.WriteLine(xs);

			nodes = newNodes;
			exitCondition = true;
			foreach (var m in nodes)
			{
				if (m != null) exitCondition = false;
			}
		}
	}
}

public class Node<T>
{
	public T Data { get; set; }
	public Node<T>? LeftChild { get; set; }
	public Node<T>? RightChild { get; set; }


	public Node(T value)
	{
		LeftChild = null;
		RightChild = null;
		Data = value;
	}

	// A balanced tree should be as close as possible to equal amount of nodes on both sides
	// 0 is best, but +1 and -1 is ok.
	public int GetBalance()
	{
		int left = (LeftChild == null) ? 0 : LeftChild.GetBalance() + 1;
		int right = (RightChild == null) ? 0 : RightChild.GetBalance() + 1;
		return right - left;
	}
}

