using EFCore6.Core.Entities;

namespace EFCore6.Core.Servics.Interfaces
{
    public interface IManyToManyService
    {
        Task SaveNewArtistCover(Artist artist, Cover cover);
    }
}