using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Message : BaseEntity
{
    public int CarId { get; set; } = default!;
    public string Text { get; set; } = default!;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
