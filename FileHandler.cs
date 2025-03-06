using Avalonia.Controls.Shapes;
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
            if (file.Content.Length < 2)
            {
                throw new Exception("File doesn't include enough information");
            }
            string[] size = file.Content[0].Split(' ');
            if (size.Length < 2)
            {
                throw new Exception("Missing height or width");
            }

            if (!int.TryParse(size[0], out int height) || !int.TryParse(size[1], out int width))
            {
                throw new Exception("Invalid values");
            }
            if ( width < 1 || height < 1) 
            {
                throw new Exception("Invalid values");
            }

            file.Height = height;
            file.Width = width;
            file.Image = new int[file.Height, file.Width];

            string line = "";
            for (int i = 1; i < file.Content.Length; i++)
            {
                line += file.Content[i];
            }

            for (int row = 0/*, lineIndex = 1*/; row < file.Height /*&& lineIndex < lines.Length*/; row++/*, lineIndex++*/)
            {
                /*string line = lines[lineIndex].Replace(" ", "");
                if (line.Length != file.Width) continue;*/
                
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
