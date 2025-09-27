namespace Ecommerce.Domain.Entities;

/// <summary>
/// قاعدة مشتركة لجميع الكيانات. تحتوي فقط على معرّف من نوع int.
/// في مشروع حقيقي يمكن إضافة خاصية CreatedAt وUpdatedAt وأحداث النطاق.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
}