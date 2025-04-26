using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.ServerFolder;

namespace BluChat.ServerConsole.Commands.User
{
    public class UserUpdateCommand(Server server) : Command(server)
    {
        public override string Name => "UserUpdate";
        public override string Description => "this will update user information";
        public override string Format => "[username] ['username'or'password'] [NewValue]";

        private string Username { get; set; } = "";
        private Core.UserFolder.User? User { get; set; } = null;

        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;

            string typeInput = inputs[2];
            Username = inputs[1];


            User = server.Database.Users.GetFirstOrDefault(x => x.UserName.ToLower() == Username.ToLower());
            if (User == null)
            {
                Commander.SendErrorMessage("User not found");
                return;
            }


            switch (typeInput.ToLower())
            {
                case "username":
                    UserNameUpdate(inputs[3]);
                    break;
                case "password":
                    PasswordUpdate(inputs[3]);
                    break;
                default:
                    Commander.SendErrorMessage("Format is: [username] ['username' or 'password'] [new value]");
                    break;
            }
        }

        private void PasswordUpdate(string value)
        {
           server.Database.Users.UpdatePassword(Username, value);
           Commander.SendSuccessMessage("Password updated");
        }

        private void UserNameUpdate(string value)
        {
            User.UserName = value;
            server.Database.Users.Update(User);
            server.Database.Save();
            Commander.SendSuccessMessage($"Username Updated ({Username} > {value})");
        }
    }
}
