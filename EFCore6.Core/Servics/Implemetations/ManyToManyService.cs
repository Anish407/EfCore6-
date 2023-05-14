using EFCore6.Core.Entities;
using EFCore6.Core.Repositories;
using EFCore6.Core.Servics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore6.Core.Servics.Implemetations
{
    public class ManyToManyService : IManyToManyService
    {
        public ManyToManyService(IPubContextUnitOfWork pubContextUnitOfWork)
        {
            PubContextUnitOfWork = pubContextUnitOfWork;
            ArtistRepository = pubContextUnitOfWork.ArtistRepository;
            CoversRepository = pubContextUnitOfWork.CoversRepository;
        }

        public IPubContextUnitOfWork PubContextUnitOfWork { get; }
        public IArtistRepository ArtistRepository { get; }
        public ICoversRepository CoversRepository { get; }

        public async Task SaveNewArtistCover(Artist artist, Cover cover)
        {
            cover.Artists.Add(artist);
            CoversRepository.Add(cover);
            var state = PubContextUnitOfWork.State;
            await PubContextUnitOfWork.SaveChanges();
        }
    }
}
