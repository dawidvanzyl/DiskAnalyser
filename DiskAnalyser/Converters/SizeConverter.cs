using System;
using System.Collections.Generic;

namespace DiskAnalyser.Converters
{
    public class SizeConverter
    {
        public static NodeSize GetNodeSize(long bytes)
        {
            if (bytes < 1024)
            {
                return GetNodeSize(bytes, "b");
            }

            if (bytes > 1024 && bytes < 1048576)
            {
                return GetNodeSize(bytes, "kB");
            }

            if (bytes > 1048576 && bytes < 1073741824)
            {
                return GetNodeSize(bytes, "MB");
            }

            return GetNodeSize(bytes, "GB");
        }

        public static NodeSize GetNodeSize(long bytes, string unit)
        {
            var size = unit switch
            {
                "b" => Convert.ToDecimal(bytes),
                "kB" => Convert.ToDecimal(bytes) / 1024.0m,
                "MB" => Convert.ToDecimal(bytes) / 1048576.0m,
                _ => Convert.ToDecimal(bytes) / 1073741824.0m,
            };

            return NodeSize.From(size, unit);
        }

        public static long GetBytes(NodeSize nodeSize)
        {
            return nodeSize.Unit switch
            {
                "b" => Convert.ToInt64(nodeSize.Size),
                "kB" => Convert.ToInt64(nodeSize.Size * 1024),
                "MB" => Convert.ToInt64(nodeSize.Size * 1048576),
                _ => Convert.ToInt64(nodeSize.Size * 1073741824),
            };
        }
    }

    public class NodeSizeComparer : IComparer<NodeSize>
    {
        public int Compare(NodeSize x, NodeSize y)
        {
            if (x.Equals(y))
            {
                return 0;
            }

            var xBytes = SizeConverter.GetBytes(x);
            var yBytes = SizeConverter.GetBytes(y);

            var scaleXDown = xBytes > yBytes;

            var scaleNodeX = scaleXDown
                ? ScaleSize(x, y.Unit)
                : x;

            var scaleNodeY = !scaleXDown
                ? ScaleSize(y, x.Unit)
                : y;

            return decimal.Compare(scaleNodeX.Size, scaleNodeY.Size);
        }

        private static NodeSize ScaleSize(NodeSize fromSize, string toUnit)
        {
            var bytes = SizeConverter.GetBytes(fromSize);
            return SizeConverter.GetNodeSize(bytes, toUnit);
        }

        internal static NodeSizeComparer Create()
        {
            return new NodeSizeComparer();
        }
    }


    public struct NodeSize
    {
        private NodeSize(decimal size, string unit)
        {
            Size = Math.Round(size, 1);
            Unit = unit;
        }

        public decimal Size { get; }
        public string Unit { get; }

        public override string ToString()
        {
            return $"{Size} {Unit}";
        }

        internal static NodeSize From(decimal size, string unit)
        { 
            return new NodeSize(size, unit);
        }

        internal static NodeSize From(string nodeSize)
        {
            var nodesizeArray = nodeSize.Split(' ');
            return NodeSize.From(Convert.ToDecimal(nodesizeArray[0]), nodesizeArray[1]);
        }
    }
}
