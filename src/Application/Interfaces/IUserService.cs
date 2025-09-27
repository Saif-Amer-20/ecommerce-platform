using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces;

/// <summary>
/// خدمة إدارة المستخدمين والأذونات
/// User Management Service Interface
/// </summary>
public interface IUserService
{
    // Authentication & Registration
    Task<(bool Success, string Token, User? User)> RegisterAsync(string email, string password, string fullName, string? phone);
    Task<(bool Success, string Token, User? User)> LoginAsync(string email, string password);
    Task<bool> LogoutAsync(int userId);
    
    // User Profile Management
    Task<User?> GetUserProfileAsync(int userId);
    Task<bool> UpdateProfileAsync(int userId, string fullName, string? phone);
    Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    
    // Address Management
    Task<IEnumerable<Address>> GetUserAddressesAsync(int userId);
    Task<Address> AddAddressAsync(int userId, string street, string city, string state, string postalCode, string? description = null);
    Task<bool> UpdateAddressAsync(int addressId, int userId, string street, string city, string state, string postalCode, string? description = null);
    Task<bool> DeleteAddressAsync(int addressId, int userId);
    
    // Admin User Management
    Task<IEnumerable<User>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 20);
    Task<User?> GetUserByIdAsync(int userId);
    Task<bool> UpdateUserRoleAsync(int userId, int roleId);
    Task<bool> DeactivateUserAsync(int userId);
    Task<bool> ActivateUserAsync(int userId);
    
    // Audit & Security
    Task<IEnumerable<AuditLog>> GetAuditLogsAsync(int pageNumber = 1, int pageSize = 50);
    Task LogActionAsync(int? userId, string action, string tableName, string? details = null);
    
    // Role & Permission Management
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    Task<bool> HasPermissionAsync(int userId, string permission);
}