using System.Collections.Generic;

namespace ConsoleApp.Snake
{
    /// <summary>Тело змейки: голова — первый элемент, хвост — последний.</summary>
    public sealed class SnakeBody
    {
        private readonly List<Point> _segments = new List<Point>();

        public Point Head
        {
            get { return _segments[0]; }
        }

        public int Length
        {
            get { return _segments.Count; }
        }

        public IReadOnlyList<Point> Segments
        {
            get { return _segments; }
        }

        public IEnumerable<Point> Tail
        {
            get
            {
                for (int i = 1; i < _segments.Count; i++)
                {
                    yield return _segments[i];
                }
            }
        }

        public void Reset(Point head, Direction direction, int length)
        {
            _segments.Clear();
            Point step = direction.Opposite().Delta();
            for (int i = 0; i < length; i++)
            {
                _segments.Add(new Point(head.X + step.X * i, head.Y + step.Y * i));
            }
        }

        public bool Contains(Point point)
        {
            for (int i = 0; i < _segments.Count; i++)
            {
                if (_segments[i] == point)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>Проверяет столкновение с телом при шаге в point.</summary>
        public bool CollidesAt(Point point, bool ignoreTail)
        {
            int count = _segments.Count - (ignoreTail ? 1 : 0);
            for (int i = 0; i < count; i++)
            {
                if (_segments[i] == point)
                {
                    return true;
                }
            }

            return false;
        }

        public void Move(Point next, bool grow)
        {
            _segments.Insert(0, next);
            if (!grow)
            {
                _segments.RemoveAt(_segments.Count - 1);
            }
        }
    }
}
