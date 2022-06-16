using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.Models
{

    public interface IContentTag
    { 
        public List<Tag> Tags { get; set; }
    }
}