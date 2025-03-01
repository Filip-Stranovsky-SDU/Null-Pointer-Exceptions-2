using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1
{
    public class FileDTO
    {
        public string Path { get; set; } = "";
        public int[,] Image { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public FileDTO(string path, int[,] image, int width, int height)
        {
            Path = path;
            Image = image;
            Width = width;
            Height = height;
        }

        public FileDTO(int[,] image, int width, int height)
        {
            Image = image;
            Width = width;
            Height = height;
        }
    }
}
