using System.Collections.Generic;

namespace PartyInvites.Models
{
    public static class Repository
    {
        private static List<GuestResponse> responses = new List<GuestResponse>();

        public static IEnumerable<GuestResponse> Responses => responses;

        public static void AddResponse(GuestResponse response)
        {
            responses.Add(response);
        }


        private static List<UserInfo> users = new List<UserInfo>();

        public static IEnumerable<UserInfo> Users => users;

        public static void AddUser(UserInfo request)
        {
            users.Add(request);
        }

        private static UserInfo loggedInusers = new UserInfo();

        public static void LogInUser(UserInfo request)
        {
            loggedInusers = request;
        }

        public static void LogOutUser()
        {
            loggedInusers = null;
        }
    }
}
