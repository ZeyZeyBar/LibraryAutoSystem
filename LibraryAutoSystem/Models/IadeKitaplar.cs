using System;
using System.Collections.Generic;

namespace LibraryAutoSystem.Models;

public partial class IadeKitaplar
{
    public int IadeKitapId { get; set; }

    public int IadeEdenId { get; set; }

    public DateOnly? IadeTarihi { get; set; }

    public virtual Uyeler IadeEden { get; set; } = null!;

    public virtual Kitaplar IadeKitap { get; set; } = null!;
}
