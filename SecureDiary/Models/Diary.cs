using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SecureDiary.Models
{
     public class Diary
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; }


        public DateTime DateOEntry { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public static implicit operator ObservableCollection<object>(Diary v)
        {
            throw new NotImplementedException();
        }
    }
}
