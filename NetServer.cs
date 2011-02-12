﻿using System;
using System.Collections.Generic;

namespace Lidgren.Network
{
	/// <summary>
	/// Specialized version of NetPeer used for "server" peers
	/// </summary>
	public class NetServer : NetPeer
	{
		public NetServer(NetPeerConfiguration config)
			: base(config)
		{
			config.AcceptIncomingConnections = true;
		}

		/// <summary>
		/// Send a message to all connections
		/// </summary>
		/// <param name="msg">The message to send</param>
		/// <param name="method">How to deliver the message</param>
		public void SendToAll(NetOutgoingMessage msg, NetDeliveryMethod method)
		{
			SendMessage(msg, this.Connections, method, 0);
		}

		/// <summary>
		/// Send a message to all connections except one
		/// </summary>
		/// <param name="msg">The message to send</param>
		/// <param name="method">How to deliver the message</param>


		public void SendToAll(NetOutgoingMessage msg, NetConnection except, NetDeliveryMethod method)
		{
			var all = this.Connections;
			if (all.Count <= 0)
				return;

			List<NetConnection> recipients = new List<NetConnection>(all.Count - 1);
			foreach (var conn in all)
				if (conn != except)
					recipients.Add(conn);

			if (recipients.Count > 0)
				SendMessage(msg, recipients, method, 0);
		}

		/// <summary>
		/// Returns a string that represents this object
		/// </summary>
		public override string ToString()
		{
			return "[NetServer " + ConnectionsCount + " connections]";
		}
	}
}
