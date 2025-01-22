﻿using System;
using System.Collections.Generic;

namespace asp.net_lab.Models.Bookstore;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? AuthorName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}