using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Infrastructure.Data;
using System.Text.Json;
using Domain.Entities;
using Logic.Entities;
using Infrastructure.Services;

namespace Logic.Managers
{
    public class GithubManager
    {
        private const int _maxPageSize = 100;

        public async static Task<List<SearchCard>> GetSearchCardsAsync(
            SearchOnGithubContext context,
            string searchString)
        {
            string response;

            var searchQuery = context.SearchQuery
                .Where(s => s.Query.ToLower() == searchString.ToLower())
                .FirstOrDefault();

            if (searchQuery == null)
            {
                var githubEndPoint = $"https://api.github.com/search/repositories?q={searchString}";

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                using (HttpResponseMessage httpResponseMessage = httpClient.GetAsync(githubEndPoint).Result)
                {
                    httpResponseMessage.EnsureSuccessStatusCode();
                    response = await httpResponseMessage.Content.ReadAsStringAsync();
                }

                var newSearchQuery = new SearchQuery()
                {
                    Query = searchString.ToLower(),
                    Response = response
                };

                context.SearchQuery.Add(newSearchQuery);
                context.SaveChanges();
            }
            else
            {
                response = searchQuery.Response;
            }

            var githubRepositoryResponse = JsonSerializer.Deserialize<GithubRepositoryResponse>(response);

            var cards = githubRepositoryResponse.Items
                .Select(r => new SearchCard()
                {
                    NameProject = r.Name,
                    Author = r.Owner.Login,
                    Link = r.HtmlUrl,
                    StargazersCount = r.StargazersCount,
                    WatchersCount = r.WatchersCount
                })
                .ToList();

            return cards;
        }

        public static void Delete(SearchOnGithubContext context, Guid id)
        {
            var searchQuery = new SearchQuery() { Id = id };
            context.SearchQuery.Attach(searchQuery);
            context.SearchQuery.Remove(searchQuery);
            context.SaveChanges();
        }

        public static PageList<SearchQuery> GetPageList(SearchOnGithubContext context, int page, int pageSize)
        {
            if (pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }

            return PageList<SearchQuery>.Create(
                context.SearchQuery,
                page,
                pageSize);
        }
    }
}
