using MemoryPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamSalesNotifier.Shared.Models
{
    [MemoryPackable]
    public partial record User(string Name, string Mail);
}
