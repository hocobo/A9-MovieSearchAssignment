
using System.Collections.Generic;

namespace A9_MovieSearchAssignment.Models
{
    public abstract class Media 

    {
    public int Id { get; set; }
    public string Title { get; set; }

    public abstract void Display();
    public abstract void Read();
    
    }
}