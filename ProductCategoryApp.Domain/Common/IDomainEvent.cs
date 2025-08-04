﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCategoryApp.Domain.Common
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
    }
}
