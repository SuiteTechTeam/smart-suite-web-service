using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;

public partial class SupplyRequestAudit: IEntityWithCreatedUpdatedDate
{
    [NotMapped]
    [Column("Created At")] public DateTimeOffset? CreatedDate { get; set; }
    
    [NotMapped]
    [Column("Updated At")] public DateTimeOffset? UpdatedDate { get; set; }
    
}