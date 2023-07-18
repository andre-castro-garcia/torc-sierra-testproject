using System.ComponentModel.DataAnnotations;

namespace Sierra.TakeHome.API.Models; 

/// <summary>
/// 
/// </summary>
public class AuthModel {
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public Guid CustomerId { get; set; }
}