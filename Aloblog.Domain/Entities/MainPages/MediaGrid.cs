﻿using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.MainPages;

public class MediaGrid:BaseEntity
{
    public string? MediaUrl { get; set; }
    public string? Alt { get; set; }
    public string? Poster { get; set; }
    public int Priority { get; set; }
}