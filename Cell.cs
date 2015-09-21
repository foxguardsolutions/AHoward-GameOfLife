using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace GameOfLife
{
    public enum LifeStates
    {
        ALIVE = '*',
        DEAD = '.',
    }

    public class Cell : IComparable<Cell>
    {
        private readonly Dictionary<LifeStates, bool> _lifeIndicators = new Dictionary<LifeStates, bool>()
        {
            { LifeStates.DEAD,  false },
            { LifeStates.ALIVE, true },
        };

        public bool Alive { get; set; }

        public int LiveNeighbors { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(LifeStates state, int x, int y)
        {
            Alive = _lifeIndicators[state];
            LiveNeighbors = 0;
            X = x;
            Y = y;
        }

        public Cell(char initValue, int x, int y)
        {
            LifeStates state = (LifeStates)initValue;
            Alive = _lifeIndicators[state];
            LiveNeighbors = 0;
            X = x;
            Y = y;
        }

        public bool SurvivesGeneration()
        {
            return LiveNeighbors >= 2 && LiveNeighbors <= 3;
        }

        public bool SpawnsInGeneration()
        {
            return !Alive && LiveNeighbors == 3;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Cell other = obj as Cell;
            return X == other.X && Y == other.Y;
        }

        public bool Neighbors(Cell other)
        {
            return (int)Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2)) == 1;
        }

        public int CompareTo(Cell obj)
        {
            if (this.Equals(obj))
            {
                return 0;
            }

            return (Y < obj.Y) ? -1 : (Y > obj.Y) ? 1 : (X < obj.X) ? -1 : 1;
        }
    }
}