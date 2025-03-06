namespace AvaloniaApplication1
{
    public class FileDTO
    {
        public string[] Content { get; set; } = [];
        public int[,] Image { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public FileDTO(string[] content, int[,] image, int width, int height)
        {
            Content = content;
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
