﻿namespace ManagementAPI.Dtos.Subscriptions
{
    public class SubscriptionFileResponsDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
    }
}