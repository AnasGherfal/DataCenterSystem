﻿namespace Infrastructure.Models;

public class SubscriptionFile : BaseModel
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public int SubscriptionId { get; set; }

    //--------------Relations

    public Subscription Subscription { get; set; } 


}
