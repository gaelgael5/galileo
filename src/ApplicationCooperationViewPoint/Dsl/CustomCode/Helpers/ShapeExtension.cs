using System.Linq;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.Modeling;

namespace Bb
{

    public static class ShapeExtension
    {

        internal static void CenterOnItem(NodeShape nodeShape, List<NodeShape> items)
        {
            var position = nodeShape.Location;
            var size = nodeShape.Size;
            var x = position.X + (size.Width / 2);
            foreach (NodeShape node in items)
            {
                size = node.Size;
                node.Location = new PointD(x - (size.Width / 2), node.Location.Y);
            }
        }

        internal static void Center(List<NodeShape> items)
        {

            double minx = double.MaxValue;
            double maxx = double.MinValue;
            foreach (var node in items)
            {
                var position = node.Location;
                var size = node.Size;
                minx = Math.Min(position.X, minx);
                maxx = Math.Max(position.X + size.Width, maxx);
            }
            var x = (maxx - minx) / 2;
            foreach (NodeShape node in items)
            {
                var size = node.Size;
                node.Location = new PointD(x - (size.Width / 2), node.Location.Y);
            }
        }

        internal static void AlignLeft(List<NodeShape> items)
        {
            double minx = double.MaxValue;
            foreach (var node in items)
            {
                var position = node.Location;
                minx = Math.Min(position.X, minx);
            }
            foreach (NodeShape node in items)
                node.Location = new PointD(minx, node.Location.Y);
        }

        internal static void AlignRight(List<NodeShape> items)
        {

            double maxx = double.MinValue;

            foreach (var node in items)
            {
                var position = node.Location;
                var size = node.Size;
                maxx = Math.Max(position.X + size.Width, maxx);
            }

            foreach (NodeShape node in items)
            {
                var position = node.Location;
                var size = node.Size;
                var x = maxx - size.Width;
                node.Location = new PointD(x, node.Location.Y);
            }
        }

        internal static void AlignMiddle(List<NodeShape> items)
        {
            double miny = double.MaxValue;
            double maxy = double.MinValue;
            foreach (var node in items)
            {
                var position = node.Location;
                var size = node.Size;
                miny = Math.Min(position.Y, miny);
                maxy = Math.Max(position.Y + size.Height, maxy);
            }
            var y = (maxy - miny) / 2;
            foreach (NodeShape node in items)
            {
                var size = node.Size;
                node.Location = new PointD(node.Location.X, y + size.Height);
            }
        }

        internal static void AlignTop(List<NodeShape> items)
        {
            double miny = double.MaxValue;
            foreach (var node in items)
            {
                var position = node.Location;
                miny = Math.Min(position.Y, miny);
            }
            foreach (NodeShape node in items)
                node.Location = new PointD(node.Location.Y, miny);
        }

        internal static void AlignBottom(List<NodeShape> items)
        {

            double maxy = double.MinValue;

            foreach (var node in items)
            {
                var position = node.Location;
                var size = node.Size;
                maxy = Math.Max(position.Y + size.Height, maxy);
            }

            foreach (NodeShape node in items)
            {
                var position = node.Location;
                var size = node.Size;
                var y = maxy - size.Height;
                node.Location = new PointD(node.Location.X, y);
            }

        }

        internal static void DistributeVerticaly(List<NodeShape> items)
        {

            items = items.OrderBy(c => c.Location.Y).ToList();

            double miny = double.MaxValue;
            double maxy = double.MinValue;
            foreach (var node in items)
            {
                var position = node.Location;
                var size = node.Size;
                miny = Math.Min(position.Y, miny);
                maxy = Math.Max(position.Y + size.Height, maxy);
            }

            var width = maxy - miny;
            var step = width / items.Count - 1;

            for (int i = 1; i < items.Count - 1; i++)
            {

                var p1 = step * i;
                var node = items[i];

                var position = node.Location;
                var size = node.Size;

                var o = position.Y + (size.Height / 2);

                node.Location = new PointD(node.Location.X, o);

            }

        }

        internal static void DistributeHorizontaly(List<NodeShape> items)
        {

            items = items.OrderBy(c => c.Location.X).ToList();

            double miny = double.MaxValue;
            double maxy = double.MinValue;
            foreach (var node in items)
            {
                var position = node.Location;
                var size = node.Size;
                miny = Math.Min(position.Y, miny);
                maxy = Math.Max(position.Y + size.Height, maxy);
            }
         
            var width = maxy - miny;
            var step = width / items.Count - 1;

            for (int i = 1; i < items.Count - 1; i++)
            {

                var p1 = step * i;
                var node = items[i];

                var position = node.Location;
                var size = node.Size;

                var o = position.X + (size.Width / 2);

                node.Location = new PointD(o, node.Location.Y);

            }

        }


    }


}
