using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record CarUpdateDTO(Guid Id, string? Brand = default, string? Model = default, int? Year = default, int? Km = default, float? Price = default, Guid? UserId = default);

