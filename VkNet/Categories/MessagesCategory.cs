using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

// ReSharper disable MemberCanBePrivate.Global

namespace VkNet.Categories
{
	/// <inheritdoc />
	public partial class MessagesCategory : IMessagesCategory
	{
		private readonly IVkApi _vk;

		/// <summary>
		/// Методы для работы с сообщениями.
		/// </summary>
		/// <param name="vk"> API </param>
		public MessagesCategory(IVkApi vk)
		{
			_vk = vk;
		}

		/// <inheritdoc />
		public bool AddChatUser(long chatId, long userId)
		{
			return AddChatUserAsync(chatId, userId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public bool AllowMessagesFromGroup(long groupId, string key)
		{
			return AllowMessagesFromGroupAsync(groupId, key, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public bool DenyMessagesFromGroup(long groupId)
		{
			return DenyMessagesFromGroupAsync(groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		[Pure]
		public MessageGetHistoryObject GetHistory(MessagesGetHistoryParams @params)
		{
			return GetHistoryAsync(@params, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		[Pure]
		public VkCollection<Message> GetById(IEnumerable<ulong> messageIds, IEnumerable<string> fields, ulong? previewLength = null, bool? extended = null, ulong? groupId = null)
		{
			return GetByIdAsync(messageIds, fields, previewLength, extended, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public MessageSearchResult Search(MessagesSearchParams @params)
		{
			return SearchAsync(@params, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public long Send(MessagesSendParams @params)
		{
			return SendAsync(@params, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public ReadOnlyCollection<MessagesSendResult> SendToUserIds(MessagesSendParams @params)
		{
			return SendToUserIdsAsync(@params, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public bool DeleteConversation(long? userId, long? peerId = null, ulong? groupId = null)
		{
			return DeleteConversationAsync(userId, peerId, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public ConversationResultObject GetConversationsById(IEnumerable<long> peerIds, IEnumerable<string> fields, bool? extended = null, ulong? groupId = null)
		{
			return GetConversationsByIdAsync(peerIds, fields, extended, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public GetConversationsResult GetConversations(GetConversationsParams getConversationsParams)
		{
			return GetConversationsAsync(getConversationsParams, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public GetConversationMembersResult GetConversationMembers(long peerId, IEnumerable<string> fields, ulong? groupId = null)
		{
			return GetConversationMembersAsync(peerId, fields, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public GetByConversationMessageIdResult GetByConversationMessageId(long peerId, IEnumerable<ulong> conversationMessageIds, IEnumerable<string> fields, bool? extended = null, ulong? groupId = null)
		{
			return GetByConversationMessageIdAsync(peerId, conversationMessageIds, fields, extended, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public SearchConversationsResult SearchConversations(string q, IEnumerable<string> fields, ulong? count = null, bool? extended = null, ulong? groupId = null)
		{
			return SearchConversationsAsync(q, fields, count, extended, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public PinnedMessage Pin(long peerId, ulong? messageId = null)
		{
			return PinAsync(peerId, messageId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public GetImportantMessagesResult GetImportantMessages(GetImportantMessagesParams getImportantMessagesParams)
		{
			return GetImportantMessagesAsync(getImportantMessagesParams, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public IDictionary<ulong, bool> Delete(IEnumerable<ulong> messageIds, bool? spam = null, ulong? groupId = null, bool? deleteForAll = null)
		{
			return DeleteAsync(messageIds, spam, groupId, deleteForAll, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public bool Restore(ulong messageId, ulong? groupId = null)
		{
			return RestoreAsync(messageId, groupId).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public bool MarkAsRead(string peerId, long? startMessageId = null, long? groupId = null)
		{
			return MarkAsReadAsync(peerId, startMessageId, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public bool SetActivity(string userId, MessageActivityType type, long? peerId = null, ulong? groupId = null)
		{
			return SetActivityAsync(userId, type, peerId, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public LastActivity GetLastActivity(long userId)
		{
			return GetLastActivityAsync(userId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public Chat GetChat(long chatId, ProfileFields fields = null, NameCase nameCase = null)
		{
			return GetChatAsync(chatId, fields, nameCase).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public ChatPreview GetChatPreview(string link, ProfileFields fields)
		{
			return GetChatPreviewAsync(link, fields, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public ReadOnlyCollection<Chat> GetChat(IEnumerable<long> chatIds, ProfileFields fields = null, NameCase nameCase = null)
		{
			return GetChatAsync(chatIds, fields, nameCase, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public long CreateChat(IEnumerable<ulong> userIds, string title)
		{
			return CreateChatAsync(userIds, title, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public bool EditChat(long chatId, string title)
		{
			return EditChatAsync(chatId, title, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public bool RemoveChatUser(ulong chatId, long? userId = null, long? memberId = null)
		{
			return RemoveChatUserAsync(chatId, userId, memberId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		[Pure]

		//TODO: изменить тип lpVersion на nullable
		public LongPollServerResponse GetLongPollServer(bool needPts = false, uint lpVersion = 2, ulong? groupId = null)
		{
			return GetLongPollServerAsync(needPts, lpVersion, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public LongPollHistoryResponse GetLongPollHistory(MessagesGetLongPollHistoryParams @params)
		{
			return GetLongPollHistoryAsync(@params, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public Chat DeleteChatPhoto(out ulong messageId, ulong chatId, ulong? groupId = null)
		{
			var parameters = new VkParameters
			{
				{ "chat_id", chatId },
				{ "group_id", groupId }
			};

			var result = _vk.Call("messages.deleteChatPhoto", parameters);
			messageId = result["message_id"];

			return result["chat"];
		}

		/// <inheritdoc />
		public long SetChatPhoto(out long messageId, string file)
		{
			var json = JObject.Parse(file);
			var rawResponse = json["response"];

			var parameters = new VkParameters { { "file", rawResponse } };

			var result = _vk.Call("messages.setChatPhoto", parameters);
			messageId = result["message_id"];

			return result["chat"];
		}

		/// <inheritdoc />
		public ReadOnlyCollection<long> MarkAsImportant(IEnumerable<long> messageIds, bool important = true)
		{
			var parameters = new VkParameters
			{
				{ "message_ids", messageIds },
				{ "important", important }
			};

			VkResponseArray result = _vk.Call("messages.markAsImportant", parameters);

			return result.ToReadOnlyCollectionOf<long>(x => x);
		}

		/// <inheritdoc />
		public long SendSticker(MessagesSendStickerParams @params)
		{
			var parameters = @params;

			return _vk.Call("messages.sendSticker", parameters);
		}

		/// <inheritdoc />
		public ReadOnlyCollection<HistoryAttachment> GetHistoryAttachments(MessagesGetHistoryAttachmentsParams @params, out string nextFrom)
		{
			var result = _vk.Call("messages.getHistoryAttachments", @params);

			nextFrom = result["next_from"];

			return result.ToReadOnlyCollectionOf<HistoryAttachment>(o => o);
		}

		/// <inheritdoc />
		public string GetInviteLink(ulong peerId, bool reset)
		{
			return _vk.Call("messages.getInviteLink", new VkParameters
			{
				{ "peer_id", peerId },
				{ "reset", reset }
			})["link"];
		}

		/// <inheritdoc />
		public bool IsMessagesFromGroupAllowed(ulong groupId, ulong userId)
		{
			return _vk.Call("messages.isMessagesFromGroupAllowed", new VkParameters
			{
				{ "group_id", groupId },
				{ "user_id", userId }
			})["is_allowed"];
		}

		/// <inheritdoc />
		public long JoinChatByInviteLink(string link)
		{
			return _vk.Call("messages.joinChatByInviteLink",
				new VkParameters
				{
					{ "link", link }
				})["chat_id"];
		}

		/// <inheritdoc />
		public bool MarkAsAnsweredConversation(long peerId, bool? answered = null, ulong? groupId = null)
		{
			return _vk.Call("messages.markAsAnsweredConversation", new VkParameters
			{
				{ "peer_id", peerId },
				{ "answered", answered },
				{ "group_id", groupId }
			});
		}

		/// <inheritdoc />
		public bool MarkAsImportantConversation(long peerId, bool? important = null, ulong? groupId = null)
		{
			return _vk.Call("messages.markAsImportantConversation", new VkParameters
			{
				{ "peer_id", peerId },
				{ "important", important },
				{ "group_id", groupId }
			});
		}

		/// <inheritdoc />
		public bool Edit(MessageEditParams @params)
		{
			return _vk.Call("messages.edit", @params);
		}

		/// <inheritdoc />
		public bool Unpin(long peerId, ulong? groupId = null)
		{
			return UnpinAsync(peerId, groupId, CancellationToken.None).GetAwaiter().GetResult();
		}

		/// <inheritdoc />
		public GetRecentCallsResult GetRecentCalls(IEnumerable<string> fields, ulong? count = null, ulong? startMessageId = null, bool? extended = null)
		{
			return GetRecentCallsAsync(fields, count, startMessageId, extended, CancellationToken.None).GetAwaiter().GetResult();
		}
	}
}