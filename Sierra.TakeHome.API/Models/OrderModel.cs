using System.ComponentModel.DataAnnotations;

namespace Sierra.TakeHome.API.Models;

/// <summary>
/// 
/// </summary>
public class OrderModel {
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public Guid ProductId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public Guid CustomerId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public decimal Total { get; set; }
}