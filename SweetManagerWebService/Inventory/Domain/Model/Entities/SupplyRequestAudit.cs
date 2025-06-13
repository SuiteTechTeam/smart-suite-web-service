using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;

public partial class SupplyRequestAudit: IEntityWithCreatedUpdatedDate
{
    [NotMapped]
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    
    [NotMapped]
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
    
}