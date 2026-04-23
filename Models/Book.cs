using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models;

public class Book
{
    public int Id { get; set; }
    
    [Required]
    public required string Title { get; set; }
    
    [Display(Name = "Cover Image")]
    public string? CoverImageUrl { get; set; }
    
    [Required]
    public required string Author { get; set; }
    public string? Genre { get; set; }
    public string? Publisher { get; set; }
    
    [Display(Name = "Publication Year")]
    public int PublicationYear { get; set; }
    public string? Language { get; set; }
    public int PageCount { get; set; }

    [Range(0, 100)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
}