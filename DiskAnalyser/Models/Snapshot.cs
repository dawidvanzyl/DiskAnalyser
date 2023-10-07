using System;

namespace DiskAnalyser.Models
{
    public class Snapshot
    {
        public Snapshot(DirectoryNode directoryNode, bool cancelled, DateTime created)
        {
            DirectoryNode = directoryNode;
            Cancelled = cancelled;
            Created = created;
        }

        public bool Cancelled { get; set; }

        public DateTime Created { get; set; }

        public DirectoryNode DirectoryNode { get; set; }
    }
}