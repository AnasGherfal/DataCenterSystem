﻿using Infrastructure.Constants;

namespace Infrastructure.Entities.Mappers;

public class DocumentForSubscription
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid SubscriptionId { get; set; } = Guid.Empty;
    public DocumentType FileType { get; set; } = DocumentType.Passport;
    public string FileLink { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public bool IsActive { get; set; }
}