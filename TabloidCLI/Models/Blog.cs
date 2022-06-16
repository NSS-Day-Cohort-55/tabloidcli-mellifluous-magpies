﻿using System.Collections.Generic;

namespace TabloidCLI.Models
{
    public class Blog : IContentTag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public override string ToString()
        {
            return $"{Title} ({Url})";
        }
    }
}