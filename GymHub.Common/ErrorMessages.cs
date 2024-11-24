using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Common
{
    public static class ErrorMessages
    {
        public static string AlreadySignedForThisClass = "You are already signed for this class!";
        public static string NotSignedForThisClass = "You are not signed for this class!";
        public static string DontHaveMembership = "You don't have this membership to cancel!";
        public static string AlreadyHaveMembershipForGym = "You alreade have a membership for this gym!";
        public static string InvalidIdMessage = "You have entered invalid id!";
        public static string UsersAreSignedForClass = "You can't delete this class.Some users may be signed for this class!";
    }
}
