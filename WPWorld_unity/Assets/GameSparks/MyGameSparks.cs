#pragma warning disable 612,618
#pragma warning disable 0114
#pragma warning disable 0108

using System;
using System.Collections.Generic;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!

namespace GameSparks.Api.Requests{
		public class LogEventRequest_SUBMIT_SCORE : GSTypedRequest<LogEventRequest_SUBMIT_SCORE, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_SUBMIT_SCORE() : base("LogEventRequest"){
			request.AddString("eventKey", "SUBMIT_SCORE");
		}
		public LogEventRequest_SUBMIT_SCORE Set_SCORE( long value )
		{
			request.AddNumber("SCORE", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_SUBMIT_SCORE : GSTypedRequest<LogChallengeEventRequest_SUBMIT_SCORE, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_SUBMIT_SCORE() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "SUBMIT_SCORE");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_SUBMIT_SCORE SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_SUBMIT_SCORE Set_SCORE( long value )
		{
			request.AddNumber("SCORE", value);
			return this;
		}			
	}
	
}
	
	
	
namespace GameSparks.Api.Requests{
	
	public class LeaderboardDataRequest_LEADERBOARD : GSTypedRequest<LeaderboardDataRequest_LEADERBOARD,LeaderboardDataResponse_LEADERBOARD>
	{
		public LeaderboardDataRequest_LEADERBOARD() : base("LeaderboardDataRequest"){
			request.AddString("leaderboardShortCode", "LEADERBOARD");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LeaderboardDataResponse_LEADERBOARD (response);
		}		
		
		/// <summary>
		/// The challenge instance to get the leaderboard data for
		/// </summary>
		public LeaderboardDataRequest_LEADERBOARD SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		/// <summary>
		/// The number of items to return in a page (default=50)
		/// </summary>
		public LeaderboardDataRequest_LEADERBOARD SetEntryCount( long entryCount )
		{
			request.AddNumber("entryCount", entryCount);
			return this;
		}
		/// <summary>
		/// A friend id or an array of friend ids to use instead of the player's social friends
		/// </summary>
		public LeaderboardDataRequest_LEADERBOARD SetFriendIds( List<string> friendIds )
		{
			request.AddStringList("friendIds", friendIds);
			return this;
		}
		/// <summary>
		/// Number of entries to include from head of the list
		/// </summary>
		public LeaderboardDataRequest_LEADERBOARD SetIncludeFirst( long includeFirst )
		{
			request.AddNumber("includeFirst", includeFirst);
			return this;
		}
		/// <summary>
		/// Number of entries to include from tail of the list
		/// </summary>
		public LeaderboardDataRequest_LEADERBOARD SetIncludeLast( long includeLast )
		{
			request.AddNumber("includeLast", includeLast);
			return this;
		}
		
		/// <summary>
		/// The offset into the set of leaderboards returned
		/// </summary>
		public LeaderboardDataRequest_LEADERBOARD SetOffset( long offset )
		{
			request.AddNumber("offset", offset);
			return this;
		}
		/// <summary>
		/// If True returns a leaderboard of the player's social friends
		/// </summary>
		public LeaderboardDataRequest_LEADERBOARD SetSocial( bool social )
		{
			request.AddBoolean("social", social);
			return this;
		}
		/// <summary>
		/// The IDs of the teams you are interested in
		/// </summary>
		public LeaderboardDataRequest_LEADERBOARD SetTeamIds( List<string> teamIds )
		{
			request.AddStringList("teamIds", teamIds);
			return this;
		}
		/// <summary>
		/// The type of team you are interested in
		/// </summary>
		public LeaderboardDataRequest_LEADERBOARD SetTeamTypes( List<string> teamTypes )
		{
			request.AddStringList("teamTypes", teamTypes);
			return this;
		}
		
	}

	public class AroundMeLeaderboardRequest_LEADERBOARD : GSTypedRequest<AroundMeLeaderboardRequest_LEADERBOARD,AroundMeLeaderboardResponse_LEADERBOARD>
	{
		public AroundMeLeaderboardRequest_LEADERBOARD() : base("AroundMeLeaderboardRequest"){
			request.AddString("leaderboardShortCode", "LEADERBOARD");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new AroundMeLeaderboardResponse_LEADERBOARD (response);
		}		
		
		/// <summary>
		/// The number of items to return in a page (default=50)
		/// </summary>
		public AroundMeLeaderboardRequest_LEADERBOARD SetEntryCount( long entryCount )
		{
			request.AddNumber("entryCount", entryCount);
			return this;
		}
		/// <summary>
		/// A friend id or an array of friend ids to use instead of the player's social friends
		/// </summary>
		public AroundMeLeaderboardRequest_LEADERBOARD SetFriendIds( List<string> friendIds )
		{
			request.AddStringList("friendIds", friendIds);
			return this;
		}
		/// <summary>
		/// Number of entries to include from head of the list
		/// </summary>
		public AroundMeLeaderboardRequest_LEADERBOARD SetIncludeFirst( long includeFirst )
		{
			request.AddNumber("includeFirst", includeFirst);
			return this;
		}
		/// <summary>
		/// Number of entries to include from tail of the list
		/// </summary>
		public AroundMeLeaderboardRequest_LEADERBOARD SetIncludeLast( long includeLast )
		{
			request.AddNumber("includeLast", includeLast);
			return this;
		}
		
		/// <summary>
		/// If True returns a leaderboard of the player's social friends
		/// </summary>
		public AroundMeLeaderboardRequest_LEADERBOARD SetSocial( bool social )
		{
			request.AddBoolean("social", social);
			return this;
		}
		/// <summary>
		/// The IDs of the teams you are interested in
		/// </summary>
		public AroundMeLeaderboardRequest_LEADERBOARD SetTeamIds( List<string> teamIds )
		{
			request.AddStringList("teamIds", teamIds);
			return this;
		}
		/// <summary>
		/// The type of team you are interested in
		/// </summary>
		public AroundMeLeaderboardRequest_LEADERBOARD SetTeamTypes( List<string> teamTypes )
		{
			request.AddStringList("teamTypes", teamTypes);
			return this;
		}
	}
}

namespace GameSparks.Api.Responses{
	
	public class _LeaderboardEntry_LEADERBOARD : LeaderboardDataResponse._LeaderboardData{
		public _LeaderboardEntry_LEADERBOARD(GSData data) : base(data){}
		public long? SCORE{
			get{return response.GetNumber("SCORE");}
		}
	}
	
	public class LeaderboardDataResponse_LEADERBOARD : LeaderboardDataResponse
	{
		public LeaderboardDataResponse_LEADERBOARD(GSData data) : base(data){}
		
		public GSEnumerable<_LeaderboardEntry_LEADERBOARD> Data_LEADERBOARD{
			get{return new GSEnumerable<_LeaderboardEntry_LEADERBOARD>(response.GetObjectList("data"), (data) => { return new _LeaderboardEntry_LEADERBOARD(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_LEADERBOARD> First_LEADERBOARD{
			get{return new GSEnumerable<_LeaderboardEntry_LEADERBOARD>(response.GetObjectList("first"), (data) => { return new _LeaderboardEntry_LEADERBOARD(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_LEADERBOARD> Last_LEADERBOARD{
			get{return new GSEnumerable<_LeaderboardEntry_LEADERBOARD>(response.GetObjectList("last"), (data) => { return new _LeaderboardEntry_LEADERBOARD(data);});}
		}
	}
	
	public class AroundMeLeaderboardResponse_LEADERBOARD : AroundMeLeaderboardResponse
	{
		public AroundMeLeaderboardResponse_LEADERBOARD(GSData data) : base(data){}
		
		public GSEnumerable<_LeaderboardEntry_LEADERBOARD> Data_LEADERBOARD{
			get{return new GSEnumerable<_LeaderboardEntry_LEADERBOARD>(response.GetObjectList("data"), (data) => { return new _LeaderboardEntry_LEADERBOARD(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_LEADERBOARD> First_LEADERBOARD{
			get{return new GSEnumerable<_LeaderboardEntry_LEADERBOARD>(response.GetObjectList("first"), (data) => { return new _LeaderboardEntry_LEADERBOARD(data);});}
		}
		
		public GSEnumerable<_LeaderboardEntry_LEADERBOARD> Last_LEADERBOARD{
			get{return new GSEnumerable<_LeaderboardEntry_LEADERBOARD>(response.GetObjectList("last"), (data) => { return new _LeaderboardEntry_LEADERBOARD(data);});}
		}
	}
}	

namespace GameSparks.Api.Messages {


}
