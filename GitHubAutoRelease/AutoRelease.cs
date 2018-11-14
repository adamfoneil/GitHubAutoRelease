using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitHubAutoRelease.Models;
using Octokit;

namespace GitHubAutoRelease
{
	/// <summary>
	/// Enables a web app to create a GitHub Release on startup automatically.
	/// This makes it easy to know what code is in production without adding steps
	/// to your publish/deployment process
	/// </summary>
	public class AutoRelease
	{
		private readonly string _token;
		private readonly string _userAgent;		
		private readonly IEnumerable<RepoLocator> _repositories;

		public AutoRelease(string token, string userAgent, IEnumerable<RepoLocator> repositories)
		{
			_token = token;
			_userAgent = userAgent;			
			_repositories = repositories;
		}

		/// <summary>
		/// Call this in your web app startup to create a Release of your application's related repositor(ies).		
		/// </summary>		
		/// <param name="isLive">Set to true to indicate that your site is running in production environment and release should be created</param>
		/// <param name="tagName">Version number or leave blank to create release name based on today's date</param>
		public async Task CreateOnStartupAsync(bool isLive, string tagName = null)
		{
			if (!isLive) return;

			var client = new GitHubClient(new ProductHeaderValue(_userAgent));
			client.Credentials = new Credentials(_token);

			foreach (var repo in _repositories)
			{
				if (string.IsNullOrEmpty(tagName)) tagName = await GetReleaseNameNowAsync(client, repo);

				var release = new NewRelease(tagName);
				await client.Repository.Release.Create(repo.Owner, repo.Name, release);
			}
		}

		private async Task<string> GetReleaseNameNowAsync(GitHubClient client, RepoLocator repo)
		{
			string defaultName = DateTime.UtcNow.ToString("yyyy-MM-dd");
			string result = defaultName;

			int increment = 0;
			var releases = await client.Repository.Release.GetAll(repo.Owner, repo.Name);

			// if we do multiple releases on same day, then we need to append some increment digit to the release name

			if (releases.Any(r => r.Name.ToLower().Equals(result)))
			{
				do
				{
					increment++;
					result = $"{defaultName}.{increment}";
				} while (releases.Any(r => r.Name.Equals(result)));
			}

			return result;
		}
	}
}