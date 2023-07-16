﻿namespace Infrastructure.Models;

public class RepresentativeFile : BaseModel
{
    public Guid Id { get; set; }
    public string Filename { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public short DocType { get; set; }
    public Guid RepresentativeId { get; set; }

    //-----------Realations

    public Representative Representative { get; set; } = default!;
}
