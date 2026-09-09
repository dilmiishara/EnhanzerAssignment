using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enhanzer.Api.Models;

[Table("Location_Details")]
public class LocationDetail
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Location_Code")]
    public string LocationCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    [Column("Location_Name")]
    public string LocationName { get; set; } = string.Empty;
}