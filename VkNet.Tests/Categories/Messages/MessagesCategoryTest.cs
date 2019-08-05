using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;
using VkNet.Enums;
using VkNet.Enums.SafetyEnums;
using VkNet.Exception;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkNet.Tests.Helper;
using VkNet.Tests.Infrastructure;

namespace VkNet.Tests.Categories.Messages
{
	[TestFixture]
	[SuppressMessage("ReSharper", "PublicMembersMustHaveComments")]
	[ExcludeFromCodeCoverage]
	public class MessagesCategoryTest : MessagesBaseTests
	{
		[Test]
		public void AddChatUser_NormalCase_True()
		{
			Url = "https://api.vk.com/method/messages.addChatUser";

			ReadJsonFile(JsonPaths.True);

			var result = Api.Messages.AddChatUser(2, 7550525);

			Assert.That(result, Is.True);
		}

		[Test]
		public void CreateChat_NormalCase_ChatId()
		{
			Url = "https://api.vk.com/method/messages.createChat";
			ReadCategoryJsonPath(nameof(CreateChat_NormalCase_ChatId));

			var chatId = Api.Messages.CreateChat(new ulong[]
				{
					5041431,
					10657891
				},
				"test chat's title");

			Assert.That(chatId, Is.EqualTo(3));
		}

		[Test]
		public void Delete_Id4446_True()
		{
			Url = "https://api.vk.com/method/messages.delete";

			ReadCategoryJsonPath(nameof(Delete_Id4446_True));

			var result = Api.Messages.Delete(new ulong[] { 4446 }, false, null, false);

			Assert.That(result[4446], Is.True);
		}

		[Test]
		public void Delete_Id999999_False()
		{
			Url = "https://api.vk.com/method/messages.delete";

			ReadErrorsJsonFile(1);

			Assert.That(() => Api.Messages.Delete(new ulong[] { 999999 }, false, null, false),
				Throws.InstanceOf<VkApiException>());
		}

		[Test]
		public void Delete_Multiple_4457And4464_True()
		{
			Url = "https://api.vk.com/method/messages.delete";

			ReadCategoryJsonPath(nameof(Delete_Multiple_4457And4464_True));

			var dict = Api.Messages.Delete(new ulong[]
				{
					4457,
					4464
				},
				false,
				null,
				false);

			Assert.That(dict.Count, Is.EqualTo(2));
			Assert.That(dict[4457], Is.True);
			Assert.That(dict[4464], Is.True);
		}

		[Test]
		public void EditChat_NormalCase_True()
		{
			Url = "https://api.vk.com/method/messages.editChat";

			ReadJsonFile(JsonPaths.True);

			var result = Api.Messages.EditChat(2, "new title");
			Assert.True(result);
		}

		[Test]
		public void GetById_Multiple_NormalCase_Messages()
		{
			Url = "https://api.vk.com/method/messages.getById";

			ReadCategoryJsonPath(nameof(GetById_Multiple_NormalCase_Messages));

			var msgs = Api.Messages.GetById(new ulong[]
				{
					1,
					3,
					5
				},
				null);

			Assert.That(msgs.TotalCount, Is.EqualTo(3));
			Assert.That(msgs.Count, Is.EqualTo(3));

			Assert.That(msgs[2].Id, Is.EqualTo(5));
			Assert.That(msgs[2].Out, Is.EqualTo(MessageType.Received));
			Assert.That(msgs[2].UserId, Is.EqualTo(684559));
			Assert.That(msgs[2].ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(msgs[2].Title, Is.EqualTo("Re(2): Как там зачетная неделя продвигаетсо?)"));

			Assert.That(msgs[2].Body,
				Is.EqualTo("Да тож не малина - последняя неделя жуть!<br>Надеюсь, домой успею ;)"));

			Assert.That(msgs[1].Id, Is.EqualTo(3));
			Assert.That(msgs[1].Out, Is.EqualTo(MessageType.Sended));
			Assert.That(msgs[1].UserId, Is.EqualTo(684559));
			Assert.That(msgs[1].ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(msgs[1].Title, Is.EqualTo("Re: Как там зачетная неделя продвигаетсо?)"));
			Assert.That(msgs[1].Body, Is.EqualTo("Парят и парят во все дыры)... у тебя как?"));
			Assert.That(msgs[0].Id, Is.EqualTo(1));
			Assert.That(msgs[0].Out, Is.EqualTo(MessageType.Received));
			Assert.That(msgs[0].UserId, Is.EqualTo(684559));
			Assert.That(msgs[0].ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(msgs[0].Title, Is.EqualTo(" ... "));
			Assert.That(msgs[0].Body, Is.EqualTo("Привеееет!!!!!!!!!!!"));
		}

		[Test]
		[Ignore("")]
		public void GetById_NormalCase_Message()
		{
			Url = "https://api.vk.com/method/messages.getById";

			ReadCategoryJsonPath(nameof(GetById_NormalCase_Message));

			var msg = Api.Messages.GetById(new ulong[] { 1 }, null).FirstOrDefault();

			Assert.That(msg.Id, Is.EqualTo(1));

			Assert.That(msg.Date,
				Is.EqualTo(new DateTime(2007,
					12,
					18,
					2,
					5,
					20,
					DateTimeKind.Utc)));

			Assert.That(msg.Out, Is.EqualTo(MessageType.Received));
			Assert.That(msg.UserId, Is.EqualTo(684559));
			Assert.That(msg.ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(msg.Title, Is.EqualTo(" ... "));
			Assert.That(msg.Body, Is.EqualTo("Привеееет!!!!!!!!!!!"));
		}

		[Test]
		public void GetChat_NormalCase_ChatObject()
		{
			Url = "https://api.vk.com/method/messages.getChat";

			ReadCategoryJsonPath(nameof(GetChat_NormalCase_ChatObject));

			var chat = Api.Messages.GetChat(2);

			Assert.That(chat.Id, Is.EqualTo(2));
			Assert.That(chat.Title, Is.EqualTo("test chat title"));
			Assert.That(chat.AdminId, Is.EqualTo(4793858));
			Assert.That(chat.Users.Count, Is.EqualTo(3));
			Assert.That(chat.Users.ElementAt(0), Is.EqualTo(4793858));
			Assert.That(chat.Users.ElementAt(1), Is.EqualTo(5041431));
			Assert.That(chat.Users.ElementAt(2), Is.EqualTo(10657891));
		}

		[Test]
		public void GetHistory_ContainsRepost_Error46()
		{
			Url = "https://api.vk.com/method/messages.getHistory";
			ReadCategoryJsonPath(nameof(GetHistory_ContainsRepost_Error46));

			var msgs = Api.Messages.GetHistory(new MessagesGetHistoryParams
			{
				UserId = 7712
			});

			// assertions
			Assert.That(msgs.TotalCount, Is.EqualTo(1940));
			var msg = msgs.Messages.FirstOrDefault();
			Assert.That(msg, Is.Not.Null);
			Assert.That(msg.Attachments.Count, Is.EqualTo(1));

			var wall = msg.Attachments[0].Instance as Model.Attachments.Wall;

			Assert.That(wall, Is.Not.Null);
			Assert.That(wall.Id, Is.EqualTo(6194));
			Assert.That(wall.FromId, Is.EqualTo(-1267));

			Assert.That(wall.Date, Is.EqualTo(DateHelper.TimeStampToDateTime(1414992610)));
			Assert.That(wall.PostType, Is.EqualTo(PostType.Post));
			Assert.That(wall.Text, Is.EqualTo(string.Empty));
			Assert.That(wall.Comments.Count, Is.EqualTo(3));
			Assert.That(wall.Comments.CanPost, Is.False);
			Assert.That(wall.Likes.Count, Is.EqualTo(9191));
			Assert.That(wall.Likes.UserLikes, Is.True);
			Assert.That(wall.Likes.CanLike, Is.False);
			Assert.That(wall.Likes.CanPublish, Is.EqualTo(true));
			Assert.That(wall.Reposts.Count, Is.EqualTo(953));
			Assert.That(wall.Reposts.UserReposted, Is.False);
			Assert.That(wall.Attachments.Count, Is.EqualTo(1));

			var photo = wall.Attachments[0].Instance as Photo;
			Assert.That(photo, Is.Not.Null);
		}

		[Test]
		public void GetHistory_ContainsSticker_Error47()
		{
			Url = "https://api.vk.com/method/messages.getHistory";
			ReadCategoryJsonPath(nameof(GetHistory_ContainsSticker_Error47));

			var msgs = Api.Messages.GetHistory(new MessagesGetHistoryParams
			{
				UserId = 7712,
				Count = 5,
				Offset = 3
			});

			// asserts
			Assert.That(msgs.TotalCount, Is.EqualTo(6));
			Assert.That(msgs.Messages.Count, Is.EqualTo(1));
			var msg = msgs.Messages.FirstOrDefault();

			Assert.That(msg, Is.Not.Null);
			Assert.That(msg.Attachments.Count, Is.EqualTo(1));

			var sticker = msg.Attachments[0].Instance as Sticker;
			Assert.That(sticker, Is.Not.Null);

			Assert.That(sticker.Id, Is.EqualTo(12345));
			Assert.That(sticker.ProductId, Is.EqualTo(54321));
		}

		[Test]
		[Ignore("")]
		public void GetHistory_NormalCaseAllFields_Messages()
		{
			Url = "https://api.vk.com/method/messages.getHistory";
			ReadCategoryJsonPath(nameof(GetHistory_NormalCaseAllFields_Messages));

			var msgs = Api.Messages.GetHistory(new MessagesGetHistoryParams());
			var messages = msgs.Messages.ToList();

			Assert.That(messages[2].Body, Is.EqualTo("думаю пива предложит попить"));
			Assert.That(messages[2].Id, Is.EqualTo(2095));
			Assert.That(messages[2].UserId, Is.EqualTo(4793858));

			Assert.That(messages[2].Date,
				Is.EqualTo(new DateTime(2010,
					9,
					25,
					18,
					34,
					4,
					DateTimeKind.Utc)));

			Assert.That(messages[2].ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(messages[2].Out, Is.EqualTo(MessageType.Sended));

			Assert.That(msgs.TotalCount, Is.EqualTo(18));
			Assert.That(messages.Count, Is.EqualTo(3));

			Assert.That(messages[0].Id, Is.EqualTo(2093));
			Assert.That(messages[0].Body, Is.EqualTo("Таких литовкиных и сычевых"));
			Assert.That(messages[0].UserId, Is.EqualTo(4793858));

			Assert.That(messages[0].Date,
				Is.EqualTo(new DateTime(2010,
					9,
					25,
					18,
					24,
					48,
					DateTimeKind.Utc)));

			Assert.That(messages[0].ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(messages[0].Out, Is.EqualTo(MessageType.Sended));

			Assert.That(messages[1].Body, Is.EqualTo("в одноклассниках и в майле есть."));
			Assert.That(messages[1].Id, Is.EqualTo(2094));
			Assert.That(messages[1].UserId, Is.EqualTo(7712));

			Assert.That(messages[1].Date,
				Is.EqualTo(new DateTime(2010,
					9,
					25,
					18,
					26,
					56,
					DateTimeKind.Utc)));

			Assert.That(messages[1].ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(messages[1].Out, Is.EqualTo(MessageType.Received));
		}

		[Test]
		public void GetLastActivity_NormalCast_LastActivityObject()
		{
			Url = "https://api.vk.com/method/messages.getLastActivity";
			ReadCategoryJsonPath(nameof(GetLastActivity_NormalCast_LastActivityObject));

			var activity = Api.Messages.GetLastActivity(77128);
			
			Assert.That(activity.IsOnline, Is.False);

			Assert.That(activity.Time,
				Is.EqualTo(new DateTime(2012,
					8,
					9,
					3,
					57,
					25,
					DateTimeKind.Utc)));
		}

		[Test]
		public void GetLongPollServer_NormalCase_LongPollServerResponse()
		{
			Url = "https://api.vk.com/method/messages.getLongPollServer";
			ReadCategoryJsonPath(nameof(GetLongPollServer_NormalCase_LongPollServerResponse));

			var response = Api.Messages.GetLongPollServer();

			Assert.That(response.Key, Is.EqualTo("6f4120988efaf3a7d398054b5bb5d019c5844bz3"));
			Assert.That(response.Server, Is.EqualTo("im46.vk.com/im1858"));
			Assert.That(response.Ts, Is.EqualTo("1627957305"));
		}

		[Test]
		public void GetLongPollServer_ThrowArgumentNullException()
		{
			Assert.That(() => Api.Messages.GetLongPollServer(), Throws.InstanceOf<ArgumentException>());
		}

		[Test]
		public void MarkAsRead_Multiple_NormalCase_True()
		{
			Url = "https://api.vk.com/method/messages.markAsRead";

			ReadJsonFile(JsonPaths.True);

			var result = Api.Messages.MarkAsRead(null);

			Assert.That(result, Is.True);
		}

		[Test]
		public void MarkAsRead_NormalCase_True()
		{
			Url = "https://api.vk.com/method/messages.markAsRead";

			ReadJsonFile(JsonPaths.True);

			var result = Api.Messages.MarkAsRead(null);

			Assert.That(result, Is.True);
		}

		[Test]
		public void RemoveChatUser_NormalCase_True()
		{
			Url = "https://api.vk.com/method/messages.removeChatUser";

			ReadJsonFile(JsonPaths.True);

			var result = Api.Messages.RemoveChatUser(2, 7550525);

			Assert.That(result, Is.True);
		}

		[Test]
		public void Restore_NormalCase_True()
		{
			Url = "https://api.vk.com/method/messages.restore";

			ReadJsonFile(JsonPaths.True);

			var result = Api.Messages.Restore(134);

			Assert.That(result, Is.True);
		}

		[Test]
		[Ignore("")]
		public void Search_NormalCase_Messages()
		{
			Url = "https://api.vk.com/method/messages.search";
			ReadCategoryJsonPath(nameof(Search_NormalCase_Messages));

			var result = Api.Messages.Search(new MessagesSearchParams
			{
				Query = "привет",
				Count = 3
			});

			var msgs = result.Items;

			Assert.That(result.Count, Is.EqualTo(680));
			Assert.NotNull(msgs);
			Assert.That(msgs.Count, Is.EqualTo(3));

			Assert.That(msgs[2].Id, Is.EqualTo(4414));

			Assert.That(msgs[2].Date,
				Is.EqualTo(new DateTime(2012,
					7,
					13,
					8,
					46,
					32,
					DateTimeKind.Utc)));

			Assert.That(msgs[2].Out, Is.EqualTo(MessageType.Received));
			Assert.That(msgs[2].UserId, Is.EqualTo(245242));
			Assert.That(msgs[2].ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(msgs[2].Title, Is.EqualTo(" ... "));
			Assert.That(msgs[2].Body, Is.EqualTo("привет, антон))"));

			Assert.That(msgs[1].Id, Is.EqualTo(4415));

			Assert.That(msgs[1].Date,
				Is.EqualTo(new DateTime(2012,
					7,
					13,
					8,
					46,
					48,
					DateTimeKind.Utc)));

			Assert.That(msgs[1].Out, Is.EqualTo(MessageType.Sended));
			Assert.That(msgs[1].UserId, Is.EqualTo(245242));
			Assert.That(msgs[1].ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(msgs[1].Title, Is.EqualTo(" ... "));
			Assert.That(msgs[1].Body, Is.EqualTo("привет))"));

			Assert.That(msgs[0].Id, Is.EqualTo(4442));

			Assert.That(msgs[0].Date,
				Is.EqualTo(new DateTime(2012,
					7,
					31,
					20,
					2,
					52,
					DateTimeKind.Utc)));

			Assert.That(msgs[0].Out, Is.EqualTo(MessageType.Received));
			Assert.That(msgs[0].UserId, Is.EqualTo(1016149));
			Assert.That(msgs[0].ReadState, Is.EqualTo(MessageReadState.Readed));
			Assert.That(msgs[0].Title, Is.EqualTo("..."));
			Assert.That(msgs[0].Body, Is.EqualTo("Привет, Антон! Как дела?"));
		}

		[Test]
		[Ignore("")]
		public void Search_NotExistedQuery_EmptyList()
		{
			Url = "https://api.vk.com/method/messages.search";

			ReadCategoryJsonPath(JsonPaths.EmptyVkCollection);

			var msgs = Api.Messages.Search(new MessagesSearchParams
			{
				Query = "fsjkadoivhjioashdpfisd",
				Count = 3
			});

			Assert.That(msgs.Count, Is.EqualTo(0));
		}

		[Test]
		public void SetActivity_NormalCase_True()
		{
			Url = "https://api.vk.com/method/messages.setActivity";

			ReadJsonFile(JsonPaths.True);

			var result = Api.Messages.SetActivity("7550525", MessageActivityType.Typing);

			Assert.That(result, Is.True);
		}
	}
}