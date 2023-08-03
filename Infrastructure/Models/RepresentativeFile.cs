﻿namespace Infrastructure.Models;

public class RepresentativeFile : BaseModel
{
    public Guid Id { get; set; }
    public string Filename { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public short DocType { get; set; }
    public short IsActive { get; set; }
    public Guid RepresentativeId { get; set; }
    public Representative Representative { get; set; } = default!;
}
