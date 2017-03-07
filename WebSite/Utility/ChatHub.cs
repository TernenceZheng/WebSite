using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models.ViewModels;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;


namespace WebSite.Utility
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        #region Data Members

        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        /// <summary>
        /// 登入，連線。
        /// </summary>
        /// <param name="userName"></param>
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            //先看List<UserDetail> 裡面有沒有同ID的，不能有相同的
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                //新增加使用者的id name   Add到List
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName });

                // send to caller固定寫法
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

                // send to all except caller client 固定寫法
                Clients.AllExcept(id).onNewUserConnected(id, userName);

            }

        }

        /// <summary>
        /// 聊天內容的「公告」，散佈給所有人。
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            AddMessageinCache(userName, message);

            // Broad cast message固定寫法
            Clients.All.messageReceived(userName, message);
        }

        /// <summary>
        /// 私人對話、私人聊天。
        /// </summary>
        /// <param name="toUserId"></param>
        /// <param name="message"></param>
        public void SendPrivateMessage(string toUserId, string message)
        {
            string fromUserId = Context.ConnectionId;

            var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {   // send to 
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }
        }


        /// <summary>
        /// //離線。
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected()   
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);   //有人離線，會出現提示。
            }
            return base.OnDisconnected();
        }  
        #endregion


        #region private Messages

        private void AddMessageinCache(string userName, string message)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        #endregion
    }
}