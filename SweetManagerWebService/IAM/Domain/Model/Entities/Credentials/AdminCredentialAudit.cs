using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials
{
    public partial class AdminCredential : IEntityWithCreatedUpdatedDate
    {
        [Column("created_at")] public DateTimeOffset? CreatedDate { get; set; }

        [Column("updated_at")] public DateTimeOffset? UpdatedDate { get; set; }
    }
}