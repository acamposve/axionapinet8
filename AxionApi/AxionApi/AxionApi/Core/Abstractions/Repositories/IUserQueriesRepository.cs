﻿using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.Repositories;

public interface IUserQueriesRepository
{
    Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken);
}
