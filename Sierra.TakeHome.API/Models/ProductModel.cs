using System.ComponentModel.DataAnnotations;

namespace Sierra.TakeHome.API.Models;

/// <summary>
/// 
/// </summary>

public class ProductModel {
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public decimal Price { get; set; }
}