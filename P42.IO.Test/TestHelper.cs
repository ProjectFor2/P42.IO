using System.IO;
using System.Text;

namespace P42.IO.Test
{
    public class TestHelper
    {
        public static void CreateFile(string fullName)
        {
            if (!File.Exists(fullName))
            {
                using (var sw = new StreamWriter(fullName, false, Encoding.UTF8))
                {
                    sw.WriteLine("Test File");
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
    }
}
