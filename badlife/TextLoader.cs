using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace badlife
{
    public class TextLoader : ITextLoader
    {
        public async Task<char[][]> LoadFile(string fileName)
        {
            if (fileName is null)
                throw new ArgumentNullException("fileName");

            return await ReadFile(fileName);
        }

        protected async Task<char[][]> ReadFile(string fileName)
        {
            var lines = new List<char[]>();

            using (var reader = new StreamReader(fileName))
            {
                string line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line.ToCharArray());
                }
            }

            return lines.ToArray();
        }
   
    }
}
