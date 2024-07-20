using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;

public class UserDto
{
    public string Username { get; init; } = string.Empty;


    public UserDto()
    {

    }
    public UserDto(string userName)
    {
        Username = userName;

    }
}
