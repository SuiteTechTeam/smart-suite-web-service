using System;
using System.Collections.Generic;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;

namespace SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;

public partial class GuestCredential
{
    public int GuestId { get; set; }

    public string? Code { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public GuestCredential() { }

    public GuestCredential(int guestId, string code)
    {
        GuestId = guestId;
        Code = code;
    }
}
