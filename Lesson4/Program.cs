using System;
using Unit4.CollectionsLib;

namespace Lesson4
{
    internal class Program
    {
        // n - מספר התווים בקלט
        // O(n)
        public static bool IsPattern()
        {
            bool isCorrect = true;

            char ch = (char)Console.Read();
            Stack<char> temp = new Stack<char>();

            // temp = [1 1 2 2 3
            while (ch != '*')
            {
                if (!char.IsDigit(ch))
                    isCorrect = false;

                temp.Push(ch);
                ch = (char)Console.Read();
            }
            // ch = '*'

            ch = (char)Console.Read();
            while (ch != '\n')
            {
                if (temp.IsEmpty()) // יותר איברים בחצי השני
                    isCorrect = false;

                else
                {
                    char first = temp.Pop();

                    if (temp.IsEmpty()) // אין זוג
                        isCorrect = false;

                    else
                    {
                        char second = temp.Pop();

                        if (first != second) // זוג לא זהה בראש המחסנית
                            isCorrect = false;
                        if (first != ch) // איבר נוכחי שנקלט שונה מהזוג בראש המחסנית
                            isCorrect = false;
                    }
                }


                ch = (char)Console.Read();
            }

            return isCorrect;
        }

        #region page 51 - ex2
        // n - מספר האיברים בתור
        // T(Count) = O(n)
        // T(QueCouples) = O(n^2) = לולאה מקוננת שמפעילה בתוכה את Count
        public static int Count(Queue<int> queue, int value)
        {
            int count = 0;
            Queue<int> temp = new Queue<int>();
            while (!queue.IsEmpty())
            {
                int current = queue.Remove();
                temp.Insert(current);

                if (current == value)
                    count++;
            }

            while (!temp.IsEmpty())
            {
                queue.Insert(temp.Remove());
            }

            return count;
        }
        public static Queue<int> QueCouples(Queue<int> queue)
        {
            // queue = [1 , 5 , 7]
            // temp  = [2, 2 , 1 , 5, 2, 8 , 9 , ]
            // newQ  = [1, 5]

            Queue<int> temp = new Queue<int>();
            Queue<int> newQ = new Queue<int>();

            while (queue.IsEmpty())
            {
                int current = queue.Remove();
                temp.Insert(current);

                if (Count(queue, current) + Count(temp, current) == 2 // נמצא פעמיים בתור המקורי
                    && Count(newQ, current) == 0) // לא נמצא בתור החדש - פעם ראשונה שנכנס לתור החדש
                    newQ.Insert(current);
            }
            // queue - תור ריק
            // temp - תור שמכיל את כל האיברים מהתור המקורי
            // newQ - תור שמכיל את כל האיברים שמופיעים פעמיים בתור המקורי

            while (!temp.IsEmpty())
                queue.Insert(temp.Remove());

            return newQ;
        }
        #endregion

        public static Queue<int> ReverseA(Queue<int> queue)
        {
            Stack<int> stack = new Stack<int>();
            Queue<int> newQ = new Queue<int>();

            while (!queue.IsEmpty())
            {
                stack.Push(queue.Remove());
            }

            while (!stack.IsEmpty())
            {
                newQ.Insert(stack.Pop());
            }

            return newQ;
        }

        // נבצע על התור המקורי ולא על תור חדש
        // בשביל תור חדש ניצור פעולת מעטפת שמעתיקה את התור ושולחת אותו לכאן
        public static void ReverseB(Queue<int> queue)
        {
            if (queue.IsEmpty())
                return;

            int item = queue.Remove();
            ReverseB(queue);
            queue.Insert(item);
        }

        // כשעוברים על מחסנית/תור
        // 1. ניתן לעבור רק ע"י הוצאת פריטים
        // 2. אם צריכים לשמור על המבנה, יש להשתמש במבנה עזר

        public static bool IsExists(Stack<int> stack, int value)
        {
            bool isValueExists = false;
            Stack<int> tempStack = new Stack<int>();

            while (!stack.IsEmpty() && !isValueExists)
            {
                int current = stack.Pop();
                tempStack.Push(current);

                if (current == value)
                    isValueExists = true;
            }
            // tempStack - מחסנית הפוכה למחסנית המקורית

            while (!tempStack.IsEmpty())
                stack.Push(tempStack.Pop());

            return isValueExists;
        }

        public static bool IsExists(Queue<int> queue, int value)
        {
            bool isValueExists = false;
            Queue<int> tempQueue = new Queue<int>();

            // כשעוברים על תור, אסור לעצור באמצע, כי זה יפגע בסדר של התור
            while (!queue.IsEmpty() /*&& !isValueExists*/)
            {
                int current = queue.Remove();
                tempQueue.Insert(current);

                if (current == value)
                    isValueExists = true;
            }
            // tempQueue - תור זהה לתור המקורי

            while (!tempQueue.IsEmpty())
                queue.Insert(tempQueue.Remove());

            return isValueExists;
        }

        static void Main(string[] args)
        {
            // ADT - Abstract Data Type
            // ממשק, לא יודעים איך ממומש

            // IStack (LIFO) - Push,   Pop,    Top,  IsEmpty
            // IQueue (FIFO) - Insert, Remove, Head, IsEmpty

            Stack<int> stack = new Stack<int>();
            Queue<int> queue = new Queue<int>();

            Console.WriteLine(stack); // אוטומטית מפעיל את ToString
            Console.WriteLine(stack.ToString());
        }
    }
}
