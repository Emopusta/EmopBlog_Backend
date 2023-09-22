using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Blog : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }

        public Blog()
        {

        }
        public Blog(Guid id, string name, string description, string text, string ımage, string author)
        {
            Id = id;
            Name = name;
            Description = description;
            Text = text;
            Image = ımage;
            Author = author;
        }
    }
}
