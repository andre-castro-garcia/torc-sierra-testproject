using System.ComponentModel.DataAnnotations;

namespace Sierra.TakeHome.API.Models; 

/// <summary>
/// 
/// </summary>
public class CreateOrderModel {
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public Guid ProductId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    [Range(1, 100)]
    public int Quantity { get; set; }
}