﻿using System;
using System.Collections.Generic;

namespace IntershipTask4.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? LastLoginTime { get; set; }

    public bool IsActive { get; set; }

    public DateTime? DeletedAt { get; set; }

    public byte[] PasswordSalt { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;
}
