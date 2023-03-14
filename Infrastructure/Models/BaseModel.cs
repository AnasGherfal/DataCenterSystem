﻿namespace Infrastructure.Models;

public abstract class BaseModel
{
    public DateTime CreatedOn { get; set; }
    public int CreatedById { get; set; }

    //------Relation
    public User CreatedBy { get; set; } = new User();
}
