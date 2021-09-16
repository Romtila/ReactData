using System;

namespace ReactData.Models
{
    public class User
    {
        public int ID { get; set; }
        public DateTime DateRegistration { get; set; }
        public DateTime DateLastActivity { get; set; }
    }
}