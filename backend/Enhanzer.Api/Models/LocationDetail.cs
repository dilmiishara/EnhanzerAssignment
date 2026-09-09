using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enhanzer.Api.Models;

[Table("Location_Details")]
public class LocationDetail
{
    [Key]
    public int Id { get; set; }

    [Column("Location_Code")]
    public string LocationCode { get; set; } = string.Empty;

    [Column("Location_Name")]
    public string LocationName { get; set; } = string.Empty;
}