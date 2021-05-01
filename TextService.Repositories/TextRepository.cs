using Microsoft.Extensions.Options;
using RepositoryBase;

namespace TextService.Repositories
{
    public class TextRepository : BaseRepository<Text>, ITextRepository
    {
        public TextRepository(IOptions<TextDbOption> dbOption) : base(dbOption)
        {
        }
    }
}
