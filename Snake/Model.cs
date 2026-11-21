using System;

namespace ConsoleApp.Snake
{
    /// <summary>Клетка игрового поля.</summary>
    public readonly struct Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtensions
    {
        public static Point Delta(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(0, -1);
                case Direction.Down:
                    return new Point(0, 1);
                case Direction.Left:
                    return new Point(-1, 0);
                case Direction.Right:
                    return new Point(1, 0);
                default:
                    return new Point(0, 0);
            }
        }

        public static Direction Opposite(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    return direction;
            }
        }

        public static bool IsOpposite(this Direction direction, Direction other)
        {
            return direction.Opposite() == other;
        }
    }

    public enum GameState
    {
        Ready,
        Running,
        Paused,
        GameOver,
        Won
    }

    public enum TickResult
    {
        Moved,
        Ate,
        HitWall,
        HitObstacle,
        HitSelf,
        Won
    }
}
