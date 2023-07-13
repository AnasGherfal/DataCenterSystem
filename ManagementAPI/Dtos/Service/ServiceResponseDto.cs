﻿using Infrastructure.Constants;

namespace ManagementAPI.Dtos.Service
{
    public class ServiceResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AmountOfPower { get; set; } = string.Empty;
        public string AcpPort { get; set; } = string.Empty;
        public string Dns { get; set; } = string.Empty;
        public int MonthlyVisits { get; set; }
        public decimal Price { get; set; }
        public GeneralStatus Status { get; set; }
    }
}
