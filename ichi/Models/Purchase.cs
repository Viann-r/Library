using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ichi.Models
{
    internal class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int Vol { get; set; }
        public override string? ToString()
        {
            return $"{Id}|{UserId}|{BookId}|{BookName}|{Vol}";
        }
    }
}
