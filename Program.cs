using System.IO;

namespace Audit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = @"D:\projects\my-repositories\Audit";
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            using var audit = new XlsAudit("Audit", @"D:\audit.xls");

            foreach(var file in files)
            {
                var info = new FileModel(file);
                audit.AddRow(info);
            }
        }
    }
}