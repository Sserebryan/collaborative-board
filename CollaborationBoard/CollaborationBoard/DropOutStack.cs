using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollaborationBoard
{
    public class Stack<T>
    {
        private T[] items;
        private int top = 0;
        public Stack(int capacity)
        {
            items = new T[capacity];
        }

        public void Push(T item)
        {
            items[top] = item;
            top = (top + 1) % items.Length;
        }
        public T Pop()
        {
            top = (items.Length + top - 1) % items.Length;
            T value = items[top];
            items[top] = default(T);
            return value;
        }
        public T Peek()
        {
            //top = (items.Length + top - 1) % items.Length;
            return items[top == 0 ? top : top-1];
        }
    }
}