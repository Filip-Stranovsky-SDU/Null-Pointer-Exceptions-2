using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            line = line.ToLower();
            for (int row = 0/*, lineIndex = 1*/; row < file.Height /*&& lineIndex < lines.Length*/; row++/*, lineIndex++*/)
            {
                /*string line = lines[lineIndex].Replace(" ", "");
                if (line.Length != file.Width) continue;*/
                
                for (int col = 0; col < file.Width; col++)
                {
                    
                    char c = line[row * file.Width + col];
                    int i = 1;
                    if(c>='0' && c<='9')
                    {
                        i = (int) (c-'0');
                    }
                    if(c>='a' && c<= 'f')
                    {
                        i = (int) (c-'a') + 10;
                    }
                    file.Image[row, col] = i;
                }
            }
        }

        public static string SaveFile(FileDTO file)
        {
            string content = $"{file.Height} {file.Width}\n";
            foreach(int n in file.Image)
            {
                content = content.Insert(content.Length,n.ToString("x"));
            }
            return content;
        }
    }
}
