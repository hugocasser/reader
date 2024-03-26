using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLogicLayer.Options;

public class GarnetOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Adress { get; set; }
    [Required]
    [Range(1, 65535)]
    public int Port { get; set; }
    public bool UseTLS { get; set; } = false;
    public int TimeoutMilliseconds { get; set; } = 0;
}