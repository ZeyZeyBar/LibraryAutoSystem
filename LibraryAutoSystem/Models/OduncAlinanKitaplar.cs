using System;
using System.Collections.Generic;

namespace LibraryAutoSystem.Models;

public partial class OduncAlinanKitaplar
{
    public int KitapId { get; set; }

    public int UyeId { get; set; }

    public DateOnly OduncAlmaTarihi { get; set; }

    public virtual Kitaplar Kitap { get; set; } = null!;

    public virtual Uyeler Uye { get; set; } = null!;
}
