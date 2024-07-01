
using System.Data.SqlTypes;
using System.Xml;

/**
 * Prvek spojoveho seznamu
 */
public class Link
{
    /** Data prvku - jeden znak */
    public char data;
    /** Dalsi prvek spojoveho seznamu */
    public Link? next;
}

/**
 * Spojovy seznam znaku
 */
public class LinkList
{
    /** Prvni prvek seznamu */
    public Link? first;
}

/// <summary>
/// Třída reprezentující iterátor
/// </summary>
class MyIterator
{
    public LinkList list;
    Link? current;

    /// <summary>
    /// Kontruktor třídy <b>MyIterator</b>
    /// </summary>
    /// <param name="list">Seznam znaků</param>
    public MyIterator(LinkList list)
    {
        this.list = list;
    }

    /**
	 * Vlozi novy znak do seznamu
	 * @param letter znak, ktery se vlozi do seznamu 
	 */
    public void Insert(char letter)
    {
        Link newLink = new Link();
        newLink.data = letter;
        if (current == null)
        {
            newLink.next = list.first;
            list.first = newLink;
        }
        else
        {
            newLink.next = current.next;
            current.next = newLink;
        }
    }

    /**
	 * Posune aktualni prvek na dalsi v seznamu
	 */
    public void Next()
    {
        if (list.first == null)
        {
            throw new Exception();
        }
        if (current == null)
        {
            current = list.first;
        }
        else
        {
            current = current.next;
            if (current == null)
            {
                throw new Exception();
            }
        }
    }

    /**
	 * Vrati znak v aktualnim prvku seznamu
	 * @return znak v aktualnim prvku seznamu
	 */
    public char Get()
    {
        if (list.first == null)
        {
            throw new Exception();
        }
        if (current == null)
        {
            return list.first.data;
        }
        if (current.next != null)
        {
            return current.next.data;
        }
        else
        {
            throw new Exception();
        }
    }

    /**
	 * Zmeni aktualni prvek na prvni prvek seznamu
	 */
    public void MoveToFirst()
    {
        current = null;
    }

    /**
	 * Vraci true, pokud existuje nasledujici prvek seznamu
	 * @return true, pokud existuje nasledujici prvek seznamu
	 */
    public bool HasNext()
    {
        if (list.first == null)
        {
            return false;
        }
        if (current == null)
        {
            return true;
        }
        return (current.next != null);
        /*
        if (current == null)
        {
            if (list.first != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (current.next != null)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }

    /// <summary>
    /// Smaže ze seznamu prvek
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Remove()
    {
        if (list.first == null)
        {
            throw new Exception();
        }
        if (current == null)
        {
            list.first = list.first.next;
        }
        else if (current.next == null)
        {
            throw new Exception();
        }
        else
        {
            current.next = current.next.next;
        }
    }
}

class MainClass
{
    static MyIterator myList;

    public static void Main(String[] args)
    {
        myList = new MyIterator(new LinkList());
        /*
        Insert('a');
        Insert('k');
        Insert('l');
        myList.Next();
        Insert('a');
        myList.Next();
        Insert('s');
        
        myList.MoveToFirst();
        Insert('v');
        Insert('i');
        Insert('p');
        myList.Next();
        myList.Next();
        myList.Next();
        Remove();
        Remove();
        Remove();
        Remove();
        Insert('o');
        myList.Next();
        Remove();
        */

        StreamReader sr = new StreamReader("input.txt");
        StreamReader sr_inst = new StreamReader("instructions.txt");

        StreamWriter sw = new StreamWriter("output.txt");

        int c;
        while ((c = sr.Read()) != -1)
        {
            if ((char)c == '\r' || (char)c == '\n')
            {
                continue;
            }
            
            myList.Insert((char)c);
            myList.Next();
        }

        string inst;
        while ((inst = sr_inst.ReadLine()) != null)
        {
            if (inst.StartsWith("N"))
            {
                myList.Next();
            }
            else if (inst.StartsWith("R"))
            {
                myList.Remove();
            }
            else if (inst.StartsWith("B"))
            {
                myList.MoveToFirst();
            }
            else if (inst.StartsWith("I"))
            {
                char insertChar = inst.ToCharArray()[2];
                myList.Insert(insertChar);
            }
        }

        for (MyIterator my = new MyIterator(myList.list); my.HasNext(); my.Next())
        {
            sw.Write(my.Get());
        }

        sw.Flush();
        sw.Close();
    }

    /// <summary>
    /// Metoda vypíše do konzole obsah seznamu
    /// </summary>
    /// <param name="list">Vypisovaný seznam</param>
    static void PrintList(LinkList list)
    {
        for (MyIterator it = new MyIterator(list); it.HasNext(); it.Next())
        {
            Console.Write(it.Get());
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Obalová metoda pro <see cref="MyIterator.Insert(char)"/>.
    /// </summary>
    /// <param name="c">Vkládaný znak</param>
    static void Insert(char c)
    {
        myList.Insert(c);

        PrintList(myList.list);
    }

    /// <summary>
    /// Obalová metoda pro <see cref="MyIterator.Remove()"/>.
    /// </summary>
    static void Remove()
    {
        myList.Remove();

        PrintList(myList.list);
    }
}