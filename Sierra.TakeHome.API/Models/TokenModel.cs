using System.ComponentModel.DataAnnotations;

namespace Sierra.TakeHome.API.Models; 

/// <summary>
/// 
/// </summary>
public class TokenModel {
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string Token { get; set; }
}