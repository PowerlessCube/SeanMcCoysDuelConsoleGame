using System;
using System.Data;
using System.Linq.Expressions;

namespace SeanMcCoysDuelConsoleGame
{
    public class Card
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Card(int value, string name, string description)
        {
            Value = value;
            Name = name;
            Description = description;
        }
    }
}