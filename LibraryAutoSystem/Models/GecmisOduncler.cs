using System;
using System.Collections.Generic;

namespace LibraryAutoSystem.Models;

public partial class GecmisOduncler
{
    public int? IadeKitapId { get; set; }

    public int? IadeEdenId { get; set; }

    public DateOnly? OduncAlmaTarihi { get; set; }
}
