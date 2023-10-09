using DiskAnalyser.Models.ValueObjects;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskAnalyser.Models
{
    [Serializable]
    public class Snapshot
    {
        private readonly string _fileName;

        private Snapshot(TreeNode[] nodes, string description, bool cancelled, DateTime created, string fileName)
        {
            Nodes = nodes;
            Description = description;
            Cancelled = cancelled;
            Created = created;
            _fileName = fileName;
        }

        public bool Cancelled { get; }

        public DateTime Created { get; }

        public string Description { get; }

        public TreeNode[] Nodes { get; }

        public static Snapshot Create(TreeNode driveNode, DirectoryModel drive, bool cancelled)
        {
            var node = new TreeNode();
            node.Nodes.Add(driveNode);

            var treeNodes = node
                .Nodes
                .Cast<TreeNode>()
                .ToArray();

            var created = DateTime.Now;
            var description = $"{drive.Name} snapshot, {created:yyyy, dd MMM HH:mm}";
            var fileName = GetFileName(drive.Name);

            if (cancelled)
            {
                description = $"{description}. Incomplete.";
            }

            return new Snapshot(treeNodes, description, cancelled, created, fileName);
        }

        public static async Task<Snapshot> LoadAsync(DriveValue drive)
        {
            return await Task.Run(() =>
            {
                var fileName = GetFileName(drive.Name);
                using (Stream file = File.Open(fileName, FileMode.Open))
                {
                    var bf = new BinaryFormatter();
                    var obj = bf.Deserialize(file);

                    return obj as Snapshot;
                }
            });
        }

        public void Save()
        {
            using (Stream file = File.Open(_fileName, FileMode.Create))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(file, this);
            }
        }

        internal static void Delete(DriveValue drive)
        {
            File.Delete(GetFileName(drive.Name));
        }

        internal static bool Exists(DriveValue drive)
        {
            return File.Exists(GetFileName(drive.Name));
        }

        private static string GetFileName(string driveName)
        {
            return $"{driveName.Replace(":", "").Replace("\\", "")}_drive.snapshot";
        }
    }
}