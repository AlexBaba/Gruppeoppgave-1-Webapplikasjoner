using Nettbutikk.Model;
using Nettbutikk.Model.RemovedEntities;
using System;

namespace Nettbutikk.Model.RemovedEntities
{
    public class OldCategory : Category, IChangedEntity
    {
        public DateTime Changed { get; set; }
        public Admin Changer { get; set; }
    }
}
