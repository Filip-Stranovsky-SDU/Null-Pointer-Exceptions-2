using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1
{
    public static class FileHandler
    {
        public static void LoadFile(FileDTO file)
        {
            string[] lines = File.ReadAllLines(file.Path);
            if (lines.Length < 2)
            {
                throw new Exception("File doesn't include enough information");
            }
            string[] size = lines[0].Split(' ');
            if (size.Length < 2)
            {
                throw new Exception("Missing height or width");
            }

            if (!int.TryParse(size[0], out int height) || !int.TryParse(size[1], out int width) || file.Width < 1 || file.Height < 1)
            {
                throw new Exception("Invalid values");
            }

            file.Height = height;
            file.Width = width;
            file.Image = new int[file.Height, file.Width];

            for (int row = 0/*, lineIndex = 1*/; row < file.Height /*&& lineIndex < lines.Length*/; row++/*, lineIndex++*/)
            {
                /*string line = lines[lineIndex].Replace(" ", "");
                if (line.Length != file.Width) continue;*/
                string line = lines[1];

                for (int col = 0; col < file.Width; col++)
                {
                    file.Image[row, col] = line[row * file.Width + col] == '1' ? 1 : 0;
                }
            }
        }

        public static string SaveFile(FileDTO file)
        {
            string content = $"{file.Height} {file.Width}\n";
            foreach(int n in file.Image)
            {
                content = content.Insert(content.Length,n.ToString());
            }
            return content;
        }
    }
}
