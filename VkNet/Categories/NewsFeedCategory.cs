﻿using System;
using System.Collections.Generic;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkNet.Categories
{
	/// <summary>
	/// Методы для работы с новостной лентой пользователя.
	/// </summary>
	public partial class NewsFeedCategory : INewsFeedCategory
	{
		/// <summary>
		/// API.
		/// </summary>
		private readonly IVkApiInvoke _vk;

		/// <summary>
		/// Методы для работы с новостной лентой пользователя.
		/// </summary>
		/// <param name="vk"> API. </param>
		public NewsFeedCategory(IVkApiInvoke vk)
		{
			_vk = vk;
		}

		/// <inheritdoc />
		public NewsFeed Get(NewsFeedGetParams @params)
		{
			return _vk.Call<NewsFeed>("newsfeed.get", @params);
		}

		/// <inheritdoc />
		public NewsFeed GetRecommended(NewsFeedGetRecommendedParams @params)
		{
			return _vk.Call<NewsFeed>("newsfeed.getRecommended", @params);
		}

		/// <inheritdoc />
		public NewsFeed GetComments(NewsFeedGetCommentsParams @params)
		{
			return _vk.Call<NewsFeed>("newsfeed.getComments", @params);
		}

		/// <inheritdoc />
		public VkCollection<Mention> GetMentions(long? ownerId = null
												, DateTime? startTime = null
												, DateTime? endTime = null
												, long? offset = null
												, long? count = null)
		{
			var parameters = new VkParameters
			{
				{ "owner_id", ownerId },
				{ "start_time", startTime },
				{ "end_time", endTime },
				{ "offset", offset }
			};

			if (count <= 50)
			{
				parameters.Add("count", count);
			}

			return _vk.Call("newsfeed.getMentions", parameters).ToVkCollectionOf<Mention>(selector: x => x);
		}

		/// <inheritdoc />
		public NewsBannedList GetBanned()
		{
			return _vk.Call("newsfeed.getBanned", VkParameters.Empty);
		}

		/// <inheritdoc />
		public NewsBannedExList GetBannedEx(UsersFields fields = null, NameCase nameCase = null)
		{
			var parameters = new VkParameters
			{
				{ "extended", true },
				{ "fields", fields },
				{ "name_case", nameCase }
			};

			return _vk.Call("newsfeed.getBanned", parameters);
		}

		/// <inheritdoc />
		public bool AddBan(IEnumerable<long> userIds, IEnumerable<long> groupIds)
		{
			var parameters = new VkParameters
			{
				{ "user_ids", userIds },
				{ "group_ids", groupIds }
			};

			return _vk.Call("newsfeed.addBan", parameters);
		}

		/// <inheritdoc />
		public bool DeleteBan(IEnumerable<long> userIds, IEnumerable<long> groupIds)
		{
			var parameters = new VkParameters
			{
				{ "user_ids", userIds },
				{ "group_ids", groupIds }
			};

			return _vk.Call("newsfeed.deleteBan", parameters);
		}

		/// <inheritdoc />
		public bool IgnoreItem(NewsObjectTypes type, long ownerId, long itemId)
		{
			var parameters = new VkParameters
			{
				{ "type", type },
				{ "owner_id", ownerId },
				{ "item_id", itemId }
			};

			return _vk.Call("newsfeed.ignoreItem", parameters);
		}

		/// <inheritdoc />
		public bool UnignoreItem(NewsObjectTypes type, long ownerId, long itemId)
		{
			var parameters = new VkParameters
			{
				{ "type", type },
				{ "owner_id", ownerId },
				{ "item_id", itemId }
			};

			return _vk.Call("newsfeed.unignoreItem", parameters);
		}

		/// <inheritdoc />
		public NewsSearchResult Search(NewsFeedSearchParams @params)
		{
			return _vk.Call<NewsSearchResult>("newsfeed.search", @params);
		}

		/// <inheritdoc />
		public VkCollection<NewsUserListItem> GetLists(IEnumerable<long> listIds, bool? extended = null)
		{
			var parameters = new VkParameters
			{
				{ "list_ids", listIds },
				{ "extended", extended }
			};

			return _vk.Call("newsfeed.getLists", parameters).ToVkCollectionOf<NewsUserListItem>(selector: x => x);
		}

		/// <inheritdoc />
		public long SaveList(string title, IEnumerable<long> sourceIds, long? listId = null, bool? noReposts = null)
		{
			var parameters = new VkParameters
			{
				{ "list_id", listId },
				{ "title", title },
				{ "source_ids", sourceIds },
				{ "no_reposts", noReposts }
			};

			return _vk.Call("newsfeed.saveList", parameters);
		}

		/// <inheritdoc />
		public bool DeleteList(long listId)
		{
			var parameters = new VkParameters
			{
				{ "list_id", listId }
			};

			return _vk.Call("newsfeed.deleteList", parameters);
		}

		/// <inheritdoc />
		public bool Unsubscribe(CommentObjectType type, long itemId, long? ownerId = null)
		{
			var parameters = new VkParameters
			{
				{ "type", type },
				{ "owner_id", ownerId },
				{ "item_id", itemId }
			};

			return _vk.Call("newsfeed.unsubscribe", parameters);
		}

		/// <inheritdoc />
		public NewsSuggestions GetSuggestedSources(long? offset = null, long? count = null, bool? shuffle = null, UsersFields fields = null)
		{
			var parameters = new VkParameters
			{
				{ "offset", offset },
				{ "shuffle", shuffle },
				{ "fields", fields }
			};

			if (count <= 1000)
			{
				parameters.Add("count", count);
			}

			return _vk.Call("newsfeed.getSuggestedSources", parameters);
		}
	}
}