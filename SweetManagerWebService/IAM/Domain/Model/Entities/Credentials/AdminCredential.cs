using System;
using System.Collections.Generic;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Commands.Credentials;

namespace SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;

public partial class AdminCredential
{
    public int AdminId { get; set; }

    public string? Code { get; set; }

    public virtual Admin Admin { get; set; } = null!;

    public AdminCredential() { }

    public AdminCredential(int userId, string code)
    {
        AdminId = userId;
        Code = code;
    }
}