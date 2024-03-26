using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLogicLayer.Options;

public class GarnetOptions
{
    [Required(AllowEmptyStrings = false)]
    public required string Address { get; set; }
    [Required]
    [Range(1, 65535)]
    public int Port { get; set; }
    public int TimeoutMilliseconds { get; set; } = 0;
}