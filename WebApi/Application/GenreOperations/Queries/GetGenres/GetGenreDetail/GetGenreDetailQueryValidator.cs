using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetailQuery
{
    public class GetGenreDetailQueryValidator:AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}