using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadImage.Models
{
    [Table("Image")]
    public class ImageModel
    {
        public ImageModel(string name, string image)
        {
            Name = name;
            Image = image;
        }

        public ImageModel()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
